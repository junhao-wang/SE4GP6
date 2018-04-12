using System.Collections.Generic;
using SimpleJSON;

// ReSharper disable CollectionNeverUpdated.Global
// ReSharper disable InconsistentNaming
// NOT USED IGNORE, FROM DARKEST DUNGEON SCRIPTS
namespace JsonClasses
{
	public class JsonPassive
	{
		public string id;
		public List<string> buffs;
		public List<string> hero_class_requirements;
		public string rarity;
		public int price;
		public int limit;
		public string origin_dungeon;

		public JsonPassive()
		{
			buffs = new List<string>();
			hero_class_requirements = new List<string>();
		}
	}

	public class JsonPassiveDatabase
	{
		public List<string> rarities;
		public List<JsonPassive> passives;
	}
//	public static class JsonDarkestDeserializer
//	{
//		public static JsonPassiveDatabase GetJsonPassiveDatabase(string passiveData)
//		{
//			//return JsonConvert.DeserializeObject<JsonPassiveDatabase>(passiveData);
//
//			JSON.Parse(passiveData);
//
//		}
//	}
}
