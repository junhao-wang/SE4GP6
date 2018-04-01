using System.Collections.Generic;
using System.Text;
using System.Collections;
using UnityEngine;

public class PassiveItem : ItemInfo {


	public string id { get; set; }
	//public string Rarity { get;  set; }
	//public List<Buff> Buffs { get; private set; }
	public PassiveItem():base("0", "passiveItem","passive", "This is a passive Item", new int[]{0,0,0,0,0,0,0,0})
	{
		//Buffs = new List<Buff>();
		//ClassRequirements = new List<string>();
		getLevel();
	}
	// Retrieves the value of the item's assigned level
	void getLevel(){
		//level = ls.getlevel ();	
		this.level = Random.Range (0, 4);
		for (int i = 0; i < stats.Length; i++) {
			this.stats[i] = this.stats[i] * (level + 1);
		}
	}
}
