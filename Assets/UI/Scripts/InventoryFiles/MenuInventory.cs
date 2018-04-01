using System;
using System.Collections.Generic;

public class MenuInventory
{
    public int MaxCapacity { get; set; }

    public List<PassiveItem>  Passives{ get; set; }

    public Action<PassiveItem> PassiveAddAction { get; set; }

    public void AddPassiveItem(PassiveItem passive)
    {
        if (PassiveAddAction != null)
            PassiveAddAction(passive);
        else
            Passives.Add(passive);
    }

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