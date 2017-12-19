using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayItemInfo : MonoBehaviour {

	public GameObject item;								//States the item that is being referenced
	public GameObject panel;							//This is the item panel that to display the item
	//public Text desc;									//This is the text section that is filled with item descriptions
	// Use this for initialization	
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//	Sets the item that is selected
	void setItem(GameObject selection){
		item = selection;
	}
	//	makes a copy of the formatted image and brings it over to the item panel to be displayed
	//	The text is also displayed
	//	TODO if the item is deselected, then clear the information in the panel
	/*void displayItem(){
		RectTransform t = panel.GetComponent<RectTransform>();
		GameObject clone = new GameObject ();
		clone.Instantiate (item, t.transform.up);
		ItemInfo s = item.GetComponents<ItemInfo> ();
	}*/
}
