using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Playable : Character
{
    protected EnergySystem energySystem;
    public EnergySystem EnergySystem { get { return energySystem; } }
    public abstract void InitialSetup(Enemy[] enemies, Playable[] playables, ExtraActionManager extraActionManager);
    public abstract void InitialPasive(Enemy[] enemies, Playable[] playables, ExtraActionManager extraActionManager);

    internal void NotifyAbilityUsed(AbilityEffect abilityEffect, ExtraActionManager extraActionManager)
    {
        foreach (var type in abilityEffect.typeDMG)
        {
            extraActionManager.PassiveManager.OnDamageOnEnemyTypeActivated.Invoke(type);
        }
        extraActionManager.PassiveManager.OnElementOnEnemyTypeActivated.Invoke(abilityEffect.elementDMG);
    }
}
