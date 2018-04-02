using System.Collections.Generic;
using System.Text;
using System.Collections;
using UnityEngine;
//This class is an extention of the ItemInfo which represents all the items that apply a passive value change to the character.
//Part of the Darkest's Dungeon scripts
public class PassiveItem : ItemInfo {


	public string id { get; set; }		//represents the specific initalization of the passiveItem
	//creats an empty base Passive item that is filled promptly
	public PassiveItem():base("0", "passiveItem","passive", "This is a passive Item", new int[]{0,0,0,0,0,0,0,0})
	{
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
