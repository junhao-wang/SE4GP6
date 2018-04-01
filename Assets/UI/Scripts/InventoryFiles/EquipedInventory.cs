using System.Collections;
using System.Collections.Generic;


public class EquipedInventory {


	public List<PassiveItem>  LeePassives{ get; set; }
	public List<PassiveItem>  KronerPassives{ get; set; }
	public List<PassiveItem>  AlexeiPassives{ get; set; }
	public void AddPassiveItem(PassiveItem passive,string Name)
	{
		if(Name.Equals("Lee"))
			LeePassives.Add(passive);
		if(Name.Equals("Kroner"))
			KronerPassives.Add(passive);
		if(Name.Equals("Alexei"))
			AlexeiPassives.Add(passive);
	}
	public void RemovePassiveItem(PassiveItem passive,string Name)
	{
		if(Name.Equals("Lee"))
			LeePassives.Remove(passive);
		if(Name.Equals("Kroner"))
			KronerPassives.Remove(passive);
		if(Name.Equals("Alexei"))
			AlexeiPassives.Remove(passive);
	}
	// Will add the basic inventory layout for the characters
	public EquipedInventory()
	{
		LeePassives = new List<PassiveItem>();
		LeePassives.Add((PassiveItem)InventoryManager.Data.Items["passive"]["1"]);
		KronerPassives = new List<PassiveItem>();
		KronerPassives.Add((PassiveItem)InventoryManager.Data.Items["passive"]["1"]);
		AlexeiPassives = new List<PassiveItem>();
		AlexeiPassives.Add((PassiveItem)InventoryManager.Data.Items["passive"]["1"]);
	}
}
