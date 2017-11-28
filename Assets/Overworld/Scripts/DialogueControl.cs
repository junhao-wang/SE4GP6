using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour {
    DialogueSet currentDialogue;

    int dialogueIndex = 0;
    int lineIndex = 0;

    GameObject dialogueParent;

    Text dText;
    Text nameText;

    Image dialogueImageLeft;
    Image dialogueImageRight;

    bool isLastPortraitLeft = true;
    // Use this for initialization
    void Start () {
        dialogueParent = transform.Find("Dialogue").gameObject;
        dText = dialogueParent.transform.Find("DialogueBox").Find("dText").GetComponent<Text>();
        nameText = dialogueParent.transform.Find("DialogueBox").Find("Name").GetComponent<Text>();
        dialogueImageLeft = dialogueParent.transform.Find("LeftDialogue").GetComponent<Image>();
        dialogueImageRight = dialogueParent.transform.Find("RightDialogue").GetComponent<Image>();
        startDialogue(1);
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            if (lineIndex < currentDialogue.Dialogue[dialogueIndex].Lines.Count - 1)
            {
                lineIndex++;
                dText.text = currentDialogue.Dialogue[dialogueIndex].Lines[lineIndex];
            }
            else if (dialogueIndex < currentDialogue.Dialogue.Length - 1)
            {
                dialogueIndex++;
                lineIndex = 0;
                dText.text = currentDialogue.Dialogue[dialogueIndex].Lines[lineIndex];
                setName();
                setPortrait();
            }
            else
            {
                lineIndex = 0;
                dialogueIndex = 0;
                dialogueParent.SetActive(false);
            }
        }
	}

    void loadDialogue(int id)
    {
        string dialogue = System.IO.File.ReadAllText("Assets/Overworld/Json/Dialogue.json");
        DialogueSet[] allDialogue = JsonHelper.getJsonArray<DialogueSet>(dialogue);
        currentDialogue = allDialogue[id - 1];
    }

    void startDialogue(int id)
    {
        loadDialogue(1);
        lineIndex = 0;
        dialogueIndex = 0;
        dText.text = currentDialogue.Dialogue[dialogueIndex].Lines[lineIndex];
        setName();
        setPortrait();
    }

    void setName()
    {
        if (currentDialogue.Dialogue[dialogueIndex].name == "Blank")
        {
            nameText.text = "";
        }
        else
        {
            nameText.text = currentDialogue.Dialogue[dialogueIndex].name;
        }
    }

    void setPortrait()
    {
        if (false)
        {

        }
        else
        {
            print(dialogueImageLeft.sprite.name);
            if (currentDialogue.Dialogue[dialogueIndex].name == "Blank")
            {
                dialogueImageRight.sprite = Resources.Load("Portraits/blank", typeof(Sprite)) as Sprite;
                dialogueImageLeft.sprite = Resources.Load("Portraits/blank", typeof(Sprite)) as Sprite;
            }
            else if (dialogueImageLeft.sprite.name != currentDialogue.Dialogue[dialogueIndex].name && dialogueImageRight.sprite.name != currentDialogue.Dialogue[dialogueIndex].name)
            {
                if (Resources.Load("Portraits/" + currentDialogue.Dialogue[dialogueIndex].name, typeof(Sprite)) as Sprite != null)
                {
                    if (currentDialogue.Dialogue[dialogueIndex].side == "L")
                    {
                        dialogueImageLeft.sprite = Resources.Load("Portraits/" + currentDialogue.Dialogue[dialogueIndex].name, typeof(Sprite)) as Sprite;
                    }
                    else if (currentDialogue.Dialogue[dialogueIndex].side == "R")
                    {
                        dialogueImageRight.sprite = Resources.Load("Portraits/" + currentDialogue.Dialogue[dialogueIndex].name, typeof(Sprite)) as Sprite;
                    }
                }
                else
                {
                    
                    if (isLastPortraitLeft)
                    {
                        dialogueImageRight.sprite = Resources.Load("Portraits/blank", typeof(Sprite)) as Sprite;
                        isLastPortraitLeft = false;
                    }
                    else
                    {
                        dialogueImageLeft.sprite = Resources.Load("Portraits/blank", typeof(Sprite)) as Sprite;
                        isLastPortraitLeft = true;
                    }

                }
                
            }          
        }
    }
}
