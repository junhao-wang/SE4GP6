using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeProperties : MonoBehaviour {
    public GameObject ParentTile;
    public GameObject PathPrefab;
    public List<GameObject> Neighbors;
    public bool visited = false;
    public List<GameObject> Pathing;
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
            connect(Node);
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

    void connect(GameObject NodeB)
    {
        Vector3 unitVec = NodeB.GetComponent<Transform>().position - gameObject.GetComponent<Transform>().position;
        float length = unitVec.magnitude;
        unitVec = unitVec.normalized;
        float angle = Vector3.Angle(new Vector3(1, 0), unitVec);
        for (float f = 0.03f;f < length - 0.03f; f+= 0.06f)
        {
            GameObject path = Instantiate(PathPrefab);
            Vector3 offset = f * unitVec;
            path.GetComponent<Transform>().position = gameObject.GetComponent<Transform>().position + offset;
            path.GetComponent<Transform>().rotation *= Quaternion.AngleAxis(-1*angle, Vector3.forward);
            Pathing.Add(path);
        }

    }

}
