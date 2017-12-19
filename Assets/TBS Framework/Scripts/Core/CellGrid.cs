using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

/// <summary>
/// CellGrid class keeps track of the game, stores cells, units and players objects. It starts the game and makes turn transitions. 
/// It reacts to user interacting with units or cells, and raises events related to game progress. 
/// </summary>
public class CellGrid : MonoBehaviour
{
    public event EventHandler GameStarted;
    public event EventHandler GameEnded;
    public event EventHandler TurnEnded;

    public bool isGameOver = false;

    private CellGridState _cellGridState;//The grid delegates some of its behaviours to cellGridState object.
    public CellGridState CellGridState
    {
        private get
        {
            return _cellGridState;
        }
        set
        {
            if(_cellGridState != null)
                _cellGridState.OnStateExit();
            _cellGridState = value;
            _cellGridState.OnStateEnter();
        }
    }

    public int NumberOfPlayers { get; private set; }

    public Player CurrentPlayer
    {
        get { return Players.Find(p => p.PlayerNumber.Equals(CurrentPlayerNumber)); }
    }
    public int CurrentPlayerNumber { get; private set; }

    public Transform PlayersParent;

    public List<Player> Players { get; private set; }
    public List<Cell> Cells { get; private set; }
    public List<Unit> Units { get; private set; }
    public object PlayerNumber { get; private set; }

    public List<Unit> unitTurnOrder = new List<Unit>();

    public List<int> turnOrder = new List<int>();
    public List<int> turnOrderPlayerNumbers;
    public int turnIndex = 0;
    public List<Unit>myUnits;
    public int lastPlayer; //ai or player

    void Start()
    {
        
        Players = new List<Player>();
        for (int i = 0; i < PlayersParent.childCount; i++)
        {
            var player = PlayersParent.GetChild(i).GetComponent<Player>();
            if (player != null)
                Players.Add(player);
            else
                Debug.LogError("Invalid object in Players Parent game object");
        }
        NumberOfPlayers = Players.Count;

        CurrentPlayerNumber = Players.Min(p => p.PlayerNumber);
        

        Cells = new List<Cell>();
        for (int i = 0; i < transform.childCount; i++)
        {
            var cell = transform.GetChild(i).gameObject.GetComponent<Cell>();
            if (cell != null)
                Cells.Add(cell);
            else
                Debug.LogError("Invalid object in cells paretn game object");
        }
      
        foreach (var cell in Cells)
        {
            cell.CellClicked += OnCellClicked;
            cell.CellHighlighted += OnCellHighlighted;
            cell.CellDehighlighted += OnCellDehighlighted;
        }
             
        var unitGenerator = GetComponent<IUnitGenerator>();
        if (unitGenerator != null)
        {
            Units = unitGenerator.SpawnUnits(Cells);

            foreach (var unit in Units)
            {
                unit.UnitClicked += OnUnitClicked;
                unit.UnitDestroyed += OnUnitDestroyed;
            }
        }
        else
        {
            Debug.LogError("No IUnitGenerator script attached to cell grid");
        }

        if (Players.Count <= 1)
        {
            isGameOver = true;
        }    
        OrderSpeed();


        StartGame();
    }

    void OrderSpeed()
    {
        unitTurnOrder = Units.OrderBy(o => -o.Speed).ToList();
        Unit shifted = unitTurnOrder[unitTurnOrder.Count-1];
        unitTurnOrder.RemoveAt(unitTurnOrder.Count -1);
        unitTurnOrder.Insert(0, shifted);
    }

    private void OnCellDehighlighted(object sender, EventArgs e)
    {
        CellGridState.OnCellDeselected(sender as Cell);
    }
    private void OnCellHighlighted(object sender, EventArgs e)
    {
        CellGridState.OnCellSelected(sender as Cell);
    } 
    private void OnCellClicked(object sender, EventArgs e)
    {
        CellGridState.OnCellClicked(sender as Cell);
    }

    private void OnUnitClicked(object sender, EventArgs e)
    {
        print("onunitclicked");
        GenericUnit unit = sender as GenericUnit;
        if (unit.PlayerNumber == 0)
        {
        }
        else { CellGridState.OnUnitClicked(sender as Unit); }
    }

    private void OnUnitDestroyed(object sender, AttackEventArgs e)
    {
        Units.Remove(sender as Unit);

        unitTurnOrder.Remove(sender as Unit);


        var totalPlayersAlive = Units.Select(u => u.PlayerNumber).Distinct().ToList(); //Checking if the game is over
        if (totalPlayersAlive.Count == 1)
        {
            if(GameEnded != null)
            {
                GameEnded.Invoke(this, new EventArgs());
                isGameOver = true;
            }
        }
    }

    public void AttackHealth()
    {
        print("Attacking Health...");
        _cellGridState.isAttacking = true;
        _cellGridState.attackingHealth = true;
        
    }

    public void AttackArmor()
    {
        print("Attacking Armor...");
        _cellGridState.isAttacking = true;
        _cellGridState.attackingHealth = false;
    }

    public void ShootGun()
    {
        
        _cellGridState.isAttacking = true;
        _cellGridState.attackingHealth = true;
        _cellGridState.isTrueDamage = true;
        _cellGridState.usingGun = true;
    }

    /// <summary>
    /// Method is called once, at the beggining of the game.
    /// </summary>
    public void StartGame()
    {
        if(GameStarted != null)
            GameStarted.Invoke(this, new EventArgs());

        CurrentPlayer.isPlaying = true;
        
        TurnEnded += TurnCycle;
        Players[unitTurnOrder[0].PlayerNumber].Play(this);

        TurnCycleInvoke();

    }
    /// <summary>
    /// Method makes turn transitions. It is called by player at the end of his turn.
    /// </summary>

    public void TurnCycle(object sender, System.EventArgs e)//code for a single turn of a unit
    {

        Unit shifted = unitTurnOrder[0];
        unitTurnOrder.RemoveAt(0);
        unitTurnOrder.Add(shifted);

        unitTurnOrder[0].OnTurnStart();


        if(unitTurnOrder[0].PlayerNumber == 0)
        {
            Players[0].Play(this);
            CellGridState.OnUnitClicked(unitTurnOrder[0]);
        }
        else
        {
            Players[1].GetComponent<NaiveAiPlayer>().SinglePlay(this, unitTurnOrder[0]);
        }
    }

    public void TurnCycleInvoke() //generates event that triggers the turn cycle
    {
        if (TurnEnded != null)
        {
            TurnEnded(this, new EventArgs());
        }
    }

    public void EndTurn() //goes to next unit's turn.
    {

        unitTurnOrder[0].OnTurnEnd();
        
        TurnCycleInvoke();
    }
}
