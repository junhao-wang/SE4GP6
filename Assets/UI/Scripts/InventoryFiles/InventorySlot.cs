using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//This class states all the UI elements of a Inventory Slot and it's item information. Button items wll be added
//Part of the Darkest Dungeon scripts
public class InventorySlot : BaseSlot//, IDropHandler
{
    [SerializeField]
    private Image overlayIcon;                                                          //The the item image that is shown
	//private Image display = GetComponent<Image>();
    public InventoryItem SlotItem { get; private set; }                                 //The information about the desired item
    public bool InteractionDisabled { private get; set; }                               //states if user can interact with the slot
	public Image OverlayIcon { get { return overlayIcon; } set { overlayIcon = value; } }//set { overlayIcon = Resources.Load<Image>("Icons/"+ InventoryManager.Data.Items["passive"]["1"].name);}}//
    public bool HasItem { get { return SlotItem.IsNotEmpty; } }                         //checks if this slot is empty or not
    //TODO implement the actions below
    public event Action<InventorySlot, InventoryItem> EventDropOut;
    public event Action<InventorySlot, InventoryItem> EventDropIn;
    public event Action<InventorySlot, InventoryItem> EventSwap;
    public event Action<InventorySlot> EventActivate;
    //initalizes the empty slot
    public void Initialize()
    {
		
        SlotItem = GetComponentInChildren<InventoryItem>();
        SlotItem.Initialize(this);
		//display = OverlayIcon;
    }

    public bool HasFreeSpaceForItem(ItemDefinition item)
    {
        return SlotItem.HasFreeSpaceForItem(item);
    }

    public void CreateItem(string itemType, string itemId, int amount)
    {
        SlotItem.gameObject.SetActive(true);
        SlotItem.Create(itemType, itemId, amount);
        
    }
    //fills the slot with a passive item
	public void CreateItem(PassiveItem passive)
    {
        SlotItem.gameObject.SetActive(true);
        SlotItem.Create(passive);
        
    }

    public void CreateItem(InventorySlotData slotData)
    {
        SlotItem.Create(slotData);
       
    }

    public void DeleteItem()
    {
        SlotItem.gameObject.SetActive(false);
        SlotItem.Delete();
    }

    public void SlotActivated()
    {
        if (EventActivate != null)
            EventActivate(this);
    }

    public void ItemDroppedIn(InventoryItem itemDroppedIn)
    {
        if (EventDropIn != null)
            EventDropIn(this, itemDroppedIn);
    }

    public void ItemDroppedOut(InventoryItem itemDroppedOut)
    {
        if (EventDropOut != null)
            EventDropOut(this, itemDroppedOut);
    }

    public void ItemSwapped(InventoryItem incomingItem)
    {
        if (EventSwap != null)
            EventSwap(this, incomingItem);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (InteractionDisabled)
            return;
        
    }

    private void CheckDrop(InventoryItem movingItem)
    {
        InventorySlot fromSlot = movingItem.Slot;

		if (movingItem.Slot != null)
			RepositionItem (movingItem);
        
    }

    private void RepositionItem(InventoryItem slotItem)
    {
        InventorySlot toSlot = this;
        InventorySlot fromSlot = slotItem.Slot;

        InventoryItem fromSlotItem = fromSlot.SlotItem;
        InventoryItem toSlotItem = toSlot.SlotItem;

        toSlot.SlotItem = fromSlotItem;
        fromSlotItem.Slot = toSlot;
        fromSlotItem.RectTransform.SetParent(toSlot.RectTransform, false);
        fromSlotItem.RectTransform.SetAsFirstSibling();
        toSlot.ItemDroppedIn(fromSlotItem);

        fromSlot.SlotItem = toSlotItem;
        toSlotItem.Slot = fromSlot;
        toSlotItem.RectTransform.SetParent(fromSlot.RectTransform, false);
        toSlotItem.RectTransform.SetAsFirstSibling();
        fromSlot.ItemDroppedOut(fromSlotItem);
    }

    private void SwapItems(InventoryItem slotItem)
    {
        InventorySlot toSlot = this;
        InventorySlot fromSlot = slotItem.Slot;

        InventoryItem fromSlotItem = fromSlot.SlotItem;
        InventoryItem toSlotItem = toSlot.SlotItem;

        toSlot.SlotItem = fromSlotItem;
        fromSlotItem.Slot = toSlot;
        fromSlotItem.RectTransform.SetParent(toSlot.RectTransform, false);
        fromSlotItem.RectTransform.SetAsFirstSibling();
        toSlot.ItemSwapped(fromSlotItem);

        fromSlot.SlotItem = toSlotItem;
        toSlotItem.Slot = fromSlot;
        toSlotItem.RectTransform.SetParent(fromSlot.RectTransform, false);
        toSlotItem.RectTransform.SetAsFirstSibling();
        fromSlot.ItemSwapped(toSlotItem);
    }

    private void MergeItems(InventoryItem slotItem)
    {
        SlotItem.MergeItems(slotItem);
    }
}