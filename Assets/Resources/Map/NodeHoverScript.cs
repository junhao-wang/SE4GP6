using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeHoverScript : MonoBehaviour {
    public Color TileColor;
    bool clickToggle = false;
    public bool Clickable = false;
	// Use this for initialization
	void Start () {
        TileColor = gameObject.GetComponent<Renderer>().material.color;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnMouseEnter()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.yellow;
    }
    void OnMouseExit()
    {
        if (clickToggle)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.blue;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.color = TileColor;
        }
    }

    void OnMouseUpAsButton()
    {
        if (!Clickable)
        {
            return;
        }
        if (clickToggle)
        {
            gameObject.GetComponent<Renderer>().material.color = TileColor;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.color = Color.blue;
        }
        clickToggle = !clickToggle;
    }
}
