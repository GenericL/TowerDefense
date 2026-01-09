using System.Collections.Generic;
using System;

public interface IStat : IReadOnlyStat
{
    new IReadOnlyList<IStat> Stats { get; }
    new float BaseValue { get; set; }
    void Clear();
}

public interface IStat<T> : IStat, IReadOnlyStat<T> where T : struct, IStatModifierData<T>
{
    new IReadOnlyList<IStat<T>> Stats { get; }
    void AddModifier(StatModifier<T> modifier);
    bool RemoveModifier(StatModifier<T> modifier);
    void UpdateTimer();
    int RemoveAllModifiers<TEquatable>(TEquatable match) where TEquatable : IEquatable<StatModifier<T>>;
}
