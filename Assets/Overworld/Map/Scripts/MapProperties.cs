using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapProperties : MonoBehaviour {
    public GameObject nodePrefab;
    public GameObject[,] Tiles;
    public static GameObject _instance;
    public List<GameObject> Nodes,Clutter;
    public GameObject PartyObject;
    bool nodeIDAssigned = false;
    int rows;
    int cols;

    

    // Use this for initialization
    void Start () {
        /*
        GameObject MController = GameObject.Find("MapController");
        MController.GetComponent<MapProperties>().loadmap();
        print("map loaded");
        */
        GameObject.Find("Canvas").GetComponent<DialogueControl>().startDialogue(11);
        DontDestroyOnLoad(transform.gameObject);
        
        Camera.main.transform.SetPositionAndRotation(new Vector3(-6, 2, Camera.main.transform.position.z), transform.rotation);



    }
    void Awake()
    {
        //if we don't have an [_instance] set yet
        if (!_instance)
            _instance = transform.gameObject;
        //otherwise, if we do, kill this thing
        else
            Destroy(transform.gameObject);


        DontDestroyOnLoad(transform.gameObject);
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Map" && gameObject != null)
        {
            
            gameObject.GetComponent<AudioSource>().mute = false;
            Camera.main.GetComponent<CameraScroll>().initX = PartyObject.transform.position.x;
            Camera.main.GetComponent<CameraScroll>().initY = PartyObject.transform.position.y;
            print("Party Location: " + PartyObject.transform.position);
            
        }
    }

    public void defeat()
    {
        GameObject[] thingsToDestroy = GameObject.FindGameObjectsWithTag("DestroyOnDefeat");
        for(int i = 0; i < thingsToDestroy.Length; i++)
        {
            Destroy(thingsToDestroy[i]);
        }
        Destroy(GameObject.FindGameObjectWithTag("Overworld Party"));
        Destroy(transform.gameObject);
    }
    //initialize tile list
    public void initList()
    {
        rows = gameObject.GetComponent<MapGenerator>().Rows;
        cols = gameObject.GetComponent<MapGenerator>().Cols;
        Tiles = new GameObject[rows, cols];
    }
    //not fully implemented yet
    public void savemap()
    {
        
        if (!nodeIDAssigned)
        {
            for (int i = 0; i < Nodes.Count; i++)
            {
                Nodes[i].GetComponent<NodeProperties>().ID = i;
            }
            nodeIDAssigned = true;
        }

        string strOut = "";
        for (int i = 0; i < Nodes.Count; i++)
        {
            strOut+= JsonUtility.ToJson(Nodes[i].GetComponent<NodeProperties>().toNodeSave());

            if (i < Nodes.Count - 1)
            {
                strOut += ";";
            }
        }
        strOut += "";
        string path = "Assets/Resources/Saves/node.txt";
        StreamWriter write = new StreamWriter(path);
        write.Write(strOut);
        write.Close();


        List<ClutterProperties.ClutterSave> c = new List<ClutterProperties.ClutterSave>();
        strOut = "";
        for (int i = 0; i < Clutter.Count; i++)
        {
            strOut += JsonUtility.ToJson(Clutter[i].GetComponent<ClutterProperties>().toClutterSave());
            if (i < Clutter.Count - 1)
            {
                strOut += ";";
            }

        }
        strOut += "";
        path = "Assets/Resources/Saves/clutters.txt";
        write = new StreamWriter(path);
        write.Write(strOut);
        write.Close();
        PartyProperties.PartySave p = new PartyProperties.PartySave();
        p = PartyObject.GetComponent<PartyProperties>().toPartySave();
        path = "Assets/Resources/Saves/party.txt";
        write = new StreamWriter(path);
        write.Write(JsonUtility.ToJson(p));
        write.Close();



    }  
    public bool loadmap()
    {
        string pathn = "Assets/Resources/Saves/node.txt";
        string pathc = "Assets/Resources/Saves/clutters.txt";
        string pathp = "Assets/Resources/Saves/party.txt";
        bool loadPossible = File.Exists(pathn) && File.Exists(pathc) && File.Exists(pathp);
        if (!loadPossible)
        {
            print("failed");
            return false;
        }

        for(int i = 0; i < Nodes.Count; i++)
        {
            Nodes[i].GetComponent<NodeProperties>().clearCurrentPaths();
            Nodes[i].GetComponent<NodeProperties>().clearPaths();
            Destroy(Nodes[i]);
        }
        Nodes.Clear();
        for(int i = 0; i < Clutter.Count; i++)
        {
            Destroy(Clutter[i]);
        }
        Clutter.Clear();
        string strin;
        StreamReader reader = new StreamReader(pathn);
        Nodes = new List<GameObject>();
        List<NodeProperties.NodeSave> n = new List<NodeProperties.NodeSave>();
        strin = reader.ReadToEnd();
        foreach (string s in strin.Split(';'))
        {
            n.Add(JsonUtility.FromJson<NodeProperties.NodeSave>(s));
        }

        NodeProperties.NodeSave[] NodeSaves = n.ToArray();
        for(int i = 0; i < NodeSaves.Length; i++)
        {
            GameObject node = GameObject.Instantiate(nodePrefab);
            Nodes.Add(node);
        }

        for(int i =0; i < NodeSaves.Length; i++)
        {
            Nodes[i].GetComponent<NodeProperties>().fromNodeSave(NodeSaves[i]);
        }

        reader.Close();

        reader = new StreamReader(pathc);
        List<ClutterProperties.ClutterSave> c = new List<ClutterProperties.ClutterSave>();
        strin = reader.ReadToEnd();
        foreach (string s in strin.Split(';'))
        {

            c.Add(JsonUtility.FromJson<ClutterProperties.ClutterSave>(s));
        }
        ClutterProperties.ClutterSave[] Clutters = c.ToArray();
        for(int i = 0; i < Clutters.Length; i++)
        {
           GameObject newclutter = GameObject.Instantiate(gameObject.GetComponent<MapGenerator>().ClutterPrefab);
            newclutter.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<MapGenerator>().clutters[Clutters[i].type];
            newclutter.GetComponent<ClutterProperties>().type = Clutters[i].type;
            newclutter.transform.position = new Vector3(Clutters[i].x, Clutters[i].y, Clutters[i].z);
            Clutter.Add(newclutter);
            print(JsonUtility.ToJson(Clutter[i].GetComponent<ClutterProperties>().toClutterSave()));
        }
        reader.Close();

        reader = new StreamReader(pathp);

        PartyObject.GetComponent<PartyProperties>().fromPartySave(JsonUtility.FromJson<PartyProperties.PartySave>(reader.ReadToEnd()));

        PartyObject.GetComponent<PartyProperties>().OccupiedNode.GetComponent<NodeProperties>().drawCurrentPaths();



        StartCoroutine(tidyUp());
        



        return true;
    }
    // Update is called once per frame

    IEnumerator tidyUp()
    {
        yield return new WaitForSeconds(1);
        GameObject Canvas = GameObject.FindWithTag("Overworld Canvas");
        Canvas.GetComponent<DialogueControl>().skipDialogue();

        StartCoroutine(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScroll>().SnapToParty());
    }
    void Update () {
		if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Map");
        }
	}
}
