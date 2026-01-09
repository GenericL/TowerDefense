using System.Collections.Generic;
using UnityEngine;

public class DragonAcidico : Enemy
{

    [SerializeField] private AbilityData basicAttack;
    [SerializeField] private AbilityData enhancedBasicAttack;
    [SerializeField] private AbilityData abilityAttack;

    private void Awake()
    {
        characterData = new CharacterData("Dragon Acidico", ElementType.NATURA, CharacterType.ENEMY, 325, 26, 10, 105);
        healthBar.SetMaxHealth(325);
    }
    public override bool Ability(List<Playable> targets, int principalTarget)
    {
        ExecuteAbility(abilityAttack, targets, principalTarget);
        return true;
    }

    public override bool Basic(List<Playable> targets, int principalTarget)
    {
        if (Random.Range(0, 2) == 0) {
            ExecuteAbility(basicAttack, targets, principalTarget);
        } else
        {
            ExecuteAbility(enhancedBasicAttack, targets, principalTarget);
        }
        return true;
    }

    public override bool Definitive(List<Playable> targets, int principalTarget)
    {
        return false;
    }

    public override bool DoTurn(List<Playable> targets, int principalTarget)
    {
        int attack = Random.Range(0, 100);
        if (attack < 75)
        {
            return Basic(targets, principalTarget);
        }
        else
        {
            return Ability(targets, principalTarget);
        }
    }

    private void ExecuteAbility(AbilityData abilityData, List<Playable> targets, int principalTarget)
    {
        foreach (var effect in abilityData.effects)
        {
            effect.Execute(this, targets.ToArray(), principalTarget);
        }
    }
}
