using System;

public class ShieldEffect : IEquatable<Character>, IComparable<ShieldEffect>
{
    private readonly Character source;
    private float amount;

    public ShieldEffect(Character source, float amount)
    {
        this.source = source;
        this.amount = amount;
    }

    public float GetAmount() { return amount; }
    public void AddAmount(float amount) { this.amount += amount; }
    public float RemoveAmount(float amount) { return this.amount += amount; }

    public bool Equals(Character other)
    {
        return other == source || other.GetCharacterData().GetCharacterName().Equals(source.GetCharacterData().GetCharacterName());
    }
    public Character GetCharacter() { return source; }

    public int CompareTo(ShieldEffect other)
    {
        int valueCompare = source.Equals(other.GetCharacter())? 1:0;
        if(valueCompare == 0)
        {
            return amount > other.amount ? 1 : 0;
        }
        return valueCompare;
    }
}
