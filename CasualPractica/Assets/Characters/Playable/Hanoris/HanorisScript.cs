using UnityEngine;

public class HanorisScript : Playable
{
    [SerializeField] private AbilityData basicAttack;
    [SerializeField] private AbilityData enhancedBasicAttack;

    [SerializeField] private AbilityData enhancedAbilityAttack;

    [SerializeField] private AbilityData startUltimateAttack;
    [SerializeField] private AbilityData endUltimateAttack;
    private void Awake() { characterData = new CharacterData(
        "Hanoris", 
        ElementType.VESANIA, 
        CharacterType.PLAYABLE, 
        CharacterStartingStats.PLAYABLE_HP_LOW, 
        CharacterStartingStats.ENEMY_ATK_HIGH, 
        CharacterStartingStats.ENEMY_DEF_LOW,
        CharacterStartingStats.PLAYABLE_SPD_HIGH); 
        energySystem = new EnergySystem(CharacterStartingStats.PLAYABLE_ENERGY_REQUIREMENT_HIGH); 
    }
    private bool overdrive = false;
    private StatModifier<StatModifierData> overdriveBuff = new StatModifier<StatModifierData>("Overdrive", 1.5f, new StatModifierData(StatModifierType.MULT));

    public override void InitialSetup(Enemy[] enemies, Playable[] playables, ExtraActionManager extraActionManager)
    {
        AddStatus((HanorisWeapon) new HanorisWeaponFactory().GetStatus(this,this), extraActionManager);
    }
    public override void InitialPasive(Enemy[] enemies, Playable[] playables, AbilityPointSystem abilityPointSystem, ExtraActionManager extraActionManager)
    {
        StartingDefinitive(enemies,0,abilityPointSystem,extraActionManager);
    }
    public override bool Basic(Character[] targets, int principalTarget, AbilityPointSystem abilityPoints, ExtraActionManager extraActionManager)
    {
        if (!overdrive)
        {
            ExecuteAbility(basicAttack, targets, principalTarget, extraActionManager);
            energySystem.RestoreEnergy(10);
        }
        else ExecuteAbility(enhancedBasicAttack, targets, principalTarget, extraActionManager);
        abilityPoints.IncrementAbilityPoint();
        return base.Basic(targets, principalTarget, abilityPoints, extraActionManager);
    }
    public override bool Ability(Character[] targets, int principalTarget, AbilityPointSystem abilityPoints, ExtraActionManager extraActionManager)
    {
        abilityPoints.DecreaseAbilityPoint();
        if (!overdrive) energySystem.RestoreEnergy(60);
        else
        {
            ExecuteAbility(enhancedAbilityAttack, targets, principalTarget, extraActionManager);
        }
        return base.Ability(targets, principalTarget, abilityPoints, extraActionManager);
    }
    public override void Definitive(Character[] targets, int principalTarget, AbilityPointSystem abilityPoints, ExtraActionManager extraActionManager)
    {
        if (energySystem.CanConsumeEnergy())
        {
            if (!overdrive)
            {
                StartingDefinitive(targets,principalTarget,abilityPoints,extraActionManager); 
            }
            else
            {
                ExecuteAbility(endUltimateAttack, targets, principalTarget, extraActionManager);
                overdrive = false;
                characterData.RemoveAttackModifier(overdriveBuff);
            }
            base.Definitive(targets, principalTarget, abilityPoints, extraActionManager);
        }
    }
    private void StartingDefinitive(Character[] targets, int principalTarget, AbilityPointSystem abilityPoints, ExtraActionManager extraActionManager)
    {
        overdrive = true;
        characterData.AddAttackModifier(overdriveBuff);
        ExecuteAbility(startUltimateAttack, targets, principalTarget, extraActionManager);
        AvanceTurn(100);
    }

    public override bool DoTurn(Character[] targets, int principalTarget, AbilityPointSystem abilityPoints, ExtraActionManager extraActionManager)
    {
        return false;
    }

    
}
