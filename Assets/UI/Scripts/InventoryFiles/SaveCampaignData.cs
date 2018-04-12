using System.Collections.Generic;
//This class represents the saved item data for the specific save file, not implemented 100%
//Part of the DarkestDugeon Scripts
public class SaveCampaignData
{
    public int SaveId;                                              //The specific Id used

    public List<string> InventoryPassives;                          //The list of PassiveItems

    public List<InventorySlotData> InventoryItems { get; set; }     //List of inventoryItems
    //Makes a new empty SaveCampaignData
    public SaveCampaignData()
    {
        InventoryPassives = new List<string>();
        InventoryItems = new List<InventorySlotData>();
    }
    //Constructors not yet implemented
    public SaveCampaignData(int newSaveId) : this()
    {
        SaveId = newSaveId;
    }

    public SaveCampaignData(int newSaveId, string newMenuName) : this()
    {
        SaveId = newSaveId;
    }
	//InventoryItems = new List<InventorySlotData>();
        
    public void UpdateFromMenu()
    {
        
        InventoryPassives.Clear();
        //for (int i = 0; i < MenuInventory.Passives.Count; i++)
            //InventoryPassives.Add(MenuInventory.Passives[i].itemID);
        
    }
}