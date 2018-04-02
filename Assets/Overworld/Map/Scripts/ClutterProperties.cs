using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ClutterProperties : MonoBehaviour {
    public int type;

    [System.Serializable]
    public struct ClutterSave
    {
        public float x, y,z;
        public int type;
    }

    public ClutterSave toClutterSave()
    {
        ClutterSave c = new ClutterSave();
        c.x = gameObject.transform.position.x;
        c.y = gameObject.transform.position.y;
        c.z = gameObject.transform.position.z;
        c.type = type;
        return c;
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
