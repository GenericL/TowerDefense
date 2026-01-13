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

    public override bool Basic(Character[] targets, int principalTarget)
    {
        foreach (var effect in basicAttack.effects)
        {
            effect.Execute(this, targets, principalTarget);
        }
        return true;
    }

    public override bool DoTurn(Character[] targets, int principalTarget)
    {
        return Basic(targets, principalTarget);
    }

    public override bool Ability(Character[] targets, int principalTarget)
    {
        return false;
    }
    public override bool Definitive(Character[] targets, int principalTarget)
    {
        return false;
    }
}
