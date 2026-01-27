

public class TurnTimer
{
    private readonly bool timer;
    private int originalTurn;
    private int turnsLeft;

    public TurnTimer() : this(false, 0){}

    public TurnTimer(bool timer, int turnsLeft)
    {
        this.timer = timer;
        this.originalTurn = turnsLeft;
        this.turnsLeft = turnsLeft;
    }

    public bool Tick()
    {
        if (timer)
        {
            turnsLeft--;
            return turnsLeft==0;
        }
        return false;
    }
    public void ResetTimer()
    {
        if (timer)
        {
            turnsLeft = originalTurn;
        }
    }
    public int GetTurnsLeft() { return turnsLeft; }
}
