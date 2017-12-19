using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleState{



	public static BattleState current;
    public bool finalBattle = false;
	public HeroStats Kroner;
	public HeroStats Lee;
	public HeroStats Alexei;

	public BattleState(){
		Kroner = new HeroStats ("Kroner");
		Lee = new HeroStats ("Lee");
		Alexei = new HeroStats ("Alexei");

	}

}
