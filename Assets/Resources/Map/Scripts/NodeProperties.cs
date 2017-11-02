using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeProperties : MonoBehaviour {
    public GameObject ParentTile;
    public GameObject PathPrefab;
    public List<GameObject> Neighbors;
    public bool visited = false;
    public List<GameObject> Pathing;
    public enum Region{A,B,C,D,E,NULL};
    public Region nodeRegion = Region.NULL;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //pathing functions
    public void drawPaths()
    {
        foreach(GameObject Node in Neighbors)
        {
            Connect(Node);
        }
    }
    public void clearPaths()
    {
        for(int i = Pathing.Count-1; i >=0 ; i--)
        {
            Destroy(Pathing[i]);
            Pathing.RemoveAt(i);
        }
    }

    void Connect(GameObject NodeB, List<GameObject> walkpath = null)
    {
        Vector3 unitVec = NodeB.GetComponent<Transform>().position - gameObject.GetComponent<Transform>().position;
        float length = unitVec.magnitude;
        unitVec = unitVec.normalized;
        float angle = Vector3.SignedAngle(new Vector3(1, 0,0), unitVec, new Vector3(1, 0, 0));
        if (unitVec[1] < 0)
        {
            angle *= -1;
        }
        print(angle);
        for (float f = 0.03f;f < length - 0.03f; f+= 0.06f)
        {
            GameObject path = Instantiate(PathPrefab);
            Vector3 offset = f * unitVec;
            path.GetComponent<Transform>().position = gameObject.GetComponent<Transform>().position + offset;
            path.GetComponent<Transform>().rotation = Quaternion.Euler(0,0, angle);
            if (walkpath==null)
            {
                Pathing.Add(path);
            }
            else
            {
                walkpath.Add(path);
            }

        }

    }

    public float costTo(GameObject NodeB)
    {
        return -1.0f;
    }

}
