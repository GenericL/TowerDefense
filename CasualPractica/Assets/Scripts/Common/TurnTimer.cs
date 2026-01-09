

public class TurnTimer
{
    private readonly bool timer;
    private int turnsLeft;

    public TurnTimer() : this(false, 0){}

    public TurnTimer(bool timer, int turnsLeft)
    {
        this.timer = timer;
        this.turnsLeft = turnsLeft;
    }

    public bool tick()
    {
        if (timer)
        {
            turnsLeft--;
            return true;
        }
        return false;
    }

    public int getTurnsLeft() { return turnsLeft; }
}
