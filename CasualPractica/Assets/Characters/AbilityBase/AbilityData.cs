using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Characters/AbilityBase/AbilityData", menuName = "Characters/AbilityData")]
public class AbilityData : ScriptableObject
{
    public string label;
    [SerializeField] public List<DamageType> damageType;
    [SerializeReference] public List<AbilityEffect> effects;

    void OnEnable()
    {
        if (string.IsNullOrEmpty(label)) label = name;
        if (damageType == null) damageType = new List<DamageType>();
        if (effects == null) effects = new List<AbilityEffect>();
    }
}

[Serializable]
public abstract class AbilityEffect
{
    public abstract void Execute(Character caster, Character[] targets, int principalTarget);
    protected void Damage(Character caster, Character target, float damagePercent)
    {
        float finalDamage = Formulas.i.Damage(caster.GetCharacterData(), target.GetCharacterData(), damagePercent);
        bool defeated = target.GetCharacterData().DamageRecieved(finalDamage);
        if (defeated)
        {
            target.Dies();
        }
        Debug.Log($"{caster.name} dealt {finalDamage} damage to {target.name}");
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
        if (principalTarget != 0)
        {
            Damage(caster, targets[principalTarget - 1], adhancedDamagePercent);
        }
        if (targets.Length > 1 && principalTarget+1 != targets.Length)
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