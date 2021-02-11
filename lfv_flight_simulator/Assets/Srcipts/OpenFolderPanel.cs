using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using System.Net.Http;
#if UNITY_EDITOR
using UnityEditor;
#endif

using System;

using UnityEngine.UI;
using System.IO;
using LitJson;
public class OpenFolderPanel : MonoBehaviour
{
    // Start is called before the first frame update
    public Text text_json_length;
    private string[] files;
    public InputField json_start;
    public InputField json_size;

    private JsonData itemData;
    private LineRenderer m_LineRenderer = null;
    private List<Vector3> m_points = null;
    private List<double> _lat_edges = new List<double>() { 52.4816234690658519, 71.0305057345145769 };
    private List<double> _lon_edges = new List<double>() { 5.3961188288366806, 23.9450010942854021 };

    public Text text_fname;
    public Text text_length ;

    public float feet_scale;
    private List<Vector3> sphere_position;
    public Button Btn1;
    public Button Btn2;
    public struct Coord
    {
        public double[] x;
        public double[] y;
        public double[] lat;
        public double[] lon;
        public double[] measured_flight_level;
    };

    public struct CoordPredicted
    {
        public double[] predicted_lat;
        public double[] predicted_lon;
        public double[] afl_value;

    };
    public struct Flight
    {
        public string[] id;
        public string[] file_name;
        public string[] start_time;
        public Coord[] coord;
        public CoordPredicted[] predicted;

        public void display(int i)
        {
            Debug.Log(id[i]);
            Debug.Log(file_name[i]);
            Debug.Log(start_time[i]);
        }


    }

    public Flight flight = new Flight();

    //public string url = "https://raw.githubusercontent.com/igorfed/json/main/2016_11_11/205169.json";



