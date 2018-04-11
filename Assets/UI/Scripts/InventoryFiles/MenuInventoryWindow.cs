using System;
using UnityEngine;
using System.Collections.Generic;
//This script creats a new inventory window that is the basis for all of the UI elements in the Inventory Window.
//Part of the Darkest Dungeon Scripts
public class MenuInventoryWindow : MonoBehaviour//, IInventory
{
    [SerializeField]
    private RectTransform ItemBox;                                              //The panel where the items are generated
    [SerializeField]
    private InventoryRow rowTemplate;                                           //The template that is used when creating another row

    private MenuInventory MenuInventory { get; set; }                           //The instance of the inventory menu
    private List<InventorySlot> InventorySlots { get { return ItemSlots; } }    //The list of inventory slots in the inventory
    private List<InventoryRow> InventoryRows { get; set; }                      //The list of inventory rows in the inventory
    private int CurrentCapacity { get; set; }                                   //The current capacity of items in the inventory
    private int MaxCapacity { get; set; }                                       //The max number of times that can be in the inventory

    private readonly List<InventorySlot> ItemSlots = new List<InventorySlot>(); //The readonly of the item slots

    public event Action EventWindowClosed;
    public GameObject inventoryManager;

    private void Awake()
    {
		
        //DragManager.Instanse.EventStartDraggingInventorySlot += DragManagerStartDraggingInventorySlot;
        //DragManager.Instanse.EventEndDraggingInventorySlot += DragManagerEndDraggingInventorySlot;
    }

    private void OnDestroy()
    {
       // DragManager.Instanse.EventStartDraggingInventorySlot -= DragManagerStartDraggingInventorySlot;
       // DragManager.Instanse.EventEndDraggingInventorySlot -= DragManagerEndDraggingInventorySlot;
    }

    
    //This method is reponsible for adding all of the items to the inventory from the inventorymanager
    public void Populate()
    {
        MenuInventory = InventoryManager.MenuInventory;
        
        InventoryRows = new List<InventoryRow>();

		int ItemCount = MenuInventory.Passives.Count;
        int rowCount = Mathf.Max(3, ItemCount / 7 + 1);
        int ItemsLoaded = 0;

        CurrentCapacity = ItemCount;
        MaxCapacity = rowCount * 7;

        for (int i = 0; i < rowCount; i++)
        {
            InventoryRow newRow = Instantiate(rowTemplate);
            newRow.Initialize(this);
            newRow.EventRowEmptied += MenuInventoryRowEmptied;
            newRow.RectTransform.SetParent(ItemBox, false);
            InventoryRows.Add(newRow);
            InventorySlots.AddRange(newRow.InventorySlots);

            for (int j = 0; j < 7; j++)
            {
                if (ItemsLoaded != ItemCount)
                {
					Debug.Log ("Place passive: "+ MenuInventory.Passives[ItemsLoaded].name);
					PassiveItem passive = MenuInventory.Passives[ItemsLoaded];
                   	newRow.InventorySlots[j].CreateItem(passive);
                    newRow.ItemAdded();
                   	ItemsLoaded++;
                }
                else
                    break;
            }
        }

        for (int i = 0; i < InventoryRows.Count; i++)
        {
            InventoryRows[i].RowNumber = i + 1;
            InventoryRows[i].EventRowEmptied += MenuInventoryRowEmptied;
            for (int j = 0; j < InventoryRows[i].SlotCount; j++)
            {
                InventoryRows[i].InventorySlots[j].EventDropIn += MenuInventorySlotDropIn;
                InventoryRows[i].InventorySlots[j].EventDropOut += MenuInventorySlotDropOut;
            }
        }
    }
    //Adds a passive item to the inventory
    public void AddPassive(PassiveItem passive)
    {
        if(CurrentCapacity < MaxCapacity)
        {
            var freeRow = InventoryRows.Find(row => row.HasEmptySlot);
            var emptySlot = freeRow.InventorySlots.Find(slot => slot.HasItem == false);
            freeRow.ItemAdded();
            emptySlot.CreateItem(passive);
            CurrentCapacity++;
			MenuInventory.Passives.Add(passive);
        }
        else
        {
            var newRow = AddRow();
            newRow.InventorySlots[0].CreateItem(passive);
            newRow.ItemAdded();
            CurrentCapacity++;
			MenuInventory.Passives.Add(passive);
        }

        if (CurrentCapacity == MaxCapacity)
            AddRow();
    }

    public void WindowClosed()
    {        
        if (EventWindowClosed != null)
            EventWindowClosed();
    }
    //Todo check to see if inventory space is filled
    public bool CheckSingleInventorySpace(ItemDefinition item)
    {
        return false;
    }

    private void DragManagerEndDraggingInventorySlot(InventorySlot slot)
    {
        if (gameObject.activeSelf)
        {
            for (int i = 0; i < InventorySlots.Count; i++)
            {
                //InventorySlots[i].SetActiveState(true);
            }
        }
    }

    private void DragManagerStartDraggingInventorySlot(InventorySlot slot)
    {
		//BASICALLY UPDATE THE INVENTORY UI AS THOUGH THE ITEM HAS BEEN REMOVED FROM IT
        if (gameObject.activeSelf)
        {
			foreach (InventorySlot menuSlot in InventorySlots)
            {
				if (!menuSlot.HasItem)
					continue;
            }
        }
    }

    private void MenuInventorySlotDropOut(InventorySlot slot, InventoryItem itemDrop)
    {
        CurrentCapacity--;
        //InventoryManager.MenuInventory.Passives.Remove(itemDrop.ItemInfo as Passive);
    }

    private void MenuInventorySlotDropIn(InventorySlot slot, InventoryItem itemDrop)
    {
        CurrentCapacity++;
        //InventoryManager.MenuInventory.Passives.Add(itemDrop.ItemInfo as Passive);
        if (CurrentCapacity == MaxCapacity)
            AddRow();
    }
    //removes an invetory row if there is less than 21 items to remove scrolling ability
    private void MenuInventoryRowEmptied(int rowNumber)
    {
        if (rowNumber > 3 && rowNumber == InventoryRows.Count)
        {
            for (int i = rowNumber; i > 3; i--)
            {
                if (InventoryRows[i - 1].HasItems)
                    break;

                InventorySlots.RemoveAll(slot => InventoryRows[i - 1].InventorySlots.Contains(slot));
                Destroy(InventoryRows[i - 1].gameObject);
                MaxCapacity -= InventoryRows[i - 1].SlotCount;
                InventoryRows.RemoveAt(i - 1);
            }
        }
    }
    //adds a row to the inventory with the information initalized
    private InventoryRow AddRow()
    {
        InventoryRow newRow = Instantiate(rowTemplate);
        newRow.Initialize(this);
        newRow.RectTransform.SetParent(ItemBox, false);
        InventoryRows.Add(newRow);
        InventorySlots.AddRange(newRow.InventorySlots);

        newRow.RowNumber = InventoryRows.Count;
        MaxCapacity += newRow.SlotCount;
        newRow.EventRowEmptied += MenuInventoryRowEmptied;

        for (int j = 0; j < newRow.SlotCount; j++)
        {
            newRow.InventorySlots[j].EventDropIn += MenuInventorySlotDropIn;
            newRow.InventorySlots[j].EventDropOut += MenuInventorySlotDropOut;
        }
        return newRow;
    }
}
