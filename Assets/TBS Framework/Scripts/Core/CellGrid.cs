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

    public int[] turnOrder;
    public int[] turnOrderPlayerNumbers;
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
        int[] speed; //list of speeds of each unit to be used later

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
            speed = new int[Units.Count];

            turnOrder = new int[Units.Count];
            for (int i = 0; i < Units.Count; i++)
            {
                turnOrder[i] = i; //default before sorting
            }

            int stemp = 0;
            foreach (var unit in Units)
            {
                speed[stemp] = unit.Speed;
                unit.UnitClicked += OnUnitClicked;
                unit.UnitDestroyed += OnUnitDestroyed;
                stemp++;
            }
        }
        else
        {
            Debug.LogError("No IUnitGenerator script attached to cell grid");
            speed= new int[] { 0 };
        }
            
        if(Players.Count <=1)
        {
            isGameOver = true;
        }
        bool ttemp = true;

        while (ttemp) //sort turn orders
        {
            ttemp = false;
            for (int i =1; i < speed.Length; i++)
            {
                if(speed[i] > speed[i - 1])
                {
                    ttemp = true;
                    speed[i] += speed[i - 1];
                    speed[i - 1] = speed[i] - speed[i - 1];
                    speed[i] = speed[i] - speed[i - 1];

                    turnOrder[i] += turnOrder[i - 1];
                    turnOrder[i - 1] = turnOrder[i] - turnOrder[i - 1];
                    turnOrder[i] = turnOrder[i] - turnOrder[i - 1];
                    
                }
            }
        }
        

        StartGame();
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
        int k = 0; //which unit index is dead
        for(int i =0; i < Units.Count(); i++)
        {
            if (sender.Equals(Units[i]))
            {
                k = i;
                print("value of k is " + k.ToString());
                print(Units[i].name);
            }
        }
        Units.Remove(sender as Unit);

        int newSize = turnOrder.Length - 1; //resizing turnOrder and turnOrderPlayerNumbers
        int[] tempArray = new int[newSize];

        int j = 0;
        int l = 0;
        for (int i = 0; i < newSize; i++)
        {
            if (turnOrder[i] != (k))
            {
                tempArray[i] = turnOrder[i + j];
            }
            else
            {
                j++;
                l = i;
                print("removing: " + i.ToString());
            }
        }
        turnOrder = tempArray;
        

        j = 0;
        for (int i = 0; i < newSize; i++)
        {
            if (i != (l ))
            {
                tempArray[i] = turnOrderPlayerNumbers[i + j];
            }
            else j++;
        }
        turnOrderPlayerNumbers = tempArray;
        foreach (var unit in turnOrder)
        {
            print("turnOrder left: " + unit.ToString());
        }
        foreach (var unit in turnOrderPlayerNumbers)
        {
            print("PlayerNumbers left: " + unit.ToString());
        }

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
    
    /// <summary>
    /// Method is called once, at the beggining of the game.
    /// </summary>
    public void StartGame()
    {
        if(GameStarted != null)
            GameStarted.Invoke(this, new EventArgs());

        //Units.FindAll(u => u.PlayerNumber.Equals(CurrentPlayerNumber)).ForEach(u => { u.OnTurnStart(); });
        //Players.Find(p => p.PlayerNumber.Equals(CurrentPlayerNumber)).Play(this);
        CurrentPlayer.isPlaying = true;

        turnOrderPlayerNumbers = new int[turnOrder.Length];

        for (int i= 0; i < turnOrderPlayerNumbers.Length; i++)
        {
            turnOrderPlayerNumbers[i] = Units[turnOrder[i]].PlayerNumber; //list of whether or not unit is ai 
        }
        lastPlayer = turnOrderPlayerNumbers[0];
        TurnEnded += TurnCycle;
        Players[turnOrderPlayerNumbers[turnIndex]].Play(this);
        TurnCycleInvoke();

    }
    /// <summary>
    /// Method makes turn transitions. It is called by player at the end of his turn.
    /// </summary>

    public void TurnCycle(object sender, System.EventArgs e)//code for 1 turn
    {
        turnIndex = turnIndex % turnOrder.Length;
        Units[turnOrder[turnIndex]].OnTurnStart();
        print("turnIndex " + turnIndex.ToString());
        print("turnOrderPlayerNumber " + turnOrderPlayerNumbers[turnIndex].ToString());
        print("Selected unit " + Units[turnOrder[turnIndex]].name);

        if (turnOrderPlayerNumbers[turnIndex] == 0) //check if player or ai's turn
            {
                
                CellGridState.OnUnitClicked(Units[turnOrder[turnIndex]]);
                turnIndex++;
                
            }
       else
            {
                Players[1].GetComponent<NaiveAiPlayer>().SinglePlay(this, Units[turnOrder[turnIndex]]);
                turnIndex++;
            }

    }

    public void TurnCycleInvoke() //generates event that triggers turncycle
    {
        if (TurnEnded != null)
        {
            TurnEnded(this, new EventArgs());
        }
    }
    /*public void EndTurn()
    {
        if (Units.Select(u => u.PlayerNumber).Distinct().Count() == 1)
        {
            return;
        }
        CellGridState = new CellGridStateTurnChanging(this);

        Units.FindAll(u => u.PlayerNumber.Equals(CurrentPlayerNumber)).ForEach(u => { u.OnTurnEnd(); });

        CurrentPlayerNumber = (CurrentPlayerNumber + 1) % NumberOfPlayers;
        while (Units.FindAll(u => u.PlayerNumber.Equals(CurrentPlayerNumber)).Count == 0)
        {
            CurrentPlayerNumber = (CurrentPlayerNumber + 1)%NumberOfPlayers;
        }//Skipping players that are defeated.
        CurrentPlayer.isPlaying = false;
        if (TurnEnded != null)
            TurnEnded.Invoke(this, new EventArgs());
        
        Units.FindAll(u => u.PlayerNumber.Equals(CurrentPlayerNumber)).ForEach(u => { u.OnTurnStart(); });
        Players.Find(p => p.PlayerNumber.Equals(CurrentPlayerNumber)).Play(this);     
        CurrentPlayer.isPlaying = true;
    }*/
    public void EndTurn() //goes to next unit's turn.
    {
        int lastUnit = (turnIndex - 1) % turnOrder.Length;
        //Units[turnOrder[turnIndex]].SetState(new UnitStateMarkedAsFinished(Units[turnOrder[turnIndex]]));
        Units[turnOrder[lastUnit]].OnTurnEnd();
        /*if (CellDehighlighted != null)
            CellDehighlighted.Invoke(this, new EventArgs());*/
        if (turnOrderPlayerNumbers[lastUnit] != lastPlayer)
        {
            Players[turnOrderPlayerNumbers[turnIndex%turnOrder.Length]].Play(this);
        }
        TurnCycleInvoke();
    }
    //Transform cameraMove = Camera.main.gameObject.transform;

}
