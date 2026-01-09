using System;
using UnityEngine;
#nullable enable
public readonly struct StatModifierData : IStatModifierData<StatModifierData>
{
    public readonly StatModifierType type;
    public readonly object? source;
    private readonly int hashCode;

    public StatModifierData(StatModifierType type, object? source = default)
    {
        this.type = type;
        this.source = source;
        hashCode = HashCode.Combine(this.type, this.source);
    }

    public int CompareTo(StatModifierData other)
    {
        // Evitando usar el CompareTo(object)
        return ((int)type).CompareTo((int)other.type);
    }

    public bool Equals(StatModifierData other)
    {
        return type == other.type && source == other.source;
    }

    public override bool Equals(object obj)
    {
        return obj is StatModifierData other && Equals(other);
    }

    public override int GetHashCode()
    {
        return hashCode;
    }

    public static bool operator ==(StatModifierData left, StatModifierData right) { return left.Equals(right); }
    public static bool operator !=(StatModifierData left, StatModifierData right) { return !left.Equals(right); }
}
