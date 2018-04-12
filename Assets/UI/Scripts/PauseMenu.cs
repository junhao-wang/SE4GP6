using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    public Slider sfx;
    public Slider music;
    public GameObject pausedMenu;
    public GameObject musicSource;
    public GameObject pausedPanel;
    public GameObject quitWarning;

	// Use this for initialization
	void Start ()
    {

        if (PlayerPrefs.HasKey("musicVolume")) { sfx.value = PlayerPrefs.GetFloat("musicVolume"); }
        if (PlayerPrefs.HasKey("SFXVolume")) { music.value = PlayerPrefs.GetFloat("SFXVolume"); }
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void Resume()
    {
        SaveVolume();
        pausedMenu.gameObject.SetActive(false);
        musicSource.gameObject.GetComponent<AudioSource>().UnPause();

    }

    public void QuitWarning()
    {
        pausedPanel.gameObject.SetActive(false);
        quitWarning.gameObject.SetActive(true);

    }

    public void NoQuit()
    {
        pausedPanel.gameObject.SetActive(true);
        quitWarning.gameObject.SetActive(false);
    }
    public void Quit()
    {
        SaveVolume();
        SceneManager.LoadScene("menu");
    }

    public void SaveVolume()
    {
        PlayerPrefs.SetFloat("musicVolume", GameObject.Find("Music Source").GetComponent<MusicLoader>().music);
        PlayerPrefs.SetFloat("SFXVolume", GameObject.Find("SFX Source").GetComponent<SFXLoader>().SFX);
    }

    public void Pause()
    {
        pausedMenu.gameObject.SetActive(true);
        musicSource.gameObject.GetComponent<AudioSource>().Pause();
    }
}
