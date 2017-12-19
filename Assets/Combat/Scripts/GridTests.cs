using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

public class GridTests : IPrebuildSetup{

    GameObject grid;
    GenerateBattlemap map;

    GameObject Units;


    public void Setup()
    {
        
    }

    void SetScene()
    {
        GameObject Players = new GameObject("Players");
        Players = Resources.Load("Prefabs/PlayerParent", typeof(GameObject)) as GameObject;
        GameObject Units = new GameObject("Units");
        Units = Resources.Load("Prefabs/UnitParent", typeof(GameObject)) as GameObject;
        grid = new GameObject();
        grid.AddComponent<GenerateBattlemap>();
        grid.AddComponent<CustomUnitGenerator>();
        grid.AddComponent<CellGrid>();
        grid.GetComponent<CustomUnitGenerator>().UnitsParent = Units.transform;
        grid.GetComponent<CustomUnitGenerator>().CellsParent = grid.transform;
        grid.GetComponent<CellGrid>().PlayersParent = Players.transform;
        map = grid.GetComponent<GenerateBattlemap>();
        map.squarePrefab = Resources.Load("Prefabs/SquareTile", typeof(GameObject)) as GameObject;
        map.width = 20;
        map.height = 20;
    }

    void SetUpWithScene()
    {
        
    }
	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator Grid_IsCreatedWithCorrect_NumberOfCells()
    {
        SceneManager.LoadScene("TestBattle");
        yield return new WaitForFixedUpdate();
        grid = GameObject.Find("CellGrid");
        map = GameObject.Find("CellGrid").GetComponent<GenerateBattlemap>();
        yield return new WaitForFixedUpdate();

        int width = map.width;
        int height = map.height;
        Assert.AreEqual(width * height, grid.transform.childCount);
	}

    [UnityTest]
    public IEnumerator Unit_TakesCorrectAmountOf_Damage()
    {
        SceneManager.LoadScene("TestBattle");
        yield return new WaitForFixedUpdate();
        grid = GameObject.Find("CellGrid");
        map = GameObject.Find("CellGrid").GetComponent<GenerateBattlemap>();
        yield return new WaitForFixedUpdate();
        int health = grid.GetComponent<CellGrid>().Units[0].HitPoints;
        grid.GetComponent<CellGrid>().Units[0].TakeDamage(3, true);
        Assert.AreEqual(health - 3, grid.GetComponent<CellGrid>().Units[0].HitPoints);
    }

    [UnityTest]
    public IEnumerator Player_HasFirst_Turn()
    {
        SceneManager.LoadScene("TestBattle");
        yield return new WaitForFixedUpdate();
        grid = GameObject.Find("CellGrid");
        map = GameObject.Find("CellGrid").GetComponent<GenerateBattlemap>();
        yield return new WaitForFixedUpdate();
        Assert.AreEqual(grid.GetComponent<CellGrid>().CurrentPlayerNumber, 0);
    }

    [UnityTest]
    public IEnumerator Turns_Alternate()
    {
        SceneManager.LoadScene("TestBattle");
        yield return new WaitForFixedUpdate();
        grid = GameObject.Find("CellGrid");
        map = GameObject.Find("CellGrid").GetComponent<GenerateBattlemap>();
        yield return new WaitForFixedUpdate();
        int currentTurnPlayer = grid.GetComponent<CellGrid>().CurrentPlayerNumber;
        grid.GetComponent<CellGrid>().EndTurn();
        Assert.AreNotEqual(currentTurnPlayer, grid.GetComponent<CellGrid>().CurrentPlayerNumber);
    }

