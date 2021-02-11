using System.Collections;
using System.Collections.Generic;

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


using UnityEngine.UI;
public class Cities : MonoBehaviour
{
    // Start is called before the first frame update
    public int map_height;
    public int map_width;
    public GameObject AirplainOriginal;

    [Header("Show Airports")]
    public KeyCode pressF1;
    public KeyCode pressF2;
    public GameObject city;
    public Text text;
    private const string show_text = "to Show Airports [F1]";
    private const string hide_text = "to Hide Airports [F1]";

    public class CoordCities
    {
        //Corners left and right 11811 x 11811
        public List<double> _lat_edges = new List<double>() { 52.4816234690658519, 71.0305057345145769 };
        public List<double> _lon_edges = new List<double>() { 5.3961188288366806, 23.9450010942854021 };

        public int Size = 11811;

        public List<Vector2> _delta(int Size, List<double> _lat_edges, List<double> _lon_edges)
        {
            List<Vector2> size = new List<Vector2>();
            float d_lat = (float)_lat_edges[1] - (float)_lat_edges[0];
            float d_lon = (float)_lon_edges[1] - (float)_lon_edges[0];
            /*d_lat = d_lat / Size;
            d_lon = d_lon / Size;*/
            //Debug.Log(d_lat);
            //Debug.Log(Size);

            size.Add(new Vector2((float)_lat_edges[0], (float)_lon_edges[0]));
            size.Add(new Vector2((float)d_lat, (float)d_lon));
            return size;
        }
        public List<double> _lat = new List<double>()
                                            {
                                            59.651164062, 57.668799, 59.352665256, 55.524664568, 65.539497842, 63.788496846,
                                            63.189832574, 57.657664036, 67.85572, 56.2666656};

        public List<double> _lon = new List<double>()
                                            {
                                            17.917829662, 12.292314, 17.937162918, 13.369498522, 22.119832854,  20.276332228,
                                             14.501164662,  18.341165302,  20.22513,  15.258998964};

        public List<string> _airports = new List<string>()
                                            {
                                             "ARN", "GOT", "BMA", "MMX", "LLA", "UMA", "OSD", "VBY", "KRN", "RNB"

                                            };

        public List<Vector2> _lat_lon2xy(GameObject AirplainOriginal, int height, int width, List<double> _lat, List<double> _lon, List<string> _airports, List<Vector2> size)
        {
            List<Vector2> cities = new List<Vector2>();
            int length = _airports.Count;
            float Easting, Northing;
            //search Father Obj
            GameObject MyFatherObj = GameObject.Find("Cities");
            // create Obj in Top hierarchy with name New Game Object
            for (int i = 0; i < length; i++)
            {
                Easting = ((float)_lat[i] - (float)size[0].x) * 1417 / ((float)size[1].x);
                Northing = ((float)_lon[i] - (float)size[0].y) * 1181 / ((float)size[1].y);
                cities.Add(new Vector2(Easting, Northing));
                //GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                GameObject Airport = Instantiate(AirplainOriginal);
                Airport.transform.parent = MyFatherObj.transform;
                Airport.transform.position = new Vector3(Northing, -4, Easting);
                Airport.transform.localScale = new Vector3(12, 3, 12);
                Airport.name = _airports[i];

                //sphere.transform.parent = MyFatherObj.transform;
                ///sphere.transform.position = new Vector3(Northing, -4, Easting);
                //sphere.transform.localScale = new Vector3(5, 2, 5);
                //sphere.name = _airports[i];

                //var sphereRender = Airport.GetComponent<Renderer>();
                //sphereRender.material.SetColor("_Color", Color.red);
            }
            return cities;
        }




    }


    void Start()
    {
        CoordCities __coordCities = new CoordCities();

        List<Vector2> size = __coordCities._delta(__coordCities.Size, __coordCities._lat_edges, __coordCities._lon_edges);
        List<Vector2> cities = __coordCities._lat_lon2xy(AirplainOriginal, map_height, map_height, __coordCities._lat, __coordCities._lon, __coordCities._airports, size);
        


    }

    // Update is called once per frame
  

}




