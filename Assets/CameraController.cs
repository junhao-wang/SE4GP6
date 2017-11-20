using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float panDistance = 0.05f;
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("w") ){
            transform.Translate(0, panDistance, 0);
            //lastKey = "w";
        };
        if (Input.GetKey("a"))
        {
            transform.Translate(-panDistance, 0, 0);
            //lastKey = "a";
        };
        if (Input.GetKey("s"))
        {
            transform.Translate(0, -panDistance, 0);
            //lastKey = "s";
        };
        if (Input.GetKey("d") )
        {
            transform.Translate(panDistance, 0, 0);
            //lastKey = "d";
        };

    }
}