    public void OpenFolder()
    {
        /*IEnumerator readJson()
         {

             using (WWW www = new WWW(url))
             {
                 yield return www;
                 string textFileContents = www.text;
                 Debug.Log(textFileContents);
             }



         }*/


        //string path = EditorUtility.OpenFolderPanel("Open folder with JSON files", "", ".json");
        //string[] files = Directory.GetFiles(path);

        string url = Application.streamingAssetsPath;
        string[] files = Directory.GetFiles(url, "*.json");
//        string[] files = Directory.GetFiles(@"C:/json", "*.json");
        //StartCoroutine(readJson());            
        
        int fileCount = files.Length;
        
        text_json_length.text = fileCount.ToString() + " plains";
        text_json_length.color = Color.red;
        //List<string> file_name = new List<string>();
        flight.id = new string[fileCount];
        flight.file_name = new string[fileCount];
        flight.start_time = new string[fileCount];
        flight.coord = new Coord[fileCount];
        flight.predicted = new CoordPredicted[fileCount];
        int i = 0;
        string start_time = null;
        string item_cnt = null;
        string jsonString;
        foreach (string file in files)
        {
            if (i < 50)
            {
                jsonString = File.ReadAllText(file);

                itemData = JsonMapper.ToObject(jsonString);
                flight.id[i] = itemData["id"].ToString();
                flight.file_name[i] = file.ToString();
                flight.start_time[i] = itemData["centre_ctrl"][0]["start_time"].ToString();
                //file_name.Add(file);
                //S = S + Path.GetFileName(file) + "\n";
                //I = I + i.ToString() + "\n";
                //id = id + itemData["id"] + "\n";
                start_time = start_time + itemData["centre_ctrl"][0]["start_time"].ToString() + "\n";
                item_cnt = item_cnt + itemData["plots"].Count.ToString() + "\n";
                //------------------------x ---------y -------------------
                flight.coord[i].lat = new double[itemData["plots"].Count];
                flight.coord[i].lon = new double[itemData["plots"].Count];
                flight.coord[i].measured_flight_level = new double[itemData["plots"].Count];


                for (int j = 0; j < itemData["plots"].Count; j++)
                {
                    flight.coord[i].lat[j] = Convert.ToDouble(itemData["plots"][j]["I062/105"]["lat"].ToString());
                    flight.coord[i].lon[j] = Convert.ToDouble(itemData["plots"][j]["I062/105"]["lon"].ToString());
                    //flight.coord[i].measured_flight_level[j] = (Convert.ToDouble(itemData["plots"][j]["I062/136"]["measured_flight_level"].ToString())); // value in meter * feet_scale
                    flight.coord[i].measured_flight_level[j] = Convert.ToDouble(itemData["plots"][j]["I062/136"]["measured_flight_level"].ToString());
                }
                /*for (int j = 0; j < itemData["predicted_trajectory"].Count; j++)
                {
                    flight.predicted[i].predicted_lat[j] = Convert.ToDouble(itemData["predicted_trajectory"][0]["route"][j]["lat"].ToString());
                    //flight.predicted[i].predicted_lon[j] = Convert.ToDouble(itemData["predicted_trajectory"][j]["lon"].ToString());
                    //flight.coord[i].measured_flight_level[j] = (Convert.ToDouble(itemData["plots"][j]["I062/136"]["measured_flight_level"].ToString())); // value in meter * feet_scale
                    //flight.predicted[i].afl_value[j] = Convert.ToDouble(itemData["plots"][j]["I062/136"]["measured_flight_level"].ToString());
                }*/
                /*Debug.Log("*-");
                for (int j = 0; j < itemData["predicted_trajectory"][0].Count; j++)
                {
                    Debug.Log(itemData["predicted_trajectory"][0]["route"][j]["lat"]);
                    //flight.predicted[i].predicted_lat[j] = Convert.ToDouble(itemData["predicted_trajectory"][0]["route"][j]["lat"].ToString());
                    //flight.predicted[i].predicted_lon[j] = Convert.ToDouble(itemData["predicted_trajectory"][0]["route"][j]["lon"].ToString());

                }*/
                //Debug.Log(itemData["predicted_trajectory"][0]["route"].Count);
                //Debug.Log(itemData["predicted_trajectory"][0]["route"][0]["lat"]);
                //Debug.Log(itemData["predicted_trajectory"][0]["route"][1]["lat"]);
                //Debug.Log("*-");
            }
            else
                break;
            //flight.display(i);
            i++;
        }
        Btn1.gameObject.SetActive(true);
        Btn2.gameObject.SetActive(false);


    }


    
    public class myCoordinates
    {
        public List<double> _lat_edges = new List<double>() { 52.4816234690658519, 71.0305057345145769 };
        public List<double> _lon_edges = new List<double>() { 5.3961188288366806, 23.9450010942854021 };
        public int Size = 11811;
        public List<double> _lat;
        public List<double> _lon;
        public List<Vector2> _delta(int Size, List<double> _lat_edges, List<double> _lon_edges)
        {
            List<Vector2> size = new List<Vector2>();
            float d_lat = (float)_lat_edges[1] - (float)_lat_edges[0];
            float d_lon = (float)_lon_edges[1] - (float)_lon_edges[0];
            /*d_lat = d_lat / Size;
            d_lon = d_lon / Size;*/
                Debug.Log(d_lat);
            Debug.Log(Size);

            size.Add(new Vector2((float)_lat_edges[0], (float)_lon_edges[0]));
            size.Add(new Vector2((float)d_lat, (float)d_lon));
            return size;
        }
        public List<Vector2> _lat_lon2xy(int height, List<float> _lat, List<float> _lon, List<Vector2> size)
        {
            List<Vector2> cities = new List<Vector2>();
            int length = _lat.Count;
            float Easting, Northing;
            for (int i = 0; i < length; i++)
            {
                Easting = ((float)_lat[i] - (float)size[0].x) * height / ((float)size[1].x);
                Northing = ((float)_lon[i] - (float)size[0].y) * height / ((float)size[1].y);
                cities.Add(new Vector2(Easting, Northing));
            }
            return cities;
        }
    }
    
