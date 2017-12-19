﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PartyProperties : MonoBehaviour {
    public GameObject OccupiedNode;
    public GameObject Canvas;
    public List<GameObject> path;
    public List<int> CompletedDialogue = new List<int>();
    public bool inDialogue = false;
    public float initialMoney = 15, initialSupply = 15;
    public enum ResourceType {MONEY,SUPPLY ,SIZE};
    public static string[] ResourceNames = { "Money", "Supply" };
    public float[] Resources= new float[(int)ResourceType.SIZE];

    // Use this for initialization
    void Start () {
        Resources[(int)ResourceType.MONEY] = initialMoney;
        Resources[(int)ResourceType.SUPPLY] = initialSupply;
        DontDestroyOnLoad(transform.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void ProccessResourceEvent(float[] modifiers)
    {
        if (modifiers.Length == Resources.Length)
        {
            for(int i = 0; i < modifiers.Length; i++)
            {
                Resources[i] += modifiers[i];
            }
            GameObject Canvas = GameObject.FindWithTag("Overworld Canvas");
            Canvas.GetComponent<OverlayUIScripts>().UpdatePartyStats();
        }
        else
        {
            print("Error: Event Formatting Error!");
        }
    }

    public void ModResource(ResourceType t, float amt)
    {
        Resources[(int)t] += amt;
        Canvas.GetComponent<OverlayUIScripts>().UpdatePartyStats();
    } 

}