using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class InventorySceneManager : MonoBehaviour
{
    public static InventorySceneManager Instanse { get; private set; }

    [SerializeField]
    private MenuInventoryWindow menuInventoryWindow;
    private bool menuInventoryOpened;

    private void Awake()
    {
        if (Instanse == null)
        {
            Instanse = this;
        }
        else
        {
            Destroy(Instanse.gameObject);
            return;
        }

        menuInventoryWindow.EventWindowClosed += MenuInventoryClose;
     
    }

    private void Start()
    {
        if (Instanse != this)
            return;    
        menuInventoryWindow.Populate();
    }

    private void OnDestroy()
    {
    }	
    public void MenuInventoryClose()
    {
        menuInventoryOpened = false;
        menuInventoryWindow.gameObject.SetActive(menuInventoryOpened);
    }
		
    public void MenuInventoryClick()
    {
        menuInventoryOpened = !menuInventoryOpened;

        menuInventoryWindow.gameObject.SetActive(menuInventoryOpened);
    }
}