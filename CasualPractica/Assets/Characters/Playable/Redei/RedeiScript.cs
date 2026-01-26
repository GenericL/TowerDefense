using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class RedeiScript : Playable
{

    [SerializeField] private AbilityData basicAttack;
    [SerializeField] private AbilityData enhancedBasicAttack;

    [SerializeField] private AbilityData abilityAttack;
    [SerializeField] private AbilityData enhancedAbilityAttack;

    private int rey = 0;
    private int tirano = 0;
    private bool enhanced = false;

    private void Awake()
    {
        characterData = new CharacterData("Redei", ElementType.ILLUSION, CharacterType.PLAYABLE, 800, 12, 12, 102);
        energySystem = new EnergySystem(3);
    }
    public override void InitialSetup(Enemy[] enemies, Playable[] playables)
    {
        AddStatus((RedeiWeapon)new RedeiWeaponFactory().GetStatus(playables,this));
    }
    public override void Dies()
    {
        PassiveManager.i.onDamageOnEnemyTypeActivated.RemoveListener(SoberanoDeLosMonstruos);
        base.Dies();
    }
    public override void AddListenersToPassiveManager()
    {
        PassiveManager.i.onDamageOnEnemyTypeActivated.AddListener(SoberanoDeLosMonstruos);
    }
    public override bool Ability(Character[] targets, int principalTarget, AbilityPointSystem abilityPoints)
    {
        if (enhanced)
        {
            ExecuteAbility(enhancedAbilityAttack, targets, principalTarget);
            tirano++;
            if (rey < tirano)
            {
                EnergySystem.RestoreEnergy(1);
            }
            if (tirano > 3)
            {
                tirano = 3;
            }
            enhanced = false;
            return base.Ability(targets, principalTarget, abilityPoints);
        }
        else if (abilityPoints.DecreaseAbilityPoint())
        {
            ExecuteAbility(abilityAttack, targets, principalTarget);
            return base.Ability(targets, principalTarget, abilityPoints);
        }
        return false;
    }

    public override bool Basic(Character[] targets, int principalTarget, AbilityPointSystem abilityPoints)
    {
        if (enhanced)
        {
            ExecuteAbility(enhancedBasicAttack, targets.ToArray(), principalTarget);
            rey++;
            if(rey > tirano)
            {
                EnergySystem.RestoreEnergy(1);
            }
            if(rey > 3)
            {
                rey = 3;
            }
            enhanced = false;
        }
        else
        {
            ExecuteAbility(basicAttack, targets.ToArray(), principalTarget);
            abilityPoints.IncrementAbilityPoint();
        }
        return base.Basic(targets, principalTarget, abilityPoints);
    }

    public override void Definitive(Character[] targets, int principalTarget, AbilityPointSystem abilityPoints)
    {
        if (rey == 3 || tirano == 3)
        {
            rey = 0;
            tirano = 0;
            EnergySystem.ConsumeAllEnergy();
            base.Definitive(targets, principalTarget, abilityPoints);
        } else
            Debug.Log("Can't use ultimate: rey = " + rey + ", tirano = " + tirano);
    }

    public override void InitialPasive(Enemy[] enemies, Playable[] playables)
    {
        PassiveManager.i.onDamageOnEnemyTypeActivated.AddListener(SoberanoDeLosMonstruos);
    }

    private bool activatedSoberanoDeLosMonstruos = false;
    public void SoberanoDeLosMonstruos(DamageType dmgType)
    {
        if (dmgType.Equals(DamageType.COORDINATED_ATTACK_DMG) && !activatedSoberanoDeLosMonstruos)
        {
            enhanced = true;
        }
    }
    public override bool DoTurn(Character[] targets, int principalTarget, AbilityPointSystem abilityPoints)
    {
        // AI logic if wanted
        return true;
    }
}
