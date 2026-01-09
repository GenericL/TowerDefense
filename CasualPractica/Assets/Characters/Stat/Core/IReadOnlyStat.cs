using System;
using System.Collections.Generic;
using UnityEngine;
#nullable enable
public interface IReadOnlyStat
{
    event Action? OnValueChanged;

    IReadOnlyList<IReadOnlyStat> Stats { get; }

    float BaseValue { get; }
    float FinalValue { get; }
}

public interface IReadOnlyStat<T> : IReadOnlyStat where T : struct, IStatModifierData<T>
{
    new IReadOnlyList<IReadOnlyStat<T>> Stats { get; }

    int ModifiersCount { get; }

    void SetModifiers(IList<StatModifier<T>> results);
}
