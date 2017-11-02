using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

public class SystemTest : IPrebuildSetup
{

    public void Setup()
    {
        SceneManager.LoadScene("Map");
    }


//#################Tests Expected to PASS###############################
    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator MainMenuStartGame() {
        SceneManager.LoadScene("menu");
        yield return new WaitForSeconds(2);
        GameObject startButton = GameObject.Find("Canvas");
        startButton.GetComponent<MainMenu>().StartGame();
        yield return new WaitForSeconds(2);
        Assert.IsTrue(SceneManager.GetActiveScene().name=="Map");
        yield return null;
    }

    [UnityTest]
    public IEnumerator NodeLoadsCombat()
    {
        SceneManager.LoadScene("Map");
        yield return new WaitForSeconds(2);
        GameObject MCont = GameObject.Find("MapController");
        List<GameObject> nodes = MCont.GetComponent<MapProperties>().Nodes;
        nodes[9].GetComponent<NodePartySelect>().DebugClick();//TODO comment this line out later
        GameObject Party = GameObject.FindWithTag("Overworld Party");
        Party.GetComponent<PartyProperties>().OccupiedNode.GetComponent<NodePartySelect>().DebugClick();//TODO comment this line out later
        Party.GetComponent<PartyProperties>().OccupiedNode.GetComponent<NodeProperties>().Neighbors[0].GetComponent<NodePartySelect>().DebugClick();
        yield return new WaitForSeconds(5);
        Assert.IsTrue(SceneManager.GetActiveScene().name == "TestBattle");
        yield return null;
    }
 
    //--------------Private methods
 

    //#################Tests Expected to FAIL###############################
 





}
