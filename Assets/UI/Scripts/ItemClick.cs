using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemClick : MonoBehaviour {

	private StatDisplay statDisplay;
	public ItemInfo item ;
	public GameObject infoPanel ;
	public InventoryItem inventoryItem{get;set;}

	void Start()
    {
    	Button b = GetComponent<Button>();
    	infoPanel = GameObject.Find("InfoPanel");
    	StatDisplay statDisplay = infoPanel.GetComponent<StatDisplay>();
        b.onClick.AddListener(TaskOnClick);
        inventoryItem = GetComponent<InventoryItem>();
        item = inventoryItem.ItemInfo;
    }

    void TaskOnClick()
    {
    	Debug.Log("You have clicked the button!");
    	infoPanel = GameObject.Find("InfoPanel");
    	StatDisplay statDisplay = infoPanel.GetComponent<StatDisplay>();
    	statDisplay.setPassiveItem(item);
    	statDisplay.setSprite(inventoryItem.itemIcon);
    }
}
