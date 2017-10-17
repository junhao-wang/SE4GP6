using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodePartySelect : MonoBehaviour {
    public static GameObject PartyIcon;
    public static GameObject SourceNode;
    public static bool spawned = false;
    public static bool walk = false;
   
    // Use this for initialization
    void Start()
    {
        NodePartySelect.PartyIcon = GameObject.FindWithTag("Overworld Party");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseUpAsButton()
    {

        if (!spawned)
        {
            spawned = true;
            PartyIcon.GetComponent<SpriteRenderer>().enabled = true;
            PartyIcon.GetComponent<Transform>().position = gameObject.GetComponent<Transform>().position + new Vector3(0, 0.12f, -0.01f);
            PartyIcon.GetComponent<PartyProperties>().OccupiedNode = gameObject;
            return;
        }
        if (walk)
        {
            return;
        }
        if (SourceNode == null && gameObject == PartyIcon.GetComponent<PartyProperties>().OccupiedNode) 
        {
            SourceNode = gameObject;
            gameObject.GetComponent<NodeHoverScript>().SetActiveColor();
        }else if(SourceNode == gameObject)
        {
            SourceNode = null;
            gameObject.GetComponent<NodeHoverScript>().ReturnNaturalColor();
        }
        else if (SourceNode != null && SourceNode.GetComponent<NodeProperties>().Neighbors.Contains(gameObject))
        {
            walk = true;
            PartyIcon.GetComponent<PartyWalk>().startWalk(SourceNode.GetComponent<Transform>(), gameObject.GetComponent<Transform>());
            PartyIcon.GetComponent<PartyProperties>().OccupiedNode = gameObject;
            SourceNode = null;

        }
 

    }
}
