using UnityEngine;

// En referancia a las stats más basicas: HP, ATK, DEF, SPD, etc...
public class BasicStatType : Stat
{
    public BasicStatType(float baseValue = 0) : base(baseValue){}

    public override float CalculateFinalValue(float baseValue)
    {
        // ((base+addBase)*(1+modificadorTotal)+addFlat)*modificadorExterno
        float finalValue = baseValue;
        finalValue += stats[(int)StatModifierType.ADD_BASE].FinalValue;
        finalValue *= stats[(int)StatModifierType.MULT_TOTAL].FinalValue;
        finalValue += stats[(int)StatModifierType.ADD_FLAT].FinalValue;
        finalValue *= stats[(int)StatModifierType.MULT].FinalValue;
        return finalValue;
    }
}
