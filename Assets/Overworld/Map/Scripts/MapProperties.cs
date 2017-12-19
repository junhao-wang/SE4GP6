using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class MapProperties : MonoBehaviour {
    public GameObject[,] Tiles;
    public List<GameObject> Nodes;
    public GameObject PartyObject;
    bool nodeIDAssigned = false;
    int rows;
    int cols;
    [System.Serializable]
    public struct NodeStruct
    {
        public int ID;


    }
    // Use this for initialization
    void Start () {
        /*
        GameObject MController = GameObject.Find("MapController");
        MController.GetComponent<MapProperties>().loadmap();
        print("map loaded");
        */
        DontDestroyOnLoad(transform.gameObject);


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
        }


        

    }  
    public void loadmap()
    {

          
        

    }
    // Update is called once per frame
    void Update () {
		
	}
}
