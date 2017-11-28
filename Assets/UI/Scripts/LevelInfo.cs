using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfo : MonoBehaviour {
			



	private Sprite level;
	private Image levelRenderer;
	public int levelnum;
	//private Object[] levelRenderer;
	// Use this for initialization
	void Awake () {




		Sprite[] levels = Resources.LoadAll <Sprite>("Icons/levels");
		levelnum = Random.Range (0, 4);
		level = levels[levelnum];
		levelRenderer = gameObject.GetComponent <Image>();
		levelRenderer.sprite = level;
	}
	
	// Update is called once per frame
	void Update () {
		//levelRenderer.sprite = levels[level];
	}

	public int getlevel(){
		return levelnum;
	}

}
