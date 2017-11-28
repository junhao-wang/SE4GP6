using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float panDistance = 0.05f;
    public float mouseBorder = 10f;

    public float maxZ = -0.7f;
    public float minZ = -3.5f;
    public float maxX = 7f;
    public float minX = -1.5f;

	// Update is called once per frame
	void Update () {
        if (Input.mousePosition.y >= Screen.height - mouseBorder && (transform.position.z <= maxZ)){
            transform.Translate(0, panDistance, 0);
            //lastKey = "w";
        };
        if (Input.mousePosition.x <=  mouseBorder && (transform.position.x >= minX))
        {
            transform.Translate(-panDistance, 0, 0);
            //lastKey = "a";
        };
        if (Input.mousePosition.y <= mouseBorder && (transform.position.z >= minZ))
        {
            transform.Translate(0, -panDistance, 0);
            //lastKey = "s";
        };
        if (Input.mousePosition.x >= Screen.width - mouseBorder && (transform.position.x<= maxX))
        {
            transform.Translate(panDistance, 0, 0);
            //lastKey = "d";
        };

    }
}
