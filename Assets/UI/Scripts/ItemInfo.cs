using System.Text;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//This class states the type of item and what values that it passes to the player, will be used for item prefabs//
public class ItemInfo {

	public string itemID { get; set; } 						// Item ID
	public string name { get; set; }			// Name of the item
	public string type{ get; set; }				// What type the item is
	public string description{ get; set; }		// A Description of the item, lore can be included
	public int level{ get; set;}						// The level of the item, retrived from LevelInfo
	public int[] stats{get; set;}				// The list of stat changes and money the item has, in form of:
														// {HP,AR,ATT,GA,SP,RG,MV,money}
	public Image image{get;set;}						// The image of the specific item
	//The information remains here to ensure that no information is lost during transition periods
//	public int activePeriod{ get; private set; }		// The time that the item is in effect
//	public int cooldown{ get; private set; }			// The max cooldown of the specific item
//	public int cdi{ get; private set;}				//	cooldown index
//	public int amount{ get; private set; }				// The number of uses for the item
//	private LevelInfo ls{ get; set;}			//	The LevelInfo script used for or level value 
	public Unit character{ get; set; }					//	The assigned unit
	//public List<Buff> buffs;							// The list of buffs applied from the item


	// Use this for initialization, it is the pipeline for the item creation.
	public ItemInfo(string itemID, string name, string type, string description, int[] stats) {
		//ls = Instantiate (gameObject).GetComponentInChildren <LevelInfo>();
		//ls = gameObject.GetComponentInChildren <LevelInfo>();
		this.itemID = itemID;
		this.name = name;
		this.type = type;
		this.description = description;
		this.stats = stats;
		//getLevel ();
		//setType ();
		//createBuff ();
	}
	// Moved the cooldown index down by one, if it reaches zero, then the timer is reset
//	void cooldowntick(){
//		cdi--;
//		if (cdi == 0) {
//			cdi = cooldown;
//		}
//	}
//	// Automatically sets the type of the item if the developer accidentally added uneccisarry values
//	void setType(){
//		if (type == 0) {	//Item is a one time consumable that is removed
//			cooldown = 0;
//			cdi = 0;
//			amount = 0;
//		}
//		if (type == 1) {	//Item is a reusable consumable with possible cooldown
//
//		}
//		if (type == 2) {	//Item is a passive item that cannot be interacted with
//			cooldown = 0;
//			cdi = 0;
//			amount = 0;
//		}
//	}
	// Retrieves the value of the item's assigned level
	void getLevel(){
		//level = ls.getlevel ();	
		this.level = Random.Range (0, 4);
		for (int i = 0; i < stats.Length; i++) {
			this.stats[i] = this.stats[i] * (level + 1);
		}
	}
	//Set character
	// TODO have it so that if the item is not assigned to a character, it does not assign to anything
	void setCharacter(){
		//sets the new characte if the item is not applied to anything
	}
	//Applies the item changes to the assigned character
	void applyBuff(){
		//foreach (Buff b in buffs) {
			//b.Apply (character);
		//}
	}
	//Removes the item properties to the assigned character.
	void removeBuff(){
		//foreach(Buff b in buffs){
			//b.Undo (character);
		//}
	}
	//This takes the item values and translates them into a list of buffs.
    /*
	void createBuff(){
		List<Buff> temp = new List<Buff>();
		//Passive item buff creations
		if (type == 2) {
			if (HP != 0) {
				temp.Add (new HealingBuff (1, (int)HP));
			}
			if (AR != 0) {
				//TODO create an attack range buff to increase attack range;
			}
			if (AF != 0) {
				temp.Add (new AttackBuff (-1, (int)AF));
			}
			if (DF != 0) {
				temp.Add (new DefenceBuff (-1, (int)DF));
			}
			if (SP != 0) {
				//TODO create an speed buff to increase attack range;
			}
			if (MV != 0) {
				//TODO create an movementpoints buff to increase attack range;
			}
		}
		//Consumable items
		//TODO fix the Consumables by changing them to our version of the stats, not the built in stats
		if (type < 2) {
			if (HP != 0) {
				temp.Add (new HealingBuff (activePeriod, (int)HP));
			}
			if (AR != 0) {
				//TODO create an attack range buff to increase attack range;
			}
<<<<<<< HEAD
				temp.Add (new AttackBuff (activePeriod, AF));
=======
			if (AF != 0) {
				temp.Add (new AttackBuff (activePeriod, (int)AF));
>>>>>>> 381173cdf8c0b7cc6dfabeac94c326eef8d1ea55
			}
			if (DF != 0) {
				temp.Add (new DefenceBuff (activePeriod, (int)DF));
			}
			if (SP != 0) {
				//TODO create an speed buff to increase attack range;
			}
			if (MV != 0) {
				//TODO create an movementpoints buff to increase attack range;
			}
		}
		//return temp buffs to the buffs array to be applied
		buffs = temp;
	}*/
}