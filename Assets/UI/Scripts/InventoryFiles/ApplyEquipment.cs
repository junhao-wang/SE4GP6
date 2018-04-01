﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyEquipment : MonoBehaviour {


	public GenericUnit[] units;
	public EquipedInventory equipedInventory { get; set; }
	public List<HeroStats> heroStats { get; set; }
	// Use this for initialization
	void Start () {
		assign ();
		//REPLACE WITH THE EQUIPEDINVENTORY MADE IN THE OVERWORLD/MENU
		equipedInventory = new EquipedInventory ();
		heroStats.Add(new HeroStats ("Kroner"));
		heroStats.Add(new HeroStats ("Lee"));
		heroStats.Add(new HeroStats ("Alexei"));
		apply ();
	}
	private void assign(){
		units = GetComponentsInChildren<GenericUnit> (); //Order goes Kroner/Lee/Alexei
	}
	private void apply(){
		foreach (GenericUnit g in units) {
			combine (g.UnitName);
		}
	}
	private void combine(string Name){
		int gu = 3;
		int[] stats; 
		List<PassiveItem> ei = null;
		if (Name.Equals ("Kroner")){
			gu = 0;
			ei = equipedInventory.KronerPassives;
		}
			if (Name.Equals ("Lee")){
			gu = 1;
			ei = equipedInventory.LeePassives;
			}
			if (Name.Equals ("Alexei")){
			gu = 2;
			ei = equipedInventory.AlexeiPassives;
			}
		foreach (PassiveItem p in ei){
			stats = p.stats;
			units [gu].HitPoints = heroStats [gu].getHP () + stats [0];
			units [gu].TotalArmor = heroStats [gu].getArmour () + stats [1];
			units [gu].AttackFactor = heroStats [gu].getAttack () + stats [2];
			units [gu].GunAttack = heroStats [gu].getGunAttack () + stats [3];
			units [gu].Speed = heroStats [gu].getSpeed () + stats [4];
			units [gu].AttackRange = heroStats [gu].getRange () + stats [5];
			units [gu].MovementPoints = heroStats [gu].getMoveRange () + stats [6];
		//REPEAT FOR EVERY OTHER stat
		}
	}

}