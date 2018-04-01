using System;
using UnityEngine;
using System.Collections.Generic;

public class MenuInventoryWindow : MonoBehaviour//, IInventory
{
    [SerializeField]
    private RectTransform ItemBox;
    [SerializeField]
    private InventoryRow rowTemplate;

    private MenuInventory MenuInventory { get; set; }
    private List<InventorySlot> InventorySlots { get { return ItemSlots; } }
    private List<InventoryRow> InventoryRows { get; set; }
    private int CurrentCapacity { get; set; }
    private int MaxCapacity { get; set; }

    private readonly List<InventorySlot> ItemSlots = new List<InventorySlot>();

    public event Action EventWindowClosed;

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
