using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileProperties : MonoBehaviour {
    public GameObject NodePrefab;
    public GameObject NodeChild;

	// Use this for initialization
	void Start () {
        NodeChild = Instantiate(NodePrefab);
        NodeChild.GetComponent<Transform>().position = calcRandNodeLocation();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    Vector3 calcRandNodeLocation()
    {
        Vector3 inipos = gameObject.GetComponent<Transform>().position;
        Vector3 offset = Random.insideUnitCircle *0.5f *0.8f;
        offset += new Vector3(0, 0, -0.1f);
        return inipos+ offset;
    }

}
