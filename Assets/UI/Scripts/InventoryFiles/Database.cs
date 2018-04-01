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
	private const string ItemImageDirectory = "Icons";
	public Dictionary<string, Dictionary<string, ItemInfo>> Items { get; private set; }

	public void Load(){
		LoadJsonPassives ();
	}



	private void LoadJsonPassives()
	{
		Items = new Dictionary<string, Dictionary<string, ItemInfo>>();

		if (!Items.ContainsKey("passive"))
			Items.Add("passive", new Dictionary<string, ItemInfo>());
		Debug.Log ("Before Json Read");
		foreach(var passive in GetJsonPassiveLibrary())
		{
			Debug.Log ("Before keys of type :"+ passive.type + " are read");
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
		for (int i = 0; i < passiveDatabase.Count; i++)
		{
			PassiveItem passive = new PassiveItem ();
			Debug.Log("Passive id is3: " + passiveDatabase["passive"][0][1].Value);
			passive.itemID = passiveDatabase["passive"][i][0].Value;
			Debug.Log("Passive id is: " + passive.itemID);
			//foreach(var buffName in jsonPassives[i].buffs.ToArray())
			{
//				if (!Buffs.ContainsKey(buffName))
//					Debug.Log("Passive buff " + buffName + " not found.");
//				else
//					passive.Buffs.Add(Buffs[buffName]);
			}
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
		}
		return passives;
	}
}


