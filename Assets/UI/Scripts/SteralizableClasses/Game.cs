using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Game {



	public static Game current;
	public HeroStats Kroner;
	public HeroStats Lee;
	public HeroStats Alexei;

	public Game(){
		Kroner = new HeroStats ("Kroner");
		Lee = new HeroStats ("Lee");
		Alexei = new HeroStats ("Alexei");

	}

}
