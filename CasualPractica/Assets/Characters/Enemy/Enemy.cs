using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Character
{
    private int sequence = 0;
    [SerializeField] protected HealthBar healthBar;

    public abstract bool DoTurn(Character[] targets, int principalTarget);
    public int GetSequence() { return sequence; }
    public void SetSequence(int sequence) { this.sequence = sequence; }
    public void IncrementSequence() {  sequence++; }
    public void ResetSequence() {  sequence = 0; }

    public void SetMaxHealth(float health) { healthBar.SetMaxHealth(health); }
    public void SetHealth(float health) { healthBar.SetHealth(health); }
}
