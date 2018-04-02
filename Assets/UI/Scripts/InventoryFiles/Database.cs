using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using JsonClasses;
using SimpleJSON;

public class Database : MonoBehaviour {

	private const string PassiveItemDirectory = "Data/Items/Passive";
	private const string ConsumableItemDirectory = "Data/Items/Consumable";
	private const string ItemImageDirectory = "Icons";
	public Dictionary<string, Dictionary<string, ItemInfo>> Items { get; private set; }

	public void Load(){
		Items = new Dictionary<string, Dictionary<string, ItemInfo>>();
		LoadJsonPassives ();
		LoadJsonConsumables ();
//		foreach (KeyValuePair<string,Dictionary<string,ItemInfo>> item in Items) {
//			Debug.Log ("Reading Item:");
//			Debug.Log (item.Key);
//		}
	}

	private void LoadJsonConsumables()
	{
		

		if (!Items.ContainsKey("consumable"))
			Items.Add("consumable", new Dictionary<string, ItemInfo>());
		//Debug.Log ("Before Json Read");
		foreach(var consumable in GetJsonConsumablesLibrary())
		{
			//Debug.Log ("Before keys of type :"+ consumable.type + " are read");
			if (Items[consumable.type].ContainsKey(consumable.itemID))
				Debug.Log("Same consumable: " + consumable.itemID);
			else
				Items[consumable.type].Add(consumable.itemID, consumable);
		}
	}
	private List<ConsumableItem> GetJsonConsumablesLibrary()
	{
		TextAsset jsonText = Resources.Load<TextAsset>(ConsumableItemDirectory);
		Debug.Log (jsonText);
		JSONNode consumableDatabase = JSONNode.Parse(jsonText.text);
		List<ConsumableItem> consumables = new List<ConsumableItem>();

		//for (int i = 0; i < consumableDatabase.Count; i++)
		for (int i = 0; i < 2; i++)
		{
			
			ConsumableItem consumable = new ConsumableItem (int.Parse(consumableDatabase["consumable"][i][5].Value),
																consumableDatabase["consumable"][i][6].Value=="true");
			//Debug.Log("Consumable id is3: " + consumableDatabase["consumable"][0][1].Value);
			consumable.itemID = consumableDatabase["consumable"][i][0].Value;
			//Debug.Log("Consumable id is: " + consumable.itemID);
			consumable.name = consumableDatabase["consumable"][i][1].Value;
			consumable.type = consumableDatabase["consumable"][i][2].Value;
			consumable.description= consumableDatabase["consumable"][i][3].Value;
			int[] statVals = new int[8];
			for(int j=0;j<8;j++){
				statVals[j] = int.Parse(consumableDatabase["consumable"][i][4][j].Value);
			}
			consumable.stats = statVals;
			Image image = Resources.Load<Image> (ItemImageDirectory+consumable.name);
			consumable.image = image;
			consumables.Add(consumable);
			Debug.Log("Consumable id is: " + consumable.name);
		}
		return consumables;
	}
	private void LoadJsonPassives()
	{
		

		if (!Items.ContainsKey("passive"))
			Items.Add("passive", new Dictionary<string, ItemInfo>());
		//Debug.Log ("Before Json Read");
		foreach(var passive in GetJsonPassiveLibrary())
		{
			//Debug.Log ("Before keys of type :"+ passive.type + " are read");
			if (Items[passive.type].ContainsKey(passive.itemID))
				Debug.Log("Same passive: " + passive.itemID);
			else
				Items[passive.type].Add(passive.itemID, passive);
		}
	}
	private List<PassiveItem> GetJsonPassiveLibrary()
	{
		TextAsset jsonText = Resources.Load<TextAsset>(PassiveItemDirectory);

		Debug.Log (jsonText);
		//var passiveDatabase = JsonDarkestDeserializer.GetJsonPassiveDatabase(jsonText.text);
		JSONNode passiveDatabase = JSONNode.Parse(jsonText.text);
		//List<JsonPassive> jsonPassives = passiveDatabase.passives;
		List<PassiveItem> passives = new List<PassiveItem>();

		//for (int i = 0; i < jsonPassives.Count; i++)
		for (int i = 0; i < 4; i++)
		{
			
			PassiveItem passive = new PassiveItem ();
			//Debug.Log("Passive id is3: " + passiveDatabase["passive"][0][1].Value);
			passive.itemID = passiveDatabase["passive"][i][0].Value;
			//Debug.Log("Passive id is: " + passive.itemID);
			passive.name = passiveDatabase["passive"][i][1].Value;
			passive.type = passiveDatabase["passive"][i][2].Value;
			passive.description= passiveDatabase["passive"][i][3].Value;
			int[] statVals = new int[8];
			for(int j=0;j<8;j++){
				statVals[j] = int.Parse(passiveDatabase["passive"][i][4][j].Value);
			}
			passive.stats = statVals;
			Image image = Resources.Load<Image> (ItemImageDirectory+passive.name);
			passive.image = image;
			passives.Add(passive);
			Debug.Log("Passive id is: " + passive.name);
		}
		return passives;
	}
}


