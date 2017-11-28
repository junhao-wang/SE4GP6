using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyProperties : MonoBehaviour {
    public GameObject OccupiedNode;
    public List<GameObject> path;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ClearPath()
    {
        for (int i = path.Count-1; i >0; i--)
        {

        }
    }
}
