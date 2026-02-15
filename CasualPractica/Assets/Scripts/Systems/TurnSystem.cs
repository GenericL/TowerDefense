

using UnityEngine;

public class TurnSystem
{
    private float avanceValue;

    public bool CanObtainTurn(float speed)
    {
        avanceValue += speed;
        if (NumericConstants.AVANCE_TURN_VALUE < avanceValue)
        {
            avanceValue = 0;
            return true;
        }
        return false;
    }
    public bool AvanceAction(float avanceValue) 
    {
        avanceValue += NumericConstants.AVANCE_TURN_VALUE * avanceValue;
        if (NumericConstants.AVANCE_TURN_VALUE < avanceValue)
        {
            avanceValue -= NumericConstants.AVANCE_TURN_VALUE;
            return true;
        }
        return false;
    }
    public void Reset() { avanceValue = 0f; }
    public int GetAvanceAproximate() { return (int)(NumericConstants.AVANCE_TURN_VALUE/avanceValue); }
}
