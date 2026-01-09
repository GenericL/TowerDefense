using UnityEngine;

public class SimpleStatMult<T> : SimpleStat<T> where T : struct, IStatModifierData<T>
{
    private int zeroCheck;
    private float nonZeroValue;
    public SimpleStatMult(float baseValue = 1) : base(baseValue) 
    {
        ClearCachedValues(baseValue);
    }
    protected override bool AddOperation(StatModifier<T> modifier, float baseValue, float currentValue, out float newValue)
    {
        float value = 1+modifier.value;
        if (value == 0)
        {
            zeroCheck++;
        }
        else
        {
            nonZeroValue *= value;
        }
        newValue =  zeroCheck > 0 ? 0 : nonZeroValue;
        return false;
    }

    protected override bool RemoveOperation(StatModifier<T> modifier, float baseValue, float currentValue, out float newValue)
    {
        float value = 1 + modifier.value;
        if (value == 0)
        {
            zeroCheck--;
        }
        else
        {
            nonZeroValue /= value;
        }
        newValue = zeroCheck > 0 ? 0 : nonZeroValue;
        return false;
    }

    protected override bool SetBaseValue(float newBaseValue, float oldBaseValue, float currentValue, out float newValue)
    {
        if (oldBaseValue == 0)
        {
            nonZeroValue = newBaseValue;
        }
        else
        {
            nonZeroValue /= oldBaseValue;
        }
        if (newBaseValue == 0)
        {
            zeroCheck++;
        }
        else
        {
            nonZeroValue *= newBaseValue;
        }
        newValue = zeroCheck > 0 ? 0 : nonZeroValue;
        return false;
    }

    protected override void ClearCachedValues(float baseValue)
    {
        if (baseValue == 0)
        {
            zeroCheck = 1;
            nonZeroValue = 1;
        }
        else
        {
            zeroCheck = 0;
            nonZeroValue = baseValue;
        }
    }
}
