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

    public abstract void Execute(Character caster, Character[] targets, int principalTarget);
    protected void Damage(Character caster, Character target, float damagePercent)
    {
        float finalDamage = Formulas.i.Damage(caster, target, damagePercent, elementDMG, kitMultiplierType);
        bool defeated = target.GetCharacterData().DamageRecieved(finalDamage);
        if (defeated)
        {
            target.Dies();
        }
        TriggerObservers(caster);
    }
    protected void Heal(Character caster, Character target, float healPercent, float flatHeal)
    {
        float finalHeal = Formulas.i.Heal(caster, target, healPercent, flatHeal, kitMultiplierType);
        target.GetCharacterData().HealingRecieved(finalHeal);
        TriggerObservers(caster);
    }
    protected void Shield(Character caster, Character target, float shieldPercent, float flatShield)
    {
        float finalShield = Formulas.i.Shield(caster, target, shieldPercent, flatShield, kitMultiplierType);
        ShieldEffect shieldEffect = new ShieldEffect(caster, finalShield);
        target.GetCharacterData().ShieldRecieved(shieldEffect);
        TriggerObservers(caster);
    }

    private void TriggerObservers(Character origin)
    {
        if (origin is Playable playable)
        {
            playable.NotifyAbilityUsed(this);
        }
        else if (origin is Enemy enemy)
        {
            enemy.NotifyAbilityUsed(this);
        }
    }
}

[Serializable]
class SingleTargetDamageEffect : AbilityEffect
{
    public float damagePercent;

    public override void Execute(Character caster, Character[] targets, int principalTarget)
    {
       Damage(caster, targets[principalTarget], damagePercent);
    }
}

[Serializable]
class SingleTargetRepeatedDamageEffect : AbilityEffect
{
    public float damagePercent;
    public float amount;

    public override void Execute(Character caster, Character[] targets, int principalTarget)
    {
        for (int i = 0; i < amount; i++)
        {
            Damage(caster, targets[principalTarget], damagePercent);
        }
    }
}

[Serializable]
class AoEDamageEffect : AbilityEffect
{
    public float principalDamagePercent;
    public float adhancedDamagePercent;

    public override void Execute(Character caster, Character[] targets, int principalTarget)
    {
        if (principalTarget != 0 && targets[principalTarget - 1] != null)
        {
            Damage(caster, targets[principalTarget - 1], adhancedDamagePercent);
        }
        if (targets.Length > 1 && principalTarget + 1 != targets.Length && targets[principalTarget + 1] != null )
        {
            Damage(caster, targets[principalTarget + 1], adhancedDamagePercent);
        }
        Damage(caster, targets[principalTarget], principalDamagePercent);
    }

    
}

[Serializable]
class HealingEffect : AbilityEffect
{
    public float healingPercent;
    public float flatHealing;

    public override void Execute(Character caster, Character[] targets, int principalTarget)
    {
        Heal(caster, targets[principalTarget], healingPercent, flatHealing);
    }
}