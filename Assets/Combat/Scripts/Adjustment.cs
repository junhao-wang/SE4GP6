using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adjustment : MonoBehaviour {

    public Vector3 adjustment;
    private Vector3 newPos;
    bool isDone = false;

  
	// Use this for initialization
	void Start () {
        newPos = new Vector3(transform.position.x + adjustment.x, transform.position.y + adjustment.y, transform.position.z + adjustment.z);
	}
	
	// Update is called once per frame
	void Update () {
        if (!isDone)
        {
            //newPos.z += 0.6f;
            transform.position = newPos;
            isDone = true;
        }
        
	}
}
