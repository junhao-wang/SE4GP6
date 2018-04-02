using System;
using System.Collections.Generic;
//This class acts as a instance for the list of current items in player's inventory
//Part of the Darkest Dungeons scripts
public class MenuInventory
{
    public int MaxCapacity { get; set; }                        //Max number of items allowed

    public List<PassiveItem>  Passives{ get; set; }             //list of passive Items

    public Action<PassiveItem> PassiveAddAction { get; set; }   //actions that can be performed with the passives
    //adds passive item to the item list
    public void AddPassiveItem(PassiveItem passive)
    {
        if (PassiveAddAction != null)
            PassiveAddAction(passive);
        else
            Passives.Add(passive);
    }
    //TODO this method is reponsible for loading the items from a save game file that fills the list, currently has a single item
    public MenuInventory(SaveCampaignData saveData)
    {
        Passives = new List<PassiveItem>();
		for(int i = 0; i < 8; i++)
			Passives.Add((PassiveItem)InventoryManager.Data.Items["passive"]["1"]);
		//Orignal savegame code
        //for(int i = 0; i < saveData.InventoryPassives.Count; i++)
           // Passives.Add((Passive)InventoryManager.Data.Items["passive"][saveData.InventoryPassives[i]]);
    }
}