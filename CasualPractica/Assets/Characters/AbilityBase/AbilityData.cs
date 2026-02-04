using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Characters/AbilityBase/AbilityData", menuName = "Characters/AbilityData")]
public class AbilityData : ScriptableObject
{
    public string label;
    [SerializeReference] public List<AbilityEffect> effects;

    void OnEnable()
    {
        if (string.IsNullOrEmpty(label)) label = name;
        if (effects == null) effects = new List<AbilityEffect>();
    }
}

[Serializable]
public abstract class AbilityEffect
{
    public ElementType elementDMG;
    public KitMultiplierStatType kitMultiplierType;
    public List<DamageType> typeDMG;

    public abstract void Execute(Character caster, Character[] targets, int principalTarget, ExtraActionManager extraActionManager);
    protected void Damage(Character caster, Character target, float damagePercent, ExtraActionManager extraActionManager)
    {
        float finalDamage = Formulas.i.Damage(caster, target, damagePercent, elementDMG, kitMultiplierType);
        bool defeated = target.GetCharacterData().DamageRecieved(finalDamage);
        if (defeated)
        {
            target.Dies(extraActionManager);
        }
    }
    protected void Heal(Character caster, Character target, float healPercent, float flatHeal)
    {
        float finalHeal = Formulas.i.Heal(caster, target, healPercent, flatHeal, kitMultiplierType);
        target.GetCharacterData().HealingRecieved(finalHeal);
    }
    protected void Shield(Character caster, Character target, float shieldPercent, float flatShield)
    {
        float finalShield = Formulas.i.Shield(caster, target, shieldPercent, flatShield, kitMultiplierType);
        ShieldEffect shieldEffect = new ShieldEffect(caster, finalShield);
        target.GetCharacterData().ShieldRecieved(shieldEffect);
    }

    protected void TriggerObservers(Character origin, ExtraActionManager extraActionManager)
    {
        if (origin is Playable playable)
        {
            playable.NotifyAbilityUsed(this, extraActionManager);
        }
        else if (origin is Enemy enemy)
        {
            enemy.NotifyAbilityUsed(this, extraActionManager);
        }
    }
}

[Serializable]
class SingleTargetDamageEffect : AbilityEffect
{
    public float damagePercent;

    public override void Execute(Character caster, Character[] targets, int principalTarget, ExtraActionManager extraActionManager)
    {
       Damage(caster, targets[principalTarget], damagePercent, extraActionManager);

        TriggerObservers(caster, extraActionManager);
    }
}

[Serializable]
class SingleTargetRepeatedDamageEffect : AbilityEffect
{
    public float damagePercent;
    public float amount;

    public override void Execute(Character caster, Character[] targets, int principalTarget, ExtraActionManager extraActionManager)
    {
        for (int i = 0; i < amount; i++)
        {
            Damage(caster, targets[principalTarget], damagePercent, extraActionManager);
        }
        TriggerObservers(caster, extraActionManager);
    }
}

[Serializable]
class AoEDamageEffect : AbilityEffect
{
    public float principalDamagePercent;
    public float adhancedDamagePercent;

    public override void Execute(Character caster, Character[] targets, int principalTarget, ExtraActionManager extraActionManager)
    {
        if (principalTarget != 0 && targets[principalTarget - 1] != null)
        {
            Damage(caster, targets[principalTarget - 1], adhancedDamagePercent, extraActionManager);
        }
        if (targets.Length > 1 && principalTarget + 1 != targets.Length && targets[principalTarget + 1] != null )
        {
            Damage(caster, targets[principalTarget + 1], adhancedDamagePercent, extraActionManager);
        }
        Damage(caster, targets[principalTarget], principalDamagePercent, extraActionManager);

        TriggerObservers(caster, extraActionManager);
    }

    
}

[Serializable]
class HealingEffect : AbilityEffect
{
    public float healingPercent;
    public float flatHealing;

    public override void Execute(Character caster, Character[] targets, int principalTarget, ExtraActionManager extraActionManager)
    {
        Heal(caster, targets[principalTarget], healingPercent, flatHealing);

        TriggerObservers(caster, extraActionManager);
    }
}