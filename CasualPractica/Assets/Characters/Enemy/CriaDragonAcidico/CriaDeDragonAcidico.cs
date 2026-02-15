using System.Collections.Generic;
using UnityEngine;

public class CriaDeDragonAcidico : Enemy
{
    [SerializeField] private AbilityData basicAttack;
    private void Awake()
    {
        characterData = new CharacterData("Cría de Dragon Acidico", ElementType.NATURA, CharacterType.ENEMY, 500, 16, 6, 120);
        healthBar.SetMaxHealth(125);
    }

    public override bool Basic(Character[] targets, int principalTarget, AbilityPointSystem abilityPoints, ExtraActionManager extraActionManager)
    {
        foreach (var effect in basicAttack.effects)
        {
            effect.Execute(this, targets, principalTarget, extraActionManager);
        }
        base.Basic(targets, principalTarget, abilityPoints, extraActionManager);
        return true;
    }

    public override bool DoTurn(Character[] targets, int principalTarget, AbilityPointSystem abilityPoints, ExtraActionManager extraActionManager)
    {
        return Basic(targets, principalTarget, abilityPoints, extraActionManager);
    }

    public override bool Ability(Character[] targets, int principalTarget, AbilityPointSystem abilityPoints, ExtraActionManager extraActionManager)
    {
        return false;
    }
    public override void Definitive(Character[] targets, int principalTarget, AbilityPointSystem abilityPoints, ExtraActionManager extraActionManager)
    {
        
    }
}
