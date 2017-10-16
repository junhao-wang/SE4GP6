using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeHoverScript : MonoBehaviour {
    public Color TileColor;
    bool clickToggle = false;
    public bool Clickable = false;
	// Use this for initialization
	void Start () {
        TileColor = gameObject.GetComponent<SpriteRenderer>().material.color;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnMouseEnter()
    {
        gameObject.GetComponent<SpriteRenderer>().material.color = Color.blue;
        gameObject.GetComponent<NodeProperties>().drawPaths();
    }
    void OnMouseExit()
    {
        gameObject.GetComponent<NodeProperties>().clearPaths();
        if (clickToggle)
        {
            gameObject.GetComponent<SpriteRenderer>().material.color = Color.green;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().material.color = TileColor;
        }
    }

    void OnMouseUpAsButton()
    {
        /*
        if (!Clickable)
        {
            return;
        }
        */
        if (clickToggle)
        {
            gameObject.GetComponent<SpriteRenderer>().material.color = TileColor;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().material.color = Color.green;
        }
        clickToggle = !clickToggle;
    }
}
