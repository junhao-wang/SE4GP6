using System.Collections;
using System.Collections.Generic;
//using UnityEngine;
//This class is responsible for instantiating the equiped inventory for the player's campaign. right now the inventory is hard coded
public class EquipedInventory {

	public List<PassiveItem>  LeePassives{ get; set; }						//these are the passive items for the the units
	public List<PassiveItem>  KronerPassives{ get; set; }
	public List<PassiveItem>  AlexeiPassives{ get; set; }
	public List<ConsumableItem>  LeeConsumables{ get; set; }				//these are the consumable items for the units
	public List<ConsumableItem>  KronerConsumables{ get; set; }
	public List<ConsumableItem>  AlexeiConsumables{ get; set; }
	//seciton for adding consumable items
	public void AddConsumableItem(ConsumableItem consumable,string Name)
	{
		if(Name.Equals("Lee"))
			LeeConsumables.Add(consumable);
		if(Name.Equals("Kroner"))
			KronerConsumables.Add(consumable);
		if(Name.Equals("Alexei"))
			AlexeiConsumables.Add(consumable);
	}
	public void RemoveConsumableItem(ConsumableItem consumable,string Name)
	{
		if(Name.Equals("Lee"))
			LeeConsumables.Remove(consumable);
		if(Name.Equals("Kroner"))
			KronerConsumables.Remove(consumable);
		if(Name.Equals("Alexei"))
			AlexeiConsumables.Remove(consumable);
	}

	//section for adding passiveitems 
	public void AddPassiveItem(PassiveItem passive,string Name)
	{
		if(Name.Equals("Lee"))
			LeePassives.Add(passive);
		if(Name.Equals("Kroner"))
			KronerPassives.Add(passive);
		if(Name.Equals("Alexei"))
			AlexeiPassives.Add(passive);
	}
	public void RemovePassiveItem(PassiveItem passive,string Name)
	{
		if(Name.Equals("Lee"))
			LeePassives.Remove(passive);
		if(Name.Equals("Kroner"))
			KronerPassives.Remove(passive);
		if(Name.Equals("Alexei"))
			AlexeiPassives.Remove(passive);
	}
	// Will add the basic inventory layout for the characters
	public EquipedInventory()
	{
		PassiveItem defaultItem = (PassiveItem)InventoryManager.Data.Items["passive"]["1"];
		ConsumableItem healthkit = (ConsumableItem)InventoryManager.Data.Items["consumable"]["1"];
		ConsumableItem grenade = (ConsumableItem)InventoryManager.Data.Items["consumable"]["2"];


		//Lee's sections
		LeePassives = new List<PassiveItem>();
		LeeConsumables = new List<ConsumableItem>();
		AddConsumableItem(healthkit,"Lee");

		//Kroner's section
		KronerPassives = new List<PassiveItem>();
		KronerConsumables = new List<ConsumableItem>();
		AddPassiveItem(defaultItem,"Kroner");
		AddConsumableItem(grenade,"Kroner");
		//Debug.Log ("THE GOD DAMN GRENADE VALUE IS: " + grenade.amount);

		//Alexei's section
		AlexeiPassives = new List<PassiveItem>();
		AlexeiConsumables = new List<ConsumableItem>();
		AddPassiveItem(defaultItem,"Alexei");
		AddConsumableItem(grenade,"Alexei");
	}
}
