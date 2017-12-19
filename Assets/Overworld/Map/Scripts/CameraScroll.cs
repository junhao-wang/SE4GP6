﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{

    public float panDistance = 0.05f;
    public float mouseBorder = 10f;

    public float maxY = 3f;
    public float minY = -4f;
    public float maxX = 2.5f;
    public float minX = -5f;

    private void Start()
    {
        transform.SetPositionAndRotation(new Vector3(minX, maxY, transform.position.z), transform.rotation);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) && (transform.position.y <= maxY))
        {
            transform.Translate(0, panDistance, 0);
            //lastKey = "w";
        };
        if (Input.GetKey(KeyCode.A) && (transform.position.x >= minX))
        {
            transform.Translate(-panDistance, 0, 0);
            //lastKey = "a";
        };
        if (Input.GetKey(KeyCode.S) && (transform.position.y >= minY))
        {
            transform.Translate(0, -panDistance, 0);
            //lastKey = "s";
        };
        if (Input.GetKey(KeyCode.D) && (transform.position.x <= maxX))
        {
            transform.Translate(panDistance, 0, 0);
            //lastKey = "d";
        };

    }
}