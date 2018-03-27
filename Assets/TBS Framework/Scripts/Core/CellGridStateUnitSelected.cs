using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;


class CellGridStateUnitSelected : CellGridState
{
    
    private Unit _unit { get; set; }
    private List<Cell> _pathsInRange;
    private List<Unit> _unitsInRange;
    private List<Cell> _attackableInRange;
    public Boolean firstTurn = true;
    private Cell _unitCell;


    public CellGridStateUnitSelected(CellGrid cellGrid, Unit unit) : base(cellGrid)
    {
        _unit = unit;
        _pathsInRange = new List<Cell>();
        _attackableInRange = new List<Cell>();
        _unitsInRange = new List<Unit>();
    }

    public override void OnCellClicked(Cell cell)
    {

        if (_unit.isMoving)
            return;
        if (_cellGrid.CurrentUnit.AttackAOE != 1)
        {
            Debug.Log("AOE: " + _cellGrid.CurrentUnit.AttackAOE);
            Unit currentUnit = _cellGrid.CurrentUnit;
            List<Cell> affectedCells = _cellGrid.Cells.FindAll(c => _cellGrid.CurrentUnit.IsCellAttackable(c) && c.GetDistance(cell) < _cellGrid.CurrentUnit.AttackAOE);
            affectedCells.Add(cell);
            
            List<Unit> affectedUnits = new List<Unit>();
            
            foreach (Cell c in affectedCells)
            {
                if (c.unit != null)
                {
                    affectedUnits.Add(c.unit);
                }

            }
            if (affectedUnits.Count > 0)
            {
                _unit.ActionPoints = affectedUnits.Count;
            }
            
            Debug.Log("multiple units Handling" + affectedUnits.Count);
            HandleAttack(affectedUnits);
        }
        else
        {
            if (cell.IsTaken)
            {
                if (cell.unit != null)
                {
                    OnUnitClicked(cell.unit);
                }
            }
            if (!_pathsInRange.Contains(cell))
            {
                //_cellGrid.CellGridState = new CellGridStateWaitingForInput(_cellGrid);
            }
            else
            {
                var path = _unit.FindPath(_cellGrid.Cells, cell);
                _unit.Move(cell, path);
                _cellGrid.CellGridState = new CellGridStateUnitSelected(_cellGrid, _unit);
            }
        }
        
        
            
        
    }

    public override void OnUnitClicked(Unit unit)
    {
        if (_unit.ActionPoints > 0 && isAttacking == true)
        {
            if (_cellGrid.CurrentUnit.AttackAOE == 1)
            {
                if (_unitsInRange.Contains(unit))
                {
                    List<Unit> units = new List<Unit>();
                    units.Add(unit);
                    HandleAttack(units);
                }
            }
            else
            {
                OnCellClicked(unit.Cell);
                
            }
        }
    }

    public void HandleAttack(List<Unit> units)
    {
        foreach (Unit unit in units)
        {
            Debug.Log("onUnitClicked branch: ");
            if (unit.Equals(_unit) || unit.isMoving)
            {
                Debug.Log("self attack?");
                return;
            }
            Debug.Log("_unit name " + _unit.name);
            Debug.Log("attacked name " + unit.name);
            if (usingGun)
            {
                _unit.AttackFactor = _unit.GunAttack;
            }
            _unit.DealDamage(unit, attackingHealth, isTrueDamage);
            
        }
        _cellGrid.CellGridState = new CellGridStateUnitSelected(_cellGrid, _unit);
        isAttacking = false;
        isTrueDamage = false;
        usingGun = false;
        _unit.AttackFactor = _unit.BaseAttack;
        _unit.AttackAOE = 1;
        if (_unit.ActionPoints == 0 && !_cellGrid.isActionDone)
        {
            Debug.Log("ENDING TURN>>>");
            _cellGrid.EndTurn();
        }
    }


    public override void OnCellDeselected(Cell cell)
    {
        base.OnCellDeselected(cell);

        foreach (var _cell in _attackableInRange)
        {
            _cell.Mark(Cell.HighlightState.Attackable);
        }
        foreach (var _cell in _pathsInRange)
        {
            _cell.Mark(Cell.HighlightState.Reachable);
        }
        foreach (var _cell in _cellGrid.Cells.Except(_attackableInRange))
        {
            _cell.UnMark();
        }
    }
    public override void OnCellSelected(Cell cell)
    {
        base.OnCellSelected(cell);

        if (firstTurn == true)
        {
            _pathsInRange = _unit.GetAvailableDestinations(_cellGrid.Cells);
            _attackableInRange = _unit.GetAvailableAttacks(_cellGrid.Cells);
            var cellsNotInRange = _cellGrid.Cells.Except(_pathsInRange);
            firstTurn = false;
        }
        if (_pathsInRange.Contains(cell) && _unit.AttackAOE == 1)
        {
            var path = _unit.FindPath(_cellGrid.Cells, cell);
            foreach (var _cell in path)
            {
                _cell.Mark(Cell.HighlightState.Path);
            }
        }
        else if (_attackableInRange.Contains(cell))
        {
            cell.Mark(Cell.HighlightState.AttackSelected);
            List<Cell> affectedCells = _cellGrid.Cells.FindAll(c => _cellGrid.CurrentUnit.IsCellAttackable(c) && c.GetDistance(cell) < _cellGrid.CurrentUnit.AttackAOE);
            Debug.Log("Highlighted Lots" + affectedCells.Count);
            foreach (Cell c in affectedCells)
            {
                c.Mark(Cell.HighlightState.AttackSelected);
            }
        }
        else
            return;

        
    }

    public override void OnStateEnter() //beginning of a unit's turn
    {

        base.OnStateEnter();
        
        _unit.OnUnitSelected();
        _unitCell = _unit.Cell;

        _pathsInRange = _unit.GetAvailableDestinations(_cellGrid.Cells);
        _attackableInRange = _unit.GetAvailableAttacks(_cellGrid.Cells);
        var cellsNotInRange = _cellGrid.Cells.Except(_pathsInRange);
        foreach (var cell in cellsNotInRange)
        {
            cell.UnMark();
        }
        foreach (var cell in _attackableInRange)
        {
            cell.Mark(Cell.HighlightState.Attackable);
        }
        foreach (var cell in _pathsInRange)
        {
            cell.Mark(Cell.HighlightState.Reachable);
        }

        if (_unit.ActionPoints <= 0) return;

        foreach (var currentUnit in _cellGrid.Units)
        {
            if (currentUnit.PlayerNumber.Equals(_unit.PlayerNumber))
                continue;
        
            if (_unit.IsUnitAttackable(currentUnit,_unit.Cell))
            {
                currentUnit.SetState(new UnitStateMarkedAsReachableEnemy(currentUnit));
                _unitsInRange.Add(currentUnit);
            }
        }
        
        if (_unitCell.GetNeighbours(_cellGrid.Cells).FindAll(c => c.MovementCost <= _unit.MovementPoints).Count == 0 
            && _unitsInRange.Count == 0)
        {
            _unit.SetState(new UnitStateMarkedAsFinished(_unit));

        }
            


    }
    public override void OnStateExit()
    {
        _unit.OnUnitDeselected();
        foreach (var unit in _unitsInRange)
        {
            if (unit == null) continue;
            unit.SetState(new UnitStateNormal(unit));
        }
        foreach (var cell in _cellGrid.Cells)
        {
            cell.UnMark();
        }   
    }

    public bool IsUnitInRange(Unit unit)
    {
        if (_unitsInRange.Contains(unit))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