    public void onButtonClick()
    {
        //int k;
        //k = 80;
        //float x, y;
        //Debug.Log("Clicked");
        // Debug.Log(flight.coord[1].lat.Length);

        float Easting, Northing, Height;
        GameObject MyFatherObj = GameObject.Find("FlightSimulator");
        GameObject Obj = new GameObject("flights");
        Obj.transform.parent = MyFatherObj.transform;
        text_fname.text = flight.start_time[0];
        text_length.text = flight.coord[0].lat.Length.ToString();
        for (int j = 0; j < flight.coord.Length; j++)
        {
            Debug.Log(j);
            m_LineRenderer = new GameObject().AddComponent<LineRenderer>();
            m_LineRenderer.positionCount = 0;
            m_LineRenderer.startColor = Color.black;
            m_LineRenderer.endColor = Color.white;
            m_LineRenderer.startWidth = 0.8f;
            m_LineRenderer.endWidth = 0.5f;
            //m_LineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Additive"));
            m_points = new List<Vector3>();
            myCoordinates __coordCities = new myCoordinates();
            List<Vector2> size = __coordCities._delta(__coordCities.Size, __coordCities._lat_edges, __coordCities._lon_edges);
            
            /////////////////////////
            GameObject newObj = new GameObject(flight.start_time[j]);
            Instantiate(newObj, Vector3.zero, Quaternion.identity);
            newObj.transform.parent = Obj.transform;
            ////////////////////////
            for (int i = 0; i < flight.coord[j].lat.Length; i++)
            {


                Easting = ((float)(flight.coord[j].lat[i]) - (float)size[0].x) * 1417 / ((float)size[1].x);
                Northing = ((float)(flight.coord[j].lon[i]) - (float)size[0].y) * 1181 / ((float)size[1].y);




                Height = (float)flight.coord[j].measured_flight_level[i] * (float) feet_scale;
                Height = (float)(Height * 30.48);
                

                if (i == 0)
                {
                    GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    sphere.name = "took off:" + (flight.coord[j].lat[i].ToString()) + " : " + (flight.coord[j].lon[i].ToString()); ;
                    Renderer rend = sphere.GetComponent<Renderer>();
                    rend.material.color = Color.black;
                    sphere.transform.parent = m_LineRenderer.transform;
                    sphere.transform.position = new Vector3(Northing, Height, Easting);

                    sphere.transform.localScale = new Vector3(2, 2, 2);
                    var sphereRender = sphere.GetComponent<Renderer>();
                    sphereRender.material.SetColor("_Color", Color.blue);
                }
                if (i == flight.coord[j].lat.Length - 1)
                {
                    GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    sphere.name = "Landed: " + (flight.coord[j].lat[i].ToString()) + " : " + (flight.coord[j].lon[i].ToString()); //S = S + Path.GetFileName(file) + "\n";
                    Renderer rend = sphere.GetComponent<Renderer>();
                    rend.material.color = Color.black;
                    sphere.transform.parent = m_LineRenderer.transform;
                    sphere.transform.position = new Vector3(Northing, Height, Easting);
                    sphere.transform.localScale = new Vector3(2, 2, 2);
                    var sphereRender = sphere.GetComponent<Renderer>();

                    sphereRender.material.SetColor("_Color", Color.red);
                }

                m_points.Add(new Vector3(Northing, 0, Easting));
                m_LineRenderer.transform.parent = Obj.transform;
                m_LineRenderer.positionCount = m_points.Count;
                m_LineRenderer.SetPosition(m_LineRenderer.positionCount - 1, new Vector3(Northing, Height, Easting));
                m_LineRenderer.name = flight.file_name[j];

                /*x = (float)(flight.coord[j].lat[i]);
                y = (float)(flight.coord[j].lon[i]);*/
                //Debug.Log(x);
                //Debug.Log(y);

                //GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                //sphere.transform.position = new Vector3(Northing, -50, Easting);
                //sphere.transform.localScale = new Vector3(30, 30, 30);

                //var sphereRender = sphere.GetComponent<Renderer>();
                //sphereRender.material.SetColor("_Color", Color.red);

            }
        }
        Btn1.gameObject.SetActive(false);
        Btn2.gameObject.SetActive(true);



    }

    public void onButtonClickDestroy() {

        Destroy(GameObject.Find("flights"));
        Btn1.gameObject.SetActive(true);
        Btn2.gameObject.SetActive(false);


    }
    public void Start()
    {
        files = Directory.GetFiles(@"Assets/json", "*.json");
        Btn1.gameObject.SetActive(false);
        Btn2.gameObject.SetActive(false);
        
        
    }

    

}
