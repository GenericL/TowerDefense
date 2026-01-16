using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Character
{
    private int sequence = 0;
    [SerializeField] protected HealthBar healthBar;

    protected void Start()
    {
        GetCharacterData().SetHealthEvent(healthBar.SetHealth);
    }
    public int GetSequence() { return sequence; }
    public void SetSequence(int sequence) { this.sequence = sequence; }
    public void IncrementSequence() {  sequence++; }
    public void ResetSequence() {  sequence = 0; }

    public override void Dies()
    {
        base.Dies();
        healthBar.gameObject.SetActive(false);
    }

    internal void NotifyAbilityUsed(AbilityEffect abilityEffect)
    {
        
    }
}
