using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverlayUIScripts : MonoBehaviour {
    public GameObject PartyStats,ChangePopUp;
    public PartyProperties pProp;
	// Use this for initialization
	void Start () {
        GameObject Party = GameObject.FindWithTag("Overworld Party");
        pProp = Party.GetComponent<PartyProperties>();
        UpdatePartyStats();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void UpdatePartyStats()
    {
        PartyStats.GetComponent<Text>().text = string.Format("Party Resources:  Money: {0:0.00}  Supplies: {1:0}", pProp.Resources[(int)PartyProperties.ResourceType.MONEY], pProp.Resources[(int)PartyProperties.ResourceType.SUPPLY]);
    }

    public void UpdateNodePopUp(NodeProperties nProp)
    {
        string output = "";
        switch (nProp.NodeEvent)
        {
            case (NodeProperties.EventType.COMBAT):
                output += "There are enemies near this location. Brace for combat!";
                break;
            case (NodeProperties.EventType.NARRATIVECORE):
                output += "Continue your story upon venturing here.";
                output += ResourceModToString(nProp.ResourceMod);
                break;
            case (NodeProperties.EventType.NARRATIVE):
                output += "There seems to be something interesting happening here.";
                output += ResourceModToString(nProp.ResourceMod);
                break;
        }
        ChangePopUp.GetComponent<Text>().text = output;

    }

    string ResourceModToString(float[] ResourceMods)
    {
        string output = "This node contains a resource cache: ";
        bool isMod = false;
        for(int i = 0;i <(int)PartyProperties.ResourceType.SIZE; i++)
        {
            if(ResourceMods[i] > 0f)
            {
                isMod = true;
                output += string.Format(" {0} {1} ", ResourceMods[i], PartyProperties.ResourceNames[i]);
            }
        }
        if (!isMod)
        {
            output = "";
        }
        
        return output;
    }

    public void ClearNodePopUp()
    {
        ChangePopUp.GetComponent<Text>().text = "";
    }
}
