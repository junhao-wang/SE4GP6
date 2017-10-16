using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapProperties : MonoBehaviour {
    public GameObject[,] Tiles;
    public List<GameObject> Nodes;
    int rows;
    int cols;
	// Use this for initialization
	void Start () {
        


	}
    public void initList()
    {
        rows = gameObject.GetComponent<MapGenerator>().Rows;
        cols = gameObject.GetComponent<MapGenerator>().Cols;
        Tiles = new GameObject[rows, cols];
    }

    // Update is called once per frame
    void Update () {
		
	}
}
