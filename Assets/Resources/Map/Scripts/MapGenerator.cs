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
    public float PruneChance;


    struct Edge
    {
        int from;
        int to;
        float dist;
    }

    // Use this for initialization
    void Start () {
        gameObject.GetComponent<MapProperties>().initList();
        GenerateTiles(Rows, Cols);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void GenerateTiles(int numOfRows,int numOfCols)
    {
        //place all phase
        for(int i = 0; i < numOfRows; i++)
        {
            for(int j = 0;j < numOfCols; j++)
            {
                GameObject Tile = InitTile(j, i);
                Tile.GetComponent<TileProperties>().GenNode();
                gameObject.GetComponent<MapProperties>().Tiles[i, j] = Tile;
                //add nodes to master list
                gameObject.GetComponent<MapProperties>().Nodes.Add(Tile.GetComponent<TileProperties>().NodeChild);
            }
        }
        //deletion phase method 1
        /*
        for (int i = 0; i < numOfRows; i++)
        {
            for (int j = 0; j < numOfCols; j++)
            {
                fillNeighbours(gameObject.GetComponent<MapProperties>().Tiles[i, j], j, i);
            }
        }

        print("Generation Phase Done");

        for (int i = 0; i < numOfRows; i++)
        {
            for (int j = 0; j < numOfCols; j++)
            {
                if(gameObject.GetComponent<MapProperties>().Tiles[i, j].GetComponent<TileProperties>().NodeChild != null)
                {
                    gameObject.GetComponent<MapProperties>().Tiles[i, j].GetComponent<TileProperties>().NodeChild.GetComponent<NodeProperties>().visited = true;
                    List<GameObject> neigh = gameObject.GetComponent<MapProperties>().Tiles[i, j].GetComponent<TileProperties>().NodeChild.GetComponent<NodeProperties>().Neighbors;
                    for (int n = 0; n < neigh.Count; n++)
                    {
                        if(neigh[n] == null)
                        {
                            continue;
                        }
                        if (!neigh[n].GetComponent<NodeProperties>().visited)
                        {
                            float roll = Random.Range(0.0f, 1.0f);
                            if (roll <= PruneChance)
                            {
                                Destroy(neigh[n]);
                            }
                        }
                    }
                }
            }
        }
 

    */

    
     //method 2

        //randomly remove nodes from the list based on preset probability and prune out all the destroyed entries
        for (int i = gameObject.GetComponent<MapProperties>().Nodes.Count-1; i >= 0; i--)
        {
            float roll = Random.Range(0.0f, 1.0f);
            if (roll <= PruneChance)
            {

                Destroy(gameObject.GetComponent<MapProperties>().Nodes[i]);
                gameObject.GetComponent<MapProperties>().Nodes.RemoveAt(i);
            }
        }
        //calculate distance matrix
        int l = gameObject.GetComponent<MapProperties>().Nodes.Count;
        float[,] distance = new float[l, l];
        for(int a = 0; a < l; a++)
        {
            distance[a, a] = float.MaxValue;
            for (int b = a+1; b < l; b++)
            {
                distance[a, b] = Vector2.Distance(gameObject.GetComponent<MapProperties>().Nodes[a].GetComponent<Transform>().position, gameObject.GetComponent<MapProperties>().Nodes[b].GetComponent<Transform>().position);
                distance[b, a] = distance[a, b];
            }
        }
        //Prim's Algorithm
        
        int[] bestI = new int[l];
        int[] flags = new int[l];
        for (int i = 0; i < l; i++)
        {
            bestI[i] = -1;
            flags[i] = -1;
        }
        float[,] adjacencyMatrix = (float[,])distance.Clone();
        bool notDone = true;
        int pivot = 0;
        flags[0] = 1;
        while (notDone)
        {
            
            //delete row from choosen vortex
            for(int i = 0; i < l; i++)
            {
                adjacencyMatrix[pivot, i] = float.MaxValue;
            }

            //find lowest value in column
            float min = float.MaxValue;
            int newPivot = -1;
            for (int i = 0; i < l; i++)
            {
                if(flags[i] > 0)
                {
                    for (int j = 0; j < l; j++)
                    {
                       
                       if (adjacencyMatrix[j,i] < min)
                        {
                            //print(adjacencyMatrix[j, i]);
                            min = adjacencyMatrix[j, i];
                            newPivot = j;
                        } 
                    }
                }
            }
            if(newPivot != -1)
            {
                bestI[pivot] = newPivot;
                pivot = newPivot;
                flags[pivot] = 1;
            }
            else
            {
                print("Error with minimum spanning tree");
                return;
            }

            //check if all columns have been visited
            int counter = 0;
            for(int i = 0; i < l; i++)
            {
                if(bestI[i] >= 0)
                {
                    counter++;
                }
            }

            notDone = (counter == l-1);
            //print(bestI.ToString());
        }

        //connect the nodes
        int index = 0;
        while (bestI[index] != -1)
        {
            connectNode(gameObject.GetComponent<MapProperties>().Nodes[index], gameObject.GetComponent<MapProperties>().Nodes[bestI[index]]);
            index = bestI[index];
            //print(string.Format("connecting %i to %i", index, bestI[index]));
        }
        
        
        


        //calculate and connect 3 closest node for each node
        for (int i = 0; i < l; i++)
        {
            int[] minIndices = { 0, 1, 2 };
            float[] minDistances = { distance[i,0], distance[i,1], distance[i,2] };
            for (int j = 3;j < l; j++)
            {
                if( distance[i, j] <= 0)
                {
                    continue;
                }
                if(distance[i,j] < minDistances[0]  )
                {
                    minIndices[0] = j;
                    minDistances[0] = distance[i, j];
                }else if(distance[i, j] < minDistances[1] )
                {
                    minIndices[1] = j;
                    minDistances[1] = distance[i, j];
                }
                else if (distance[i, j] < minDistances[2])
                {
                    minIndices[2] = j;
                    minDistances[2] = distance[i, j];
                }
            }
            connectNode(gameObject.GetComponent<MapProperties>().Nodes[i], gameObject.GetComponent<MapProperties>().Nodes[minIndices[0]]);
            connectNode(gameObject.GetComponent<MapProperties>().Nodes[i], gameObject.GetComponent<MapProperties>().Nodes[minIndices[1]]);
            connectNode(gameObject.GetComponent<MapProperties>().Nodes[i], gameObject.GetComponent<MapProperties>().Nodes[minIndices[2]]);



        }
        //debug draw all paths
        /*
        foreach(GameObject Node in gameObject.GetComponent<MapProperties>().Nodes)
        {
            Node.GetComponent<NodeProperties>().drawPaths();
        }
        */


    }

    void connectNode(GameObject nodeA,GameObject nodeB)
    {
        if (!nodeA.GetComponent<NodeProperties>().Neighbors.Contains(nodeB))
        {
            nodeA.GetComponent<NodeProperties>().Neighbors.Add(nodeB);
        }
        if (!nodeB.GetComponent<NodeProperties>().Neighbors.Contains(nodeA))
        {
            nodeB.GetComponent<NodeProperties>().Neighbors.Add(nodeA);
        }
    }



    /*

    
    void fillNeighbours(GameObject Tile,int i, int j)
    {
        for(int x = -1;x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                if(!(x == 0 && y == 0)){
                    if (inRange(i+x, j+y))
                    {
                        Tile.GetComponent<TileProperties>().NodeChild.GetComponent<NodeProperties>().Neighbors.Add(gameObject.GetComponent<MapProperties>().Tiles[i + x, j + y].GetComponent<TileProperties>().NodeChild);
                    }
                }
            }
        }
    }
    */



    bool inRange(int x, int y)
    {
        if (0<=x && x < Rows && 0<= y && y< Cols)
        {
            return true;
        }
        else
        {
            return false;
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
