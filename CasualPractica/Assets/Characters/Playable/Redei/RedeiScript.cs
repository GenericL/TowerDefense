using System.Linq;
using UnityEngine;

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
        characterData = new CharacterData(
            "Redei", 
            ElementType.ILLUSION, 
            CharacterType.PLAYABLE,
            CharacterStartingStats.PLAYABLE_HP_MEDIUM, 
            CharacterStartingStats.PLAYABLE_ATK_HIGH, 
            CharacterStartingStats.PLAYABLE_DEF_LOW, 
            CharacterStartingStats.ENEMY_SPD_MEDIUM);
        energySystem = new EnergySystem(3);
    }
    public override void InitialSetup(Enemy[] enemies, Playable[] playables, ExtraActionManager extraActionManager)
    {
        AddStatus((RedeiWeapon)new RedeiWeaponFactory().GetStatus(playables,this), extraActionManager);
    }
    public override void Dies(ExtraActionManager extraActionManager)
    {
        extraActionManager.PassiveManager.OnDamageOnEnemyTypeActivated.RemoveListener(SoberanoDeLosMonstruos);
        base.Dies(extraActionManager);
    }
    public override bool Ability(Character[] targets, int principalTarget, AbilityPointSystem abilityPoints, ExtraActionManager extraActionManager)
    {
        if (enhanced)
        {
            ExecuteAbility(enhancedAbilityAttack, targets, principalTarget, extraActionManager);
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
            return base.Ability(targets, principalTarget, abilityPoints, extraActionManager);
        }
        else if (abilityPoints.DecreaseAbilityPoint())
        {
            ExecuteAbility(abilityAttack, targets, principalTarget, extraActionManager);
            return base.Ability(targets, principalTarget, abilityPoints, extraActionManager);
        }
        return false;
    }

    public override bool Basic(Character[] targets, int principalTarget, AbilityPointSystem abilityPoints, ExtraActionManager extraActionManager)
    {
        if (enhanced)
        {
            ExecuteAbility(enhancedBasicAttack, targets.ToArray(), principalTarget, extraActionManager);
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
            ExecuteAbility(basicAttack, targets.ToArray(), principalTarget, extraActionManager);
            abilityPoints.IncrementAbilityPoint();
        }
        return base.Basic(targets, principalTarget, abilityPoints, extraActionManager);
    }

    public override void Definitive(Character[] targets, int principalTarget, AbilityPointSystem abilityPoints, ExtraActionManager extraActionManager)
    {
        if (rey == 3 || tirano == 3)
        {
            rey = 0;
            tirano = 0;
            EnergySystem.ConsumeAllEnergy();
            base.Definitive(targets, principalTarget, abilityPoints, extraActionManager);
        } else
            Debug.Log("Can't use ultimate: rey = " + rey + ", tirano = " + tirano);
    }

    public override void InitialPasive(Enemy[] enemies, Playable[] playables, AbilityPointSystem abilityPointSystem, ExtraActionManager extraActionManager)
    {
        extraActionManager.PassiveManager.OnDamageOnEnemyTypeActivated.AddListener(SoberanoDeLosMonstruos);
    }

    private bool activatedSoberanoDeLosMonstruos = false;
    public void SoberanoDeLosMonstruos(DamageType dmgType, ExtraActionManager extraActionManager)
    {
        if (dmgType.Equals(DamageType.COORDINATED_ATTACK_DMG) && !activatedSoberanoDeLosMonstruos)
        {
            enhanced = true;
        }
    }
    public override bool DoTurn(Character[] targets, int principalTarget, AbilityPointSystem abilityPoints, ExtraActionManager extraActionManager)
    {
        // AI logic if wanted
        return true;
    }
}
