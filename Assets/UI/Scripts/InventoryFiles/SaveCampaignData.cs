using System.Collections.Generic;

public class SaveCampaignData
{
    public int SaveId;

    public List<string> InventoryPassives;

    public List<InventorySlotData> InventoryItems { get; set; }

    public SaveCampaignData()
    {
        InventoryPassives = new List<string>();
        InventoryItems = new List<InventorySlotData>();
    }

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