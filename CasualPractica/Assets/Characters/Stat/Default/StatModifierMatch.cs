using System;
using UnityEngine;

public readonly struct StatModifierMatch : IEquatable<StatModifier<StatModifierData>>
{
    public readonly ValueContainer<float> modifierValue;
    public readonly ValueContainer<StatModifierType> type;
    public readonly ValueContainer<object> source;

    public StatModifierMatch(ValueContainer<float> modifierValue = default, ValueContainer<StatModifierType> type = default, ValueContainer<object> source = default)
    {
        this.modifierValue = modifierValue;
        this.type = type;
        this.source = source;
    }
    
    public bool Equals(StatModifier<StatModifierData> other)
    {
        return (!modifierValue.hasValue || modifierValue.value == other.value)
            && (!type.hasValue || type.value == other.data.type)
            && (!source.hasValue || source.value == other.data.source);
    }
}
public readonly struct ValueContainer<T>
{
    public readonly T value;
    public readonly bool hasValue;

    public ValueContainer(T value)
    {
        this.value = value;
        hasValue = true;
    }

    public static implicit operator ValueContainer<T>(T value) => new(value);
}
