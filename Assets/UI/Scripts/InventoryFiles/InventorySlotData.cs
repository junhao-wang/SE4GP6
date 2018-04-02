//This class instantiates the data that needed for the inventoryslots without needing to refer to the item itself
//Part of the Darkest Dungeon scripts
public class InventorySlotData
{
    public ItemDefinition Item { get; set; }                            //the itemdefinintioninfo it is refering to
    public ItemInfo ItemInfo { get; set; }                              //the item info it is refering to

    public InventorySlotData()
    {
    }

    public InventorySlotData(ItemDefinition itemDefinition)
    {
        Item = itemDefinition;

       // if (InventoryManager.Data.ItemExists(Item))
           // ItemInfo = InventoryManager.Data.Items[itemDefinition.Type][itemDefinition.Id];
    }
}
