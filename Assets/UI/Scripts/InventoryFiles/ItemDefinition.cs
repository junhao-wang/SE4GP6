//This class defines information about all kinds of times, will be replaced soon by placing into InfoItems
//from Darkest Dungeon Scripts
public class ItemDefinition
{
    public string Type { get; set; }                            //the type of item
    public string Id { get; set; }                              //the iD of the item
    public int Amount { get; set; }                             //the amout of the item

    public ItemDefinition()
    {
    }

    public ItemDefinition(string type, string id, int amount)
    {
        Type = type;
        Id = id;
        Amount = amount;
    }

    public bool IsSameItem(ItemDefinition compareItem)
    {
        return Type == compareItem.Type && Id == compareItem.Id;
    }
}
