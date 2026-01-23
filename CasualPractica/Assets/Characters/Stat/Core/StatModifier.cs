using System;
using UnityEngine;
#nullable enable
public readonly struct StatModifier<T> : IComparable<StatModifier<T>>, IEquatable<StatModifier<T>> where T : struct, IStatModifierData<T>
{
    public readonly float value;
    public readonly TurnTimer? timer;
    public readonly T data;

    private readonly int hashCode;

    public StatModifier(float value, T data, TurnTimer? timer = default)
    {
        this.value = value;
        this.data = data;
        this.timer = timer;
        hashCode = HashCode.Combine(value, data);
    }

    public int CompareTo(StatModifier<T> other)
    {
        int valueComparison = value.CompareTo(other.value);
        if (valueComparison == 0)
        {
            return data.CompareTo(other.data);
        }
        return valueComparison;
    }

    public bool Equals(StatModifier<T> other)
    {
        return value == other.value && data.Equals(other.data) && timer?.GetTurnsLeft() == other.timer?.GetTurnsLeft();
    }

    public override bool Equals(object obj)
    {
        return obj is StatModifier<T> other && Equals(other);
    }

    public override int GetHashCode()
    {
        return hashCode;
    }

    public void UpdateTimer()
    {
        timer?.Tick();
    }

    public static bool operator ==(StatModifier<T> left, StatModifier<T> right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(StatModifier<T> left, StatModifier<T> right)
    {
        return !left.Equals(right);
    }
}
