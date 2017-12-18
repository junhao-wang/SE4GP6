using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemControl : MonoBehaviour {

	public GameObject itemPanel;
	public GameObject item;
	private ItemLayout layout;
	void Start(){
		layout = itemPanel.GetComponent<ItemLayout> ();
	}
	void Update(){
		//itemPanel.GetComponentsInChildren<
		Button b = item.GetComponent<Button> ();
		InputControl ();
	}
	void InputControl(){
		bool trigger = false;
		if (Input.GetKeyDown (KeyCode.A)) {
			Add (item);
			trigger = true;
		}
		if (Input.GetKeyDown (KeyCode.S)) {
			Create (item);
			trigger = true;
		}
		if (Input.GetKeyDown (KeyCode.D)) {
			Hide (item);
			trigger = true;
		}
		if (Input.GetKeyDown (KeyCode.F)) {
			Remove (item);
			trigger = true;
		}
		if (trigger) {
			layout.formatItems ();
		}
	}
	//Unhides the item and adds it back into the pool
	void Add(GameObject b){
		b.SetActive (true);
	}
	//Creates a new item and adds it to the item pool
	void Create(GameObject b){
		//Created new Item
		Instantiate(b);
	}
	//Removes the item from the pool via equiping
	void Hide(GameObject b){
		b.SetActive(false);
		//run the equiping function that equips the item
	}
	//Destroys the item via selling or consumption
	void Remove(GameObject b){
		Destroy (b);
	}
}
