using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using System.IO;
using LitJson;
public class LocalizationManager : MonoBehaviour
{
    // Start is called before the first frame update
    string path;
    string jsonString;
    private JsonData itemData;
    [System.Serializable]
    public class Flight
    {
        public string[] id;
        public string[] file_name;
        public string[] start_time;
        public Coord[] coord;

    }
    public class Coord
    {
        public double[] x;
        public double[] y;
        public double[] lat;
        public double[] lon;
        public double[] measured_flight_level;
    };
    

    void Start()
    {
        string url = Application.streamingAssetsPath;
        //UnityWebRequest www = UnityWebRequest.Get(url);
        //url = www.ToString();
        //string[] dirs = Directory.GetDirectories(url, " * ", SearchOption.TopDirectoryOnly);
        string[] files = Directory.GetFiles(url, "*.json");
        foreach (string file in files)
        {
            Debug.Log(file);

        }
            path = Application.streamingAssetsPath + "/200673.json";
        jsonString = File.ReadAllText(path);


        

        itemData = JsonMapper.ToObject(jsonString);
        Flight flight = new Flight();
        flight.id = new string[1];
        flight.file_name = new string[1];
        flight.start_time = new string[1];
        flight.coord = new Coord[1];
        

        flight.id[0] = itemData["id"].ToString();

        Debug.Log(flight.id[0]);
        Debug.Log(url);
        Debug.Log(files);

    }

    


    // Update is called once per frame
    void Update()
    {
        
    }
}
