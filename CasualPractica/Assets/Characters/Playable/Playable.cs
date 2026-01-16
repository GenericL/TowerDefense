using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Playable : Character
{
    protected EnergySystem energySystem;
    public EnergySystem EnergySystem { get { return energySystem; } }
    public abstract void InitialPasive();

    internal void NotifyAbilityUsed(AbilityEffect abilityEffect)
    {
        foreach (var type in abilityEffect.typeDMG)
        {
            PassiveManager.i.onDamageOnEnemyTypeActivated.Invoke(type);
        }
        PassiveManager.i.onElementOnEnemyTypeActivated.Invoke(abilityEffect.elementDMG);
    }
}
