using System.Collections.Generic;
using UnityEngine;

public class DragonAcidico : Enemy
{

    [SerializeField] private AbilityData basicAttack;
    [SerializeField] private AbilityData enhancedBasicAttack;
    [SerializeField] private AbilityData abilityAttack;

    private void Awake()
    {
        characterData = new CharacterData("Dragon Acidico", ElementType.NATURA, CharacterType.ENEMY, 4000, 3, 10, 105);
        healthBar.SetMaxHealth(325);
    }
    public override bool Ability(Character[] targets, int principalTarget, AbilityPointSystem abilityPoints, ExtraActionManager extraActionManager)
    {
        ExecuteAbility(abilityAttack, targets, principalTarget, extraActionManager);
        return base.Ability(targets,principalTarget,abilityPoints,extraActionManager);
    }

    public override bool Basic(Character[] targets, int principalTarget, AbilityPointSystem abilityPoints, ExtraActionManager extraActionManager)
    {
        if (Random.Range(0, 2) == 0) {
            ExecuteAbility(basicAttack, targets, principalTarget, extraActionManager);
        } else
        {
            ExecuteAbility(enhancedBasicAttack, targets, principalTarget, extraActionManager);
        }
        base.Basic(targets,principalTarget,abilityPoints,extraActionManager);
        return true;
    }

    public override void Definitive(Character[] targets, int principalTarget, AbilityPointSystem abilityPoints, ExtraActionManager extraActionManager)
    {
    }

    public override bool DoTurn(Character[] targets, int principalTarget, AbilityPointSystem abilityPoints, ExtraActionManager extraActionManager)
    {
        int attack = Random.Range(0, 100);
        if (attack < 75)
        {
            return Basic(targets, principalTarget, abilityPoints, extraActionManager);
        }
        else
        {
            return Ability(targets, principalTarget, abilityPoints, extraActionManager);
        }
    }
}
