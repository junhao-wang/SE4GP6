using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLayout : MonoBehaviour {


	public int panelScale;
	public int spacingY; 			//acts as the spacing between the rows of items
	// Use this for initialization
	void Start () {
		formatItems ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void formatItems(){
		RectTransform canvasRT = gameObject.GetComponent<RectTransform>();
		RectTransform[] rt = gameObject.GetComponentsInChildren<RectTransform>();
		float x = 200;				//for scale -570
		float y = 40 + 240;				//for scale -1050, far right boundary is 1800
		int i = 0;					//item counter
		foreach (RectTransform t in rt){
			if(t.gameObject.GetInstanceID() != gameObject.GetInstanceID()){
				
				if (x >= 650) {
						//move to next row;
						x = 200;
						y -= 40;
					//x = 0;
					//y = 0;
				}
				t.position = new Vector2 (x, y);
				i++;
				if (i == 3) {								// three because of the parent, base picture and level
					x += 50;
					i = 0;
				}
			}
		}
	}
}
