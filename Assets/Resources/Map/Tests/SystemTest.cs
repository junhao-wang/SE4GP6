using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

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
        yield return null;
    }
    [UnityTest]
    public IEnumerator EachNodeThreeNeighbours()
    {
        
        yield return null;
    }
 
    //--------------Private methods
 

    //#################Tests Expected to FAIL###############################
 





}
