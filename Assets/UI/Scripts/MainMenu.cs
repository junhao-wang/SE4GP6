using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        GameObject.Find("SFX Source").GetComponent<SFXLoader>().LoadDoorSFX();
        PlayerPrefs.SetFloat("musicVolume", GameObject.Find("Music Source").GetComponent<MusicLoader>().music);
        PlayerPrefs.SetFloat("SFXVolume", GameObject.Find("SFX Source").GetComponent<SFXLoader>().SFX);
        SceneManager.LoadScene("Map");

    }

    public void ExitGame()
    {
        GameObject.Find("SFX Source").GetComponent<SFXLoader>().LoadDoorSFX();
        Application.Quit();
    }

    public void Options()
    {
        GameObject.Find("SFX Source").GetComponent<SFXLoader>().LoadDoorSFX();
        if (transform.Find("OptionsPanel").gameObject.activeSelf == true)
        {
            transform.Find("OptionsPanel").gameObject.SetActive(false);
        }
        else
        {
            transform.Find("OptionsPanel").gameObject.SetActive(true);
        }
    }

    public void Instructions()
    {
        GameObject.Find("SFX Source").GetComponent<SFXLoader>().LoadDoorSFX();
        if (transform.Find("InstructionPanel").gameObject.activeSelf == true)
        {
            transform.Find("InstructionPanel").gameObject.SetActive(false);
        }
        else
        {
            transform.Find("InstructionPanel").gameObject.SetActive(true);
        }
    }

}
