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
    public List<DamageType> typeDMG;

    public abstract void Execute(Character caster, Character[] targets, int principalTarget);
    protected void Damage(Character caster, Character target, float damagePercent)
    {
        TriggerObservers(caster);
        float finalDamage = Formulas.i.Damage(caster, target, damagePercent);
        bool defeated = target.GetCharacterData().DamageRecieved(finalDamage);
        if (defeated)
        {
            target.Dies();
        }
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
    public float healing;

    public override void Execute(Character caster, Character[] targets, int principalTarget)
    {
        // TODO: Actualizarlo con formulas luego
        targets[principalTarget].GetCharacterData().HealingRecieved(healing);
        Debug.Log($"{caster.name} healt {healing} to {targets[principalTarget].name}");
    }
}