using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour {
    DialogueSet currentDialogue;
    public GameObject Party;

    int dialogueIndex = 0;
    int lineIndex = 0;

    GameObject dialogueParent;

    //TextObject for the Dialogue
    Text dText;
    //TextObject for the name of the current speaker
    Text nameText;

    //the image on the left and right of the screen
    Image dialogueImageLeft;
    Image dialogueImageRight;

    bool isLastPortraitLeft = true;

    // Initialize Dialogue 
    void Start () {
        dialogueParent = transform.Find("DialogueUI").gameObject;
        dText = dialogueParent.transform.Find("DialogueBox").Find("dText").GetComponent<Text>();
        nameText = dialogueParent.transform.Find("DialogueBox").Find("Name").GetComponent<Text>();
        dialogueImageLeft = dialogueParent.transform.Find("LeftDialogue").GetComponent<Image>();
        dialogueImageRight = dialogueParent.transform.Find("RightDialogue").GetComponent<Image>();
        //startDialogue(1);
    }
	
	// Update is called once per frame
	void Update () {
<<<<<<< HEAD
        //move on to the next piece of dialogue on click or space
		if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode. Space)) 
=======
        //move on to the next piece of dialogue
		if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode. Space) ) && dialogueParent.activeSelf) 
>>>>>>> 381173cdf8c0b7cc6dfabeac94c326eef8d1ea55
        {
            //move through the dialogue of one character
            if (lineIndex < currentDialogue.Dialogue[dialogueIndex].Lines.Count - 1)
            { 
                lineIndex++;
                dText.text = currentDialogue.Dialogue[dialogueIndex].Lines[lineIndex];
            }
            //move to the next person speaking
            else if (dialogueIndex < currentDialogue.Dialogue.Length - 1)
            {
                dialogueIndex++;
                lineIndex = 0;
                dText.text = currentDialogue.Dialogue[dialogueIndex].Lines[lineIndex];
                setName();
                setPortrait();
            }
            //if there is no more dialogue, end the dialogue box
            else
            {
                lineIndex = 0;
                dialogueIndex = 0;

                dialogueParent.SetActive(false);
                Party.GetComponent<PartyProperties>().inDialogue = false;
            }
        }
	}

    //load the dialogue json
    void loadDialogue(int id)
    {
        string dialogue = System.IO.File.ReadAllText("Assets/Overworld/Json/Dialogue.json");
        DialogueSet[] allDialogue = JsonHelper.getJsonArray<DialogueSet>(dialogue);
        currentDialogue = allDialogue[id - 1];
    }

    //open the dialogue box with a dialogue id
    public void startDialogue(int id)
    {
        dialogueParent.SetActive(true);
        loadDialogue(id);
        lineIndex = 0;
        dialogueIndex = 0;
        dText.text = currentDialogue.Dialogue[dialogueIndex].Lines[lineIndex];
        setName();
        setPortrait();
    }

    //set the name of the character speaking
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

    //set the portrait of the character speaking
    void setPortrait()
    {
        if (false)
        {

        }
        else
        {
            //if no one is speaking, both portraits are false
            if (currentDialogue.Dialogue[dialogueIndex].name == "Blank")
            {
                dialogueImageRight.sprite = Resources.Load("Portraits/blank", typeof(Sprite)) as Sprite;
                dialogueImageLeft.sprite = Resources.Load("Portraits/blank", typeof(Sprite)) as Sprite;
            }

            else if (dialogueImageLeft.sprite.name != currentDialogue.Dialogue[dialogueIndex].name && dialogueImageRight.sprite.name != currentDialogue.Dialogue[dialogueIndex].name)
            {
                //if the name of the character matches a portrait
                if (Resources.Load("Portraits/" + currentDialogue.Dialogue[dialogueIndex].name, typeof(Sprite)) as Sprite != null)
                {
                    //set left
                    if (currentDialogue.Dialogue[dialogueIndex].side == "L")
                    {
                        dialogueImageLeft.sprite = Resources.Load("Portraits/" + currentDialogue.Dialogue[dialogueIndex].name, typeof(Sprite)) as Sprite;
                    }
                    //set right
                    else if (currentDialogue.Dialogue[dialogueIndex].side == "R")
                    {
                        dialogueImageRight.sprite = Resources.Load("Portraits/" + currentDialogue.Dialogue[dialogueIndex].name, typeof(Sprite)) as Sprite;
                    }
                }
                else
                {
                    //set the current image as blank
                    if (currentDialogue.Dialogue[dialogueIndex].side == "R")
                    {
                        dialogueImageRight.sprite = Resources.Load("Portraits/blank", typeof(Sprite)) as Sprite;
                    }
                    else if (currentDialogue.Dialogue[dialogueIndex].side == "L")
                    {
                        dialogueImageLeft.sprite = Resources.Load("Portraits/blank", typeof(Sprite)) as Sprite;
                    }

                }
                
            }          
        }
    }
}
