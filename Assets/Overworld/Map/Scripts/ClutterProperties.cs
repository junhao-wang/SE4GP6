using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ClutterProperties : MonoBehaviour {
    public int type;
    public static int Y1000Min=-3000;

    [System.Serializable]
    public struct ClutterSave
    {
        public float x, y,z;
        public int type;
    }

    public void sortOrder()
    {
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = ((int)gameObject.transform.position.y * 1000) - Y1000Min; 
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
