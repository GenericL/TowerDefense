using System.Collections.Generic;
using System;
using System.Buffers;
using UnityEngine;
#nullable enable
public abstract class SimpleStat<T> : IStat<T> where T : struct, IStatModifierData<T>
{
    [Header("Stat Modifiers")]
    private readonly Dictionary<String, StatModifier<T>> modifiers = new();
    private readonly Dictionary<String, int> modifierAmount = new();

    [Header("Stat Values")]
    private bool isDirty;
    private float baseValue;
    private float currentValue;
    private int modifierCount;

    public event Action? OnValueChanged;
    public float BaseValue
    {
        get => baseValue;
        set => SetBaseValue(value);
    }
    public int ModifiersCount => modifierCount;
    public float FinalValue => GetFinalValue();

    [Header("IStat herency")]
    IReadOnlyList<IStat> IStat.Stats => Array.Empty<IStat>();
    IReadOnlyList<IStat<T>> IStat<T>.Stats => Array.Empty<IStat<T>>();
    IReadOnlyList<IReadOnlyStat<T>> IReadOnlyStat<T>.Stats => Array.Empty<IStat<T>>();
    IReadOnlyList<IReadOnlyStat> IReadOnlyStat.Stats => Array.Empty<IReadOnlyStat>();

    protected SimpleStat(float baseValue = 0)
    {
        this.baseValue = baseValue;
        currentValue = baseValue;
    }

    private void SetBaseValue(float value)
    {
       if(baseValue != value)
        {
            bool isDirty = SetBaseValue(value, baseValue, currentValue, out float newValue);
            baseValue = value;
            CheckValueChanged(isDirty, newValue);
        }
    }
    private float GetFinalValue()
    {
        if (isDirty)
        {
            isDirty = false;
            currentValue = baseValue;
            ClearCachedValues(baseValue);

            foreach (KeyValuePair<String, StatModifier<T>> item in modifiers)
            {
                for (int i = 0; i < modifierAmount[item.Key]; i++)
                {
                    AddOperation(item.Value, baseValue, currentValue, out currentValue);
                }
            }
        }
        return currentValue;
    }
    protected abstract bool AddOperation(StatModifier<T> modifier, float baseValue, float currentValue, out float newValue);
    protected abstract bool RemoveOperation(StatModifier<T> modifier, float baseValue, float currentValue, out float newValue);
    protected abstract bool SetBaseValue(float newBaseValue, float baseValue, float currentValue, out float newValue);
    protected virtual void ClearCachedValues(float baseValue) { }
    private void Add(StatModifier<T> modifier)
    {
        if(modifierAmount.TryGetValue(modifier.name, out int amount))
        {
            modifierAmount[modifier.name]++;
        }
        else
        {
            modifiers[modifier.name] = modifier;
            modifierAmount[modifier.name] = 1;
        }
        modifierCount++;
    }
    private bool Remove(StatModifier<T> modifier)
    {
        if (modifierAmount.TryGetValue(modifier.name, out int amount))
        {
            modifierCount--;
            if (amount > 1)
            {
                modifierAmount[modifier.name] = amount - 1;
                return true;
            }
            else
            {
                bool success = modifierAmount.Remove(modifier.name);
                if (success)
                     success = modifiers.Remove(modifier.name);
                return success;
            }
        }
        return false;
    }
    public void AddModifier(StatModifier<T> modifier)
    {
        Add(modifier);
        bool isDirty = AddOperation(modifier, baseValue, currentValue, out float newValue);
        CheckValueChanged(isDirty, newValue);
    }
    public void UpdateTimer()
    {
        foreach (var item in modifiers)
        {
            item.Value.UpdateTimer();
        }
    }
    public bool RemoveModifier(StatModifier<T> modifier)
    {
        if (Remove(modifier))
        {
            bool isDirty = RemoveOperation(modifier, baseValue, currentValue, out float newValue);
            CheckValueChanged(isDirty, newValue);
            return true;
        }
        return false;
    }
    public int RemoveAllModifiers<IEquatable>(IEquatable match) where IEquatable : IEquatable<StatModifier<T>>
    {
        int removedCount = 0;
        bool isDirty = false;
        float newValue = currentValue;
        int keysCount = modifiers.Count;

        StatModifier<T>[] keys = ArrayPool<StatModifier<T>>.Shared.Rent(keysCount);
        modifiers.Values.CopyTo(keys, 0);

        for (int i = 0; i < keysCount; i++)
        {
            StatModifier<T> modifier = keys[i];
            if (match.Equals(modifier) && modifierAmount.Remove(modifier.name, out int count) && modifiers.Remove(modifier.name))
            {
                for (int j = 0; j < count; j++)
                {
                    isDirty |= RemoveOperation(modifier, baseValue, newValue, out newValue);
                }
                removedCount += count;
            }
        }
        Array.Clear(keys, 0, keysCount); // Clear the used portion of the array
        ArrayPool<StatModifier<T>>.Shared.Return(keys);

        modifierCount -= removedCount;
        CheckValueChanged(isDirty, newValue);
        return removedCount;
    }
    public void Clear()
    {
        modifiers.Clear();
        modifierCount = 0;
        ClearCachedValues(baseValue);
        CheckValueChanged(false, baseValue);
    }
    public void SetModifiers(IList<StatModifier<T>> results)
    {
        foreach (KeyValuePair<String, StatModifier<T>> item in modifiers)
        {
            for (int i = 0; i < modifierAmount[item.Key]; i++)
            {
                results.Add(item.Value);
            }
        }
    }
    private void CheckValueChanged(bool newIsDirty, float newValue)
    {
        if (newIsDirty || currentValue != newValue)
        {
            isDirty |= newIsDirty;
            currentValue = newValue;
            OnValueChanged?.Invoke();
        }
    }

    
}