    [UnityTest]
    public IEnumerator Player_HasNoControlWhen_NotItsTurn()
    {
        SceneManager.LoadScene("TestBattle");
        yield return new WaitForFixedUpdate();
        grid = GameObject.Find("CellGrid");
        map = GameObject.Find("CellGrid").GetComponent<GenerateBattlemap>();
        yield return new WaitForFixedUpdate();
        
        for (int i = 0; i< grid.GetComponent<CellGrid>().Players.Count; i++)
        {
            if (grid.GetComponent<CellGrid>().Players[i] == grid.GetComponent<CellGrid>().CurrentPlayer)
            {
                Assert.AreEqual(grid.GetComponent<CellGrid>().CurrentPlayer.isPlaying, true);
            }
            else
            {
                Assert.AreNotEqual(grid.GetComponent<CellGrid>().Players[i], true);
            }
            
        }
        
    }

    [UnityTest]
    public IEnumerator Game_EndsWhenOnlyOne_Player()
    {
        SceneManager.LoadScene("TestBattle");
        yield return new WaitForFixedUpdate();
        grid = GameObject.Find("CellGrid");
        map = GameObject.Find("CellGrid").GetComponent<GenerateBattlemap>();

        yield return new WaitForFixedUpdate();
        for(int i = grid.GetComponent<CellGrid>().Units.Count -1; i >= 0; i--)
        {
            if (grid.GetComponent<CellGrid>().Units[i].PlayerNumber != grid.GetComponent<CellGrid>().CurrentPlayerNumber)
            {
                grid.GetComponent<CellGrid>().Units[i].TakeDamage(1000, true);
            }
        }
        yield return new WaitForFixedUpdate();
        Assert.AreEqual(grid.GetComponent<CellGrid>().isGameOver, true);
    }

    [UnityTest]
    public IEnumerator Player_CanAccessMenuDuring_Action()
    {
        SceneManager.LoadScene("TestBattle");
        yield return new WaitForFixedUpdate();
        grid = GameObject.Find("CellGrid");
        map = GameObject.Find("CellGrid").GetComponent<GenerateBattlemap>();
        bool isMenuOpen = false;

        yield return new WaitForFixedUpdate();
        grid.GetComponent<CellGrid>().EndTurn();
        //Menu.open()
        yield return new WaitForFixedUpdate();
        Assert.AreEqual(isMenuOpen, true);
    }

    [UnityTest]
    public IEnumerator PlayerWins_GoTo_Map()
    {
        SceneManager.LoadScene("TestBattle");
        yield return new WaitForFixedUpdate();
        grid = GameObject.Find("CellGrid");
        map = GameObject.Find("CellGrid").GetComponent<GenerateBattlemap>();

        yield return new WaitForFixedUpdate();
        for (int i = grid.GetComponent<CellGrid>().Units.Count - 1; i >= 0; i--)
        {
            if (grid.GetComponent<CellGrid>().Units[i].PlayerNumber != grid.GetComponent<CellGrid>().CurrentPlayerNumber)
            {
                grid.GetComponent<CellGrid>().Units[i].TakeDamage(1000, true);
            }
        }
        yield return new WaitForFixedUpdate();
        Assert.AreEqual(SceneManager.GetActiveScene().name, "Map");
    }

    [UnityTest]
    public IEnumerator PlayerLoses_GoTo_Menu()
    {
        SceneManager.LoadScene("TestBattle");
        yield return new WaitForFixedUpdate();
        grid = GameObject.Find("CellGrid");
        map = GameObject.Find("CellGrid").GetComponent<GenerateBattlemap>();

        yield return new WaitForFixedUpdate();
        for (int i = grid.GetComponent<CellGrid>().Units.Count - 1; i >= 0; i--)
        {
            if (grid.GetComponent<CellGrid>().Units[i].PlayerNumber == grid.GetComponent<CellGrid>().CurrentPlayerNumber)
            {
                grid.GetComponent<CellGrid>().Units[i].TakeDamage(1000, true);
            }
        }

        yield return new WaitForFixedUpdate();
        Assert.AreEqual(SceneManager.GetActiveScene().name, "menu");
    }

