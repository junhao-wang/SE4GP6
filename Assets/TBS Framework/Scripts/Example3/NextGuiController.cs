using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class NextGuiController : MonoBehaviour
{
    public CellGrid CellGrid;
    public GameObject UnitsParent;

    public Button NextTurnButton;

    public GameObject InfoPanel;
    public GameObject GameOverPanel;
    public Canvas Canvas;
    public GameObject ActionPanel;
    public GameObject HpBar;

    private GameObject _infoPanel;
    private GameObject _gameOverPanel;
    private GameObject _actionPanel;

    private GenericUnit currentUnit = new GenericUnit();

    private GameObject[] turnOrderPortraits = new GameObject[6];

    GameObject[] _bar;

    private RectTransform canvasTransform;

    void Start()
    {
        turnOrderPortraits[0] = Canvas.transform.Find("TurnOrder").Find("PortraitFrameMain").gameObject;
        turnOrderPortraits[1] = Canvas.transform.Find("TurnOrder").Find("Frame1").gameObject;
        turnOrderPortraits[2] = Canvas.transform.Find("TurnOrder").Find("Frame2").gameObject;
        turnOrderPortraits[3] = Canvas.transform.Find("TurnOrder").Find("Frame3").gameObject;
        turnOrderPortraits[4] = Canvas.transform.Find("TurnOrder").Find("Frame4").gameObject;
        turnOrderPortraits[5] = Canvas.transform.Find("TurnOrder").Find("Frame5").gameObject;

        canvasTransform = Canvas.GetComponent<RectTransform>();
        CellGrid.GameStarted += OnGameStarted;
        CellGrid.TurnEnded += OnTurnEnded;
        CellGrid.GameEnded += OnGameEnded;


    }

    private void Update()
    {
        for (int i = 0; i < CellGrid.Units.Count; i++)
        {
            Unit u = CellGrid.Units[i];
            if (!u.transform.GetComponentInChildren<SpriteRenderer>().isVisible)
            {
                _bar[i].SetActive(false);
            }
            else
            {
                _bar[i].SetActive(true);
            }
            Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(u.transform.position);
            Vector2 WorldObject_ScreenPosition = new Vector2(
            ((ViewportPosition.x * canvasTransform.sizeDelta.x) - (canvasTransform.sizeDelta.x * 0.5f)),
            ((ViewportPosition.y * canvasTransform.sizeDelta.y) - (canvasTransform.sizeDelta.y * 0.275f)));

            //set bars in canvas position
            _bar[i].GetComponent<RectTransform>().localPosition = WorldObject_ScreenPosition;
            RectTransform hpRect = _bar[i].transform.Find("HPBar").GetComponent<RectTransform>();
            RectTransform armorRect = _bar[i].transform.Find("ArmorBar").GetComponent<RectTransform>();

            hpRect.offsetMin = new Vector2(hpRect.offsetMin.x, 1.5f + 56.5f * (1 - (float)u.HitPoints / u.TotalHitPoints));
            armorRect.offsetMin = new Vector2(armorRect.offsetMin.x, 1.5f + 56.5f * (1 - (float)u.Armor / u.TotalArmor));
            _bar[i].transform.Find("HPText").GetComponent<Text>().text = u.HitPoints.ToString();
            _bar[i].transform.Find("ArmorText").GetComponent<Text>().text = u.Armor.ToString();

        }
        for (int i = CellGrid.Units.Count;  i < _bar.Length; i++)
        {
            _bar[i].SetActive(false);
        }
    }

    private void OnGameStarted(object sender, EventArgs e)
    {
        foreach (Transform unit in UnitsParent.transform)
        {
            unit.GetComponent<Unit>().UnitHighlighted += OnUnitHighlighted;
            unit.GetComponent<Unit>().UnitDehighlighted += OnUnitDehighlighted;
            unit.GetComponent<Unit>().UnitDestroyed += OnUnitDestroyed;
            unit.GetComponent<Unit>().UnitAttacked += OnUnitAttacked;
            unit.GetComponent<Unit>().UnitSelected += OnUnitSelected;

        }
        
        print("Unit: " + CellGrid.Units.Count);
        _bar = new GameObject[CellGrid.Units.Count];
        for (int i = 0; i < CellGrid.Units.Count; i++)
        {
            _bar[i] = Instantiate(HpBar);
            print("Bar Instantiated");
            _bar[i].transform.parent = Canvas.transform.Find("HPBarParent");
        }
        CellGrid.TurnEnded += OnNextTurn;

    }

    private void OnTurnEnded(object sender, EventArgs e)
    {
        NextTurnButton.interactable = ((sender as CellGrid).CurrentPlayer is HumanPlayer);
        UpdateTurnUI();
    }
    private void OnGameEnded(object sender, EventArgs e)
    {
        _gameOverPanel = Instantiate(GameOverPanel);
        _gameOverPanel.transform.Find("InfoText").GetComponent<Text>().text = "Player " + ((sender as CellGrid).CurrentPlayerNumber + 1) + "\nwins!";
        
        _gameOverPanel.transform.Find("DismissButton").GetComponent<Button>().onClick.AddListener(DismissPanel);
 
        _gameOverPanel.GetComponent<RectTransform>().SetParent(Canvas.GetComponent<RectTransform>(), false);

    }

    private void OnUnitAttacked(object sender, AttackEventArgs e)
    {
        if (!(CellGrid.CurrentPlayer is HumanPlayer)) return;

        OnUnitDehighlighted(sender, e);

        if ((sender as Unit).HitPoints <= 0) return;

        OnUnitHighlighted(sender, e);
    }
    private void OnUnitDestroyed(object sender, AttackEventArgs e)
    {

        Destroy(_infoPanel);
        print("unitdestroyed");



    }
    private void OnUnitDehighlighted(object sender, EventArgs e)
    {
        Destroy(_infoPanel);
        print("unitdehighlighted");

        LoadInfoPanel(currentUnit);
    }
    private void OnUnitHighlighted(object sender, EventArgs e)
    {
        print("unithighlighted");
        var unit = sender as GenericUnit;
        if (unit.UnitName == currentUnit.UnitName)
        {

        }
        else
        {
            Destroy(_infoPanel);

            LoadInfoPanel(unit);
        }

    }

    private void OnNextTurn(object sender, EventArgs e)
    {
        print("nextturn");
        Destroy(_infoPanel);


    }
    private void OnUnitSelected(object sender, EventArgs e)
    {
        print("unitselected");
        var unit = sender as GenericUnit;
        if (unit.UnitName != currentUnit.UnitName)
        {
            currentUnit = unit;
            LoadInfoPanel(unit);
        }


    }


    private void LoadInfoPanel(GenericUnit unit)
    {
        _infoPanel = Instantiate(InfoPanel);

        float hpScale = (float)((float)(unit).HitPoints / (float)(unit).TotalHitPoints);

        _infoPanel.transform.Find("Name").GetComponent<Text>().text = unit.UnitName;
        _infoPanel.transform.Find("Health").Find("Text").GetComponent<Text>().text = unit.HitPoints.ToString() + "/" + unit.TotalHitPoints.ToString();
        _infoPanel.transform.Find("Attack").Find("Text").GetComponent<Text>().text = unit.AttackFactor.ToString();
        _infoPanel.transform.Find("Armor").Find("Text").GetComponent<Text>().text = unit.Armor.ToString() + "/" + unit.TotalArmor;
        _infoPanel.transform.Find("Range").Find("Text").GetComponent<Text>().text = unit.AttackRange.ToString();
        _infoPanel.transform.Find("Speed").Find("Text").GetComponent<Text>().text = unit.Speed.ToString();

        _infoPanel.GetComponent<RectTransform>().SetParent(Canvas.GetComponent<RectTransform>(), false);
    }

    public void DismissPanel()
    {
        Destroy(_gameOverPanel);
    }
    public void RestartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
    private void UpdateTurnUI()
    {
        for (int i = 0; i<turnOrderPortraits.Length; i++)
        {
            print((CellGrid.turnIndex) % CellGrid.turnOrder.Length);
            int unitIndex = CellGrid.turnOrder[(CellGrid.turnIndex + i) % CellGrid.turnOrder.Length];
            turnOrderPortraits[i].transform.Find("Portrait").GetComponent<Image>().sprite = CellGrid.Units[unitIndex].transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite;
        }
    }

}

