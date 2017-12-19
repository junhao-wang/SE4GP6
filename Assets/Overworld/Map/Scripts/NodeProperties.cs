using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[System.Serializable]
public class NodeProperties : MonoBehaviour {
    public GameObject ParentTile;
    public GameObject PathPrefab;
    public GameObject Party;
    public int ID; //node id used purely for saving/loading
    public Color ResourceEvent, DialogueEvent, CombatEvent, NoEvent;
    public List<GameObject> Neighbors;
    public bool visited = false;
    public List<GameObject> Pathing,OccupiedPathing;
    public EventType NodeEvent =EventType.NONE ;
    public int dialogueID = 0;
    public enum Region{A,B,C,D,E,NULL};
    public float[] ResourceMod = new float[(int)PartyProperties.ResourceType.SIZE];
    public enum EventType { COMBAT,NARRATIVECORE,NARRATIVE,NONE};
    public Region nodeRegion = Region.NULL;
    public List<int> dialogueSet = new List<int>();

    // Use this for initialization
    void Start () {
        Party = GameObject.Find("PartyPlaceholder");
        DontDestroyOnLoad(transform.gameObject);
        DontDestroyOnLoad(ParentTile);
        dialogueSet.Add(0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    //pathing functions
    public void drawCurrentPaths()//draws paths available on occupied node
    {
        foreach (GameObject Node in Neighbors)
        {
            Connect(Node, OccupiedPathing);
        }
    }
    public void clearCurrentPaths()//clears pathing when party moves
    {
        for (int i = OccupiedPathing.Count - 1; i >= 0; i--)
        {
            Destroy(OccupiedPathing[i]);
            OccupiedPathing.RemoveAt(i);
        }
    }
    public void drawPaths()//draws paths of highlighted node
    {
        foreach(GameObject Node in Neighbors)
        {
            Connect(Node);
        }
    }
    public void clearPaths()//clears pathing when party moves
    {
        for(int i = Pathing.Count-1; i >=0 ; i--)
        {
            Destroy(Pathing[i]);
            Pathing.RemoveAt(i);
        }
    }

    //Connects this node to another node by updating both node's neighbour lists

    void Connect(GameObject NodeB, List<GameObject> walkpath = null)
    {
        Vector3 unitVec = NodeB.GetComponent<Transform>().position - gameObject.GetComponent<Transform>().position;
        float length = unitVec.magnitude;
        unitVec = unitVec.normalized;
        float angle = Vector3.SignedAngle(new Vector3(1, 0,0), unitVec, new Vector3(1, 0, 0));
        if (unitVec[1] < 0)
        {
            angle *= -1;
        }
        //print(angle);
        for (float f = 0.03f;f < length - 0.03f; f+= 0.06f)
        {
            GameObject path = Instantiate(PathPrefab);
            Vector3 offset = f * unitVec;
            path.GetComponent<Transform>().position = gameObject.GetComponent<Transform>().position + offset;
            path.GetComponent<Transform>().rotation = Quaternion.Euler(0,0, angle);
            if (walkpath==null)
            {
                Pathing.Add(path);
            }
            else
            {
                walkpath.Add(path);
            }

        }

    }
    //cost function
    public float costTo(GameObject NodeB)
    {
        return 1.0f;
    }

    //processes the event associated with the node
    public void PopEvent()
    {
        if (NodeEvent == EventType.NONE)
        {
            return;
        } else if (NodeEvent == EventType.NARRATIVECORE)
        {
            GameObject Canvas = GameObject.FindWithTag("Overworld Canvas");
            Canvas.transform.Find("DialogueUI").gameObject.SetActive(true);
            //Canvas.GetComponent<DialogueControl>().startDialogue(dialogueID);
            Canvas.GetComponent<DialogueControl>().startDialogue(1);
            Party.GetComponent<PartyProperties>().inDialogue = true;
            if (dialogueSet.Count == 1 && dialogueSet[0] == 0)
            {
                NodeEvent = EventType.NONE;
            }
            

        } else if (NodeEvent == EventType.COMBAT)
        {
            NodeEvent = EventType.NONE;
            GameObject MController = GameObject.Find("MapController");

            SceneManager.LoadScene("TestBattle");

        }
        else if (NodeEvent == EventType.NARRATIVE)
        {
            NodeEvent = EventType.NONE;
            GameObject Party = GameObject.FindWithTag("Overworld Party");
            Party.GetComponent<PartyProperties>().ProccessResourceEvent(gameObject.GetComponent<NodeProperties>().ResourceMod);


        }
        SetColor();
    }

    //sets the node color to the one appropriate for the event it contains
    public void SetColor()
    {

        switch (NodeEvent)
        {
            case (EventType.NONE):
                gameObject.GetComponent<SpriteRenderer>().color = NoEvent;
                break;
            case (EventType.COMBAT):
                gameObject.GetComponent<SpriteRenderer>().color = CombatEvent;
                break;
            case (EventType.NARRATIVECORE):
                gameObject.GetComponent<SpriteRenderer>().color = DialogueEvent;
                break;
            case (EventType.NARRATIVE):
                gameObject.GetComponent<SpriteRenderer>().color = ResourceEvent;
                break;
        }
    }

    public void loadDialogueSet(int SetID)
    {
       /*some code here to load the set of dialogues represented by SetID into the
        dialogueSet list
        */

    }

    public List<int> fetchRequirements(int dialogueID)
    {
        List<int> result = new List<int>();
        /*code for loading dialogue requirements or
         a function call that returns a list of requirements*/

        return result;
    }

    public bool testDialogueReq(List<int> requirements)
    {
        if(requirements[0] == 0)
        {
            return true;
        }

        foreach(int dialogue in Party.GetComponent<PartyProperties>().CompletedDialogue)
        {
            bool cond1 = requirements.Contains(dialogue / 10 * 10);
            bool cond2 = requirements.Contains(dialogue);
            if(cond1 || cond2)
            {
                requirements.Remove(dialogue);
            }
        }



        if(requirements.Count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void startDialogue()
    {
        int dialogueID = -1;
        foreach (int dialogue in dialogueSet)
        {
            if (testDialogueReq(fetchRequirements(dialogue)))
            {
                dialogueID = dialogue;

            }
        }
        if (dialogueID == -1)
        {
            return;
        }
        else
        {
            GameObject Canvas = GameObject.FindWithTag("Overworld Canvas");
            Canvas.GetComponent<DialogueControl>().startDialogue(dialogueID);
            if (!Party.GetComponent<PartyProperties>().CompletedDialogue.Contains(dialogueID))
            {
                Party.GetComponent<PartyProperties>().CompletedDialogue.Add(dialogueID);
                dialogueSet.Remove(dialogueID);
            }
        }


    }

}
