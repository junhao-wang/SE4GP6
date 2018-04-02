using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//Script for creating a new inventory item 
//Part of the Darkest Dungeon scripts
public class InventoryItem : MonoBehaviour//, IPointerEnterHandler, IPointerExitHandler, 
    //IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IPointerClickHandler
{
    [SerializeField]
    private Image itemIcon;
    [SerializeField]
    private Image rarityIcon;
    [SerializeField]
    private Text amountText;

    public string ItemType { get { return Item.Type; } }
    public int Amount { get { return Item.Amount; } }
    public bool IsNotEmpty { get { return !isEmpty; } }
    public bool IsFull { get { return Amount >= 0; } }
    public bool Deactivated { get; private set; }
    public InventorySlot Slot { get; set; }
    public ItemDefinition Item { get; private set; }
    public ItemInfo ItemInfo { get; private set; }
    public RectTransform RectTransform { get; private set; }


    
    private bool isEmpty;
    private bool isUnavailable;

#if UNITY_ANDROID || UNITY_IOS
    float doubleTapTimer = 0f;
    float doubleTapTime = 0.2f;

    private void Update()
    {
        if (doubleTapTimer > 0)
            doubleTapTimer -= Time.deltaTime;
    }
#endif

    public bool IsSameItem(ItemDefinition compareItem)
    {
        return !isEmpty && Item.IsSameItem(compareItem);
    }

    public bool HasFreeSpaceForItem(ItemDefinition compareItem)
    {
        if (isEmpty)
            return false;

		if (Item.IsSameItem (compareItem))
			return Item.Amount < 0;

        return false;
    }
    //creats a new empty inventory item
    public void Initialize(InventorySlot inventorySlot)
    {
        isEmpty = true;
        //Item = new ItemDefinition("", "", 0);
		Item = new ItemDefinition("passive", "1", 0);
        Slot = inventorySlot;
        RectTransform = GetComponent<RectTransform>();
        ItemInfo= null;
        gameObject.SetActive(false);
    }

    public void Create(string itemType, string itemId, int amount)
    {
        isEmpty = false;
        Item.Type = itemType;
        Item.Id = itemId;
        Item.Amount = amount;
        LoadItem();
    }
    //creates a new passive item
    public void Create(PassiveItem passive)
    {
        isEmpty = false;
		Item.Type = "passive";//passive.type;
        Item.Id = passive.id;
        Item.Amount = 1;
        LoadPassiveItem(passive);
    }

    public void Create(InventorySlotData slotData)
    {
        //isEmpty = slotData.ItemInfo == null;
        if (isEmpty)
            Delete();
        else
        {
            Item.Type = slotData.Item.Type;
            Item.Id = slotData.Item.Id;
            Item.Amount = slotData.Item.Amount;
			Slot.OverlayIcon.sprite = Resources.Load<Sprite>("Icons/"+ InventoryManager.Data.Items[Item.Type][Item.Id].name);
            LoadItem();
        }
    }
    //removes the item by setting it to empty
    public void Delete()
    {

        isEmpty = true;

        Item.Type = "";
        Item.Id = "";
        Item.Amount = 0;

        ItemInfo = null;

        gameObject.SetActive(false);
    }

    public int AddItems(int addAmount)
    {
        if (addAmount == 0)
            return 0;
        if (Amount == 0)
            return addAmount;

        int itemsLeft;
        if (Item.Amount + addAmount > 0)
        {
            itemsLeft = addAmount - (0 - Item.Amount);
			Item.Amount = 0;
        }
        else
        {
            itemsLeft = 0;
            Item.Amount += addAmount;
        }

        UpdateAmount();

        return itemsLeft;
    }

    public void RemoveItems(int remAmount)
    {
        if (remAmount > Item.Amount)
            Item.Amount = 0;
        else
            Item.Amount -= remAmount;

        if (Item.Amount == 0)
            Delete();
        else
            UpdateAmount();
    }

    public void MergeItems(InventoryItem itemSource)
    {
		int stackLimit = 0;

        int transferAmount = Mathf.Min(stackLimit - Amount, itemSource.Item.Amount);

        Item.Amount += transferAmount;
        UpdateAmount();

        itemSource.Item.Amount -= transferAmount;
        if (itemSource.Item.Amount == 0)
            itemSource.Delete();
        else
            itemSource.UpdateAmount();
    }

    public void SetDropUnavailable()
    {
        if (Slot.OverlayIcon != null)
        {
            isUnavailable = true;
            Slot.OverlayIcon.enabled = true;
            //Slot.OverlayIcon.sprite = InvetoryManager.Data.Sprites["eqp_unavailable_mouseover"];
        }
    }

    public void SetOverlayDefault()
    {
        if (Slot.OverlayIcon != null)
        {
            isUnavailable = false;
            Slot.OverlayIcon.enabled = false;
        }
    }

    private void LoadItem()
    {
        //ItemInfo =InventoryManager.Data.Items[Item.type][Item.id];
		ItemInfo = InventoryManager.Data.Items["trinket"]["1"];
        if (Item.Type == "passive")
        {
            LoadPassiveItem(ItemInfo as PassiveItem);
            return;
        }
        UpdateAmount();
        gameObject.SetActive(true);
    }

    private void LoadPassiveItem(PassiveItem passive)
    {
        ItemInfo = passive;

        //itemIcon.sprite = InvetoryManager.Data.Sprites["inv_" + Item.Type + "+" + Item.Id]
		Debug.Log ("Before Load Sprite");
		Debug.Log ("Desired sprite is: " + InventoryManager.Data.Items[passive.type][passive.itemID].name);
		itemIcon = passive.image;
		//itemIcon.sprite = passive.image.sprite;
        //rarityIcon.sprite = InventoryManager.Data.Sprites["rarity_" + trinket.RarityId];

        UpdateAmount();

        gameObject.SetActive(true);
    }

    private void UpdateAmount()
    {
        //itemIcon.sprite = InventoryManager.Data.Sprites["inv_" + Item.Type + "+" + Item.Id + "_" + thresholdIndex];
    }
}