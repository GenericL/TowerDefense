using System;

public abstract class Stat : Stat<StatModifierData>
{
    public Stat(float baseValue = 0) : base(baseValue, GetStats())
    {
    }

    public int RemoveModifiersFromSource(object source)
    {
        return RemoveAllModifiers(new StatModifierMatch(source: source));
    }

    public override void AddModifier(StatModifier<StatModifierData> modifier)
    {
        stats[(int) modifier.data.type].AddModifier(modifier);
    }
    public override void UpdateTimer()
    {
        foreach (var item in stats)
        {
            item.UpdateTimer();
        }
    }
    public override bool RemoveModifier(StatModifier<StatModifierData> modifier)
    {
        return stats[(int)modifier.data.type].RemoveModifier(modifier);
    }
    public abstract override float CalculateFinalValue(float baseValue);

    private static readonly StatModifierType[] modifierTypes = (StatModifierType[])Enum.GetValues(typeof(StatModifierType));
    private static IStat<StatModifierData>[] GetStats()
    {
        IStat<StatModifierData>[] stats = new IStat<StatModifierData>[modifierTypes.Length];

        for (int i = 0; i < stats.Length; i++)
        {
            stats[i] = modifierTypes[i] switch
            {
                StatModifierType.ADD_BASE => new SimpleStatAdd<StatModifierData>(0),
                StatModifierType.MULT => new SimpleStatAdd<StatModifierData>(1),
                StatModifierType.ADD_FLAT => new SimpleStatAdd<StatModifierData>(0),
                StatModifierType.MULT_TOTAL => new SimpleStatMult<StatModifierData>(1),
                StatModifierType.MAX => new SimpleStatMax<StatModifierData>(float.MinValue),
                StatModifierType.MIN => new SimpleStatMin<StatModifierData>(float.MaxValue),
                _ => throw new NotImplementedException()
            };
        }
        return stats;
    }
}
