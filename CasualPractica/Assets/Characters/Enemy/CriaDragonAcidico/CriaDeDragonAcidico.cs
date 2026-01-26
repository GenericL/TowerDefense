using System.Collections.Generic;
using UnityEngine;

public class CriaDeDragonAcidico : Enemy
{
    [SerializeField] private AbilityData basicAttack;
    private void Awake()
    {
        characterData = new CharacterData("Cría de Dragon Acidico", ElementType.NATURA, CharacterType.ENEMY, 125, 16, 6, 120);
        healthBar.SetMaxHealth(125);
    }

    public override bool Basic(Character[] targets, int principalTarget, AbilityPointSystem abilityPoints)
    {
        foreach (var effect in basicAttack.effects)
        {
            effect.Execute(this, targets, principalTarget);
        }
        return true;
    }

    public override bool DoTurn(Character[] targets, int principalTarget, AbilityPointSystem abilityPoints)
    {
        return Basic(targets, principalTarget, abilityPoints);
    }

    public override bool Ability(Character[] targets, int principalTarget, AbilityPointSystem abilityPoints)
    {
        return false;
    }
    public override void Definitive(Character[] targets, int principalTarget, AbilityPointSystem abilityPoints)
    {
        
    }

    public override void AddListenersToPassiveManager()
    {
        
    }
}
