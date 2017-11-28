using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adjustment : MonoBehaviour {

    public Vector3 adjustment;
    private Vector3 newPos;
    bool isDone = false;

	// Use this for initialization
	void Start () {
        //newPos = new Vector3(transform.position.x + adjustment.x, transform.position.y + adjustment.y, transform.position.z + adjustment.z);
	}
	
	// Update is called once per frame
	/*void Update () {
        if (!isDone)
        {
            StartCoroutine(Update2());
            isDone = true; 
        }
        
	}

    private IEnumerator Update2()
    {
        System.Threading.Thread.Sleep(2000);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.6f);
        yield return 0;
    }*/
}
