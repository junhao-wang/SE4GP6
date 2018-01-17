using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame()
    {
        SceneManager.LoadScene("Map");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Instructions()
    {
        if(transform.Find("InstructionPanel").gameObject.activeSelf == true)
        {
            transform.Find("InstructionPanel").gameObject.SetActive(false);
        }
        else
        {
            transform.Find("InstructionPanel").gameObject.SetActive(true);
        }
    }
}
