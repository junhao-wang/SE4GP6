﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MouseHover : MonoBehaviour {

	public DisplayItemInfo display;
	// Use this for initialization
	void Start() {
		
	}
	
	// Update is called once per frame
	void Update () {
		display.GetComponent<DisplayItemInfo> ();
		display.item = gameObject;
		MouseHover thisScript = GetComponent<MouseHover> ();
		thisScript.enabled = false;
	}

}