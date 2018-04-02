using System.Collections.Generic;
using System.Text;
using System.Collections;
using UnityEngine;

public class ConsumableItem : ItemInfo {


	public string id { get; set; }
	public int amount{ get; private set; }				// The number of uses for the item
	public bool forAllies{get; private set;}				// Decides if the item is used on allies (0) or enemies(1);
	public GenericUnit target{ get; set; }						//	The assigned unit
	//public string Rarity { get;  set; }
	//public List<Buff> Buffs { get; private set; }
	public ConsumableItem(int amount,bool forAllies):base("0", "consumableItem","consumable", "This is a consumable Item", new int[]{0,0,0,0,0,0,0,0})
	{
		//Buffs = new List<Buff>();
		//ClassRequirements = new List<string>();
		this.amount = amount;
		this.forAllies = forAllies;
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
	public void assignTarget(GenericUnit target){
		this.target = target;
	}
}
