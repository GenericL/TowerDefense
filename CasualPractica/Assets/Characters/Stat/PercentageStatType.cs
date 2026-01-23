using UnityEngine;

// En referancia a las stats que no tienen añadidos del tipo flat y son porcentajes: Crit, RES, BonusDMG, etc...
public class PercentageStatType : Stat
{
    public PercentageStatType(float baseValue = 0) : base(baseValue) { }
    public override float CalculateFinalValue(float baseValue)
    {
        // base + modificadorTotal + modificadorExterno
        float finalValue = baseValue;
        finalValue += (1-stats[(int)StatModifierType.MULT].FinalValue);
        finalValue += (1 - stats[(int)StatModifierType.MULT_TOTAL].FinalValue);
        return finalValue;
    }
}
