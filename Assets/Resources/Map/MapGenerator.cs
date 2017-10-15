using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public GameObject TilePrefab;
    public float VerticleTilingOffset;
    public float HorizontleTilingOffset;
    public float HorizontalRowOffset;
    public int Rows;
    public int Cols;


	// Use this for initialization
	void Start () {
        GenerateTiles(Rows, Cols);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void GenerateTiles(int numOfRows,int numOfCols)
    {
        for(int i = 0; i < numOfRows; i++)
        {
            for(int j = 0;j < numOfCols; j++)
            {
                InitTile(j, i); 

            }
        }
    }

    Vector3 calcSpawnPositionFromIndex(int x, int y)
    {
        Vector3 initpos = gameObject.GetComponent<Transform>().position;
        Vector3 offset = new Vector3(x * HorizontleTilingOffset + (y % 2 * HorizontalRowOffset),-1*VerticleTilingOffset * y,10);//not sure why i need the 10 there
        return initpos + offset;
    }

   
    GameObject InitTile(int x,int y)
    {
        GameObject Tile = Instantiate(TilePrefab);
        Tile.transform.position = calcSpawnPositionFromIndex(x, y);
        return Tile;
    }

}
