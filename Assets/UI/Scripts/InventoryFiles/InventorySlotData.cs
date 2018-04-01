public class InventorySlotData
{
    public ItemDefinition Item { get; set; }
    public ItemData ItemInfo { get; set; }

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
