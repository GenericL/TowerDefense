using System;
using System.Collections.Generic;
using UnityEngine;
#nullable enable
public abstract class Stat<T> : IStat<T> where T : struct, IStatModifierData<T>
{
    protected readonly IStat<T>[] stats;

    private bool isDirty;
    [SerializeReference] private float baseValue;
    private float currentValue;

    public event Action? OnValueChanged;

    public float BaseValue
    {
        get => baseValue;
        set => SetBaseValue(value);
    }
    public float FinalValue => GetFinalValue();
    public int ModifiersCount => GetModifiersCount();

    public IReadOnlyList<IStat<T>> Stats => stats;

    IReadOnlyList<IStat> IStat.Stats => Stats;
    IReadOnlyList<IReadOnlyStat<T>> IReadOnlyStat<T>.Stats => Stats;
    IReadOnlyList<IReadOnlyStat> IReadOnlyStat.Stats => Stats;

    protected Stat(float baseValue = 0, params IStat<T>[] stats)
    {
        this.stats = stats;
        this.baseValue = baseValue;
        currentValue = CalculateFinalValue(baseValue);

        Action onChangedDelegate = OnChanged;

        foreach (var stat in stats)
        {
            stat.OnValueChanged += onChangedDelegate;
        }
    }

    private void OnChanged()
    {
        isDirty = true;
        OnValueChanged?.Invoke();
    }
    private void SetBaseValue(float value)
    {
        if (baseValue != value)
        {
            isDirty = true;
            baseValue = value;
            OnValueChanged?.Invoke();
        }
    }
    private int GetModifiersCount()
    {
        int count = 0;
        foreach (var stat in stats)
        {
            count += stat.ModifiersCount;
        }
        return count;
    }
    private float GetFinalValue()
    {
        if (isDirty)
        {
            isDirty = false;
            currentValue = CalculateFinalValue(baseValue);
        }
        return currentValue;
    }

    public abstract void AddModifier(StatModifier<T> modifier);
    public abstract bool RemoveModifier(StatModifier<T> modifier);
    public abstract void UpdateTimer();
    public abstract float CalculateFinalValue(float baseValue);
    public int RemoveAllModifiers<TEquatable>(TEquatable match) where TEquatable : IEquatable<StatModifier<T>>
    {
        int removedCount = 0;
        for (int i = 0; i < stats.Length; i++)
        {
            removedCount += stats[i].RemoveAllModifiers(match);
        }
        return removedCount;
    }

    public void Clear()
    {
        for (int i = 0; i < stats.Length; i++)
        {
            stats[i].Clear();
        }
    }

    public void SetModifiers(IList<StatModifier<T>> results)
    {
        for (int i = 0; i < stats.Length; i++)
        {

            stats[i].SetModifiers(results);
        }
    }
   
}
