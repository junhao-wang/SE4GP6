using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyProperties : MonoBehaviour {
    public GameObject OccupiedNode;
    public GameObject Canvas;
    public List<GameObject> path;
    public enum ResourceType {MONEY,SUPPLY ,SIZE};
    public static string[] ResourceNames = { "Money", "Supply" };
    public float[] Resources= new float[(int)ResourceType.SIZE];

    // Use this for initialization
    void Start () {
		
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

}
