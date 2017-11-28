using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//This script states the type of item and what values that it passes, will be used for item prefabs//
public class ItemInfo : MonoBehaviour {


	public string name;
	public int type;
	public int level;
	public float StatChange;
	public int cooldown;
	private int cdi;		//cooldown index
	public int amount;
	private LevelInfo ls;
	private bool setLevelFlag;


	// Use this for initialization
	void Start () {
		//ls = Instantiate (gameObject).GetComponentInChildren <LevelInfo>();
		ls = gameObject.GetComponentInChildren <LevelInfo>();
		getLevel ();
		setType ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void cooldowntick(){
		cdi--;
		if (cdi == 0) {
			cdi = cooldown;
		}
	}

	void setType(){
		if (type == 0) {	//Item is a one time consumable that is removed
			cooldown = 0;
			cdi = 0;
			amount = 0;
		}
		if (type == 1) {	//Item is a reusable consumable with possible cooldown

		}
		if (type == 2) {	//Item is a passive item that cannot be interacted with
			cooldown = 0;
			cdi = 0;
			amount = 0;
		}
	}
	void getLevel(){
		
		level = ls.getlevel ();
		StatChange = StatChange * ((float)level+1);
	}

}