    [UnityTest]
    public IEnumerator Player_CanSelect_Character()
    {
        SceneManager.LoadScene("TestBattle");
        yield return new WaitForFixedUpdate();
        grid = GameObject.Find("CellGrid");
        map = GameObject.Find("CellGrid").GetComponent<GenerateBattlemap>();
        yield return new WaitForFixedUpdate();

        grid.GetComponent<CellGrid>().Units[0].OnUnitSelected();
        yield return new WaitForFixedUpdate();
        Assert.True(grid.GetComponent<CellGrid>().Units[0].isSelected);
    }

    [UnityTest]
    public IEnumerator Player_CanMove_Character()
    {
        SceneManager.LoadScene("TestBattle");
        yield return new WaitForFixedUpdate();
        grid = GameObject.Find("CellGrid");
        map = GameObject.Find("CellGrid").GetComponent<GenerateBattlemap>();
        yield return new WaitForFixedUpdate();

        grid.GetComponent<CellGrid>().Units[0].OnUnitSelected();
        yield return new WaitForFixedUpdate();
        Assert.True(grid.GetComponent<CellGrid>().Units[0].GetAvailableDestinations(grid.GetComponent<CellGrid>().Cells) != null);
    }

    [UnityTest]
    public IEnumerator Character_CannotMoveTo_Obstacle()
    {
        SceneManager.LoadScene("TestBattle");
        yield return new WaitForFixedUpdate();
        grid = GameObject.Find("CellGrid");
        map = GameObject.Find("CellGrid").GetComponent<GenerateBattlemap>();
        yield return new WaitForFixedUpdate();

        grid.GetComponent<CellGrid>().Units[0].OnUnitSelected();
        yield return new WaitForFixedUpdate();
        foreach (Cell c in grid.GetComponent<CellGrid>().Units[0].GetAvailableDestinations(grid.GetComponent<CellGrid>().Cells))
        {
            Assert.True(!c.IsTaken);
        }
        
    }

    [UnityTest]
    public IEnumerator Character_CanPerformPossible_Actions()
    {
        SceneManager.LoadScene("TestBattle");
        yield return new WaitForFixedUpdate();
        grid = GameObject.Find("CellGrid");
        map = GameObject.Find("CellGrid").GetComponent<GenerateBattlemap>();
        yield return new WaitForFixedUpdate();

        grid.GetComponent<CellGrid>().Units[0].OnUnitSelected();
        yield return new WaitForFixedUpdate();
        foreach (Cell c in grid.GetComponent<CellGrid>().Units[0].GetAvailableDestinations(grid.GetComponent<CellGrid>().Cells))
        {
            Assert.False(grid.GetComponent<CellGrid>().Units[0].canAttack);
        }
    }

    [UnityTest]
    public IEnumerator Unit_RemovedOn_Death()
    {
        SceneManager.LoadScene("TestBattle");
        yield return new WaitForFixedUpdate();
        grid = GameObject.Find("CellGrid");
        map = GameObject.Find("CellGrid").GetComponent<GenerateBattlemap>();
        yield return new WaitForFixedUpdate();
        string uName = grid.GetComponent<CellGrid>().Units[0].name;
        grid.GetComponent<CellGrid>().Units[0].TakeDamage(10000, true);
        yield return new WaitForFixedUpdate();
        Assert.True(grid.GetComponent<CellGrid>().Units[0].name != uName);
    }

    [UnityTest]
    public IEnumerator Unit_DiesPreempt_Order()
    {
        SceneManager.LoadScene("TestBattle");
        yield return new WaitForFixedUpdate();
        grid = GameObject.Find("CellGrid");
        map = GameObject.Find("CellGrid").GetComponent<GenerateBattlemap>();
        yield return new WaitForFixedUpdate();
        grid.GetComponent<CellGrid>().Units[1].TakeDamage(10000, true);
        yield return new WaitForSeconds(3);
        Assert.True(grid.GetComponent<CellGrid>().CurrentPlayerNumber == 1);
    }

}
