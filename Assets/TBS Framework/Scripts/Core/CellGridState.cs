using System.Linq;

public abstract class CellGridState
{
    protected CellGrid _cellGrid;

    public bool isAttacking { get; set; }
    public bool attackingHealth { get; set; }
    public bool isTrueDamage = false;
    public bool usingGun = false;

    protected CellGridState(CellGrid cellGrid)
    {
        _cellGrid = cellGrid;
    }

    public virtual void OnUnitClicked(Unit unit)
    { }
    
    public virtual void OnCellDeselected(Cell cell)
    {
        cell.UnMark();
    }
    public virtual void OnCellSelected(Cell cell)
    {
        cell.Mark(Cell.HighlightState.Highlighted);
    }
    public virtual void OnCellClicked(Cell cell)
    {
        if (cell.unit != null)
        {
            OnUnitClicked(cell.unit);
        }
    }

    public virtual void OnStateEnter()
    {
        if (_cellGrid.Units.Select(u => u.PlayerNumber).Distinct().ToList().Count == 1)
        {
            _cellGrid.CellGridState = new CellGridStateGameOver(_cellGrid);
        }
    }
    public virtual void OnStateExit()
    {
    }
}