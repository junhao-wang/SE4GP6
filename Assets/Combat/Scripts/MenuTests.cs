using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuTests : IPrebuildSetup{
	GameObject grid;
	GenerateBattlemap map;

	GameObject Units;


	public void Setup()
	{

	}

	void SetScene()
	{
		SceneManager.LoadScene("Map");
	}

	void SetUpWithScene()
	{

	}
	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator MainMenuOpened()
	{
		
		Assert.AreEqual(SceneManager.GetActiveScene, "Map");
	}

	[UnityTest]
	public IEnumerator MainMenuClosed()
	{
		Application.Quit;
		Assert.AreEqual(SceneManager.GetActiveScene, null);
	}

	[UnityTest]
	public IEnumerator GameMenuOpenClose()
	{
		SceneManager.LoadScene("TestBattle");
		Input.GetKey (KeyCode.Escape);
		Assert.False();
	}

	[UnityTest]
	public IEnumerator AudioMenuOpenClose()
	{
		SceneManager.LoadScene("TestBattle");
		Input.GetKey (KeyCode.Escape);
		Assert.False();
	}
	[UnityTest]
	public IEnumerator GraphicMenuOpenClose()
	{
		SceneManager.LoadScene("TestBattle");
		Input.GetKey (KeyCode.Escape);
		Assert.False();
	}
	[UnityTest]
	public IEnumerator LoadGameMenuOpenClose()
	{
		SceneManager.LoadScene("TestBattle");
		Input.GetKey (KeyCode.Escape);
		Assert.False();
	}
	[UnityTest]
	public IEnumerator SaveGameMenuOpenClose()
	{
		SceneManager.LoadScene("TestBattle");
		Input.GetKey (KeyCode.Escape);
		Assert.False();
	}
	[UnityTest]
	public IEnumerator NewGameMenuOpenClose()
	{
		Input.GetKey (KeyCode.N);
		SceneManager.LoadScene("TestBattle");
		Assert.AreEquals(SceneManager.GetActiveScene,"TestBattle");
	}
	public IEnumerator ItemMenuOpenClose()
	{
		Input.GetKey (KeyCode.I);
		SceneManager.LoadScene("Items");
		Assert.AreEquals(SceneManager.GetActiveScene,"Items");
	}


}