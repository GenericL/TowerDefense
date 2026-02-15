using UnityEngine;


public class HanorisWeaponFactory: SingleTargetStatusFactory<HanorisWeaponData, HanorisWeapon> { }
public struct HanorisWeaponData
{
    public StatModifier<StatModifierData> selfElementalDmg => new StatModifier<StatModifierData>("Hanoris Weapon", 0.12f, new StatModifierData(StatModifierType.MULT));
    public StatModifier<StatModifierData> selfBonusDmg => new StatModifier<StatModifierData>("Bonus dmg", 0.6f, new StatModifierData(StatModifierType.MULT));
    public bool twoTimesTurn;
    public bool applied;
}
public class HanorisWeapon : SingleTargetStatus<HanorisWeaponData>
{
    public override void ApplyStatus(ExtraActionManager extraActionManager)
    {
        source.GetCharacterData().AddVesaniaBonusModifier(data.selfElementalDmg);
        data.twoTimesTurn = false;
        data.applied = false;
    }

    public void OnStartingTurn(Character source, ExtraActionManager extraActionManager)
    {
        if (data.twoTimesTurn && !data.applied)
        {
            source.GetCharacterData().AddTotalDamageBonusModifier(data.selfBonusDmg);
            data.applied = true;
        }
        else if (!data.twoTimesTurn && data.applied) { 
            source.GetCharacterData().RemoveTotalDamageBonusModifier(data.selfBonusDmg); data.applied = false; 
        }

        if (this.source.GetCharacterData().GetCharacterName() == source.GetCharacterData().GetCharacterName()) data.twoTimesTurn = true;
        else data.twoTimesTurn = false;
    }

    public override void RemoveStatus(ExtraActionManager extraActionManager)
    {
        source.GetCharacterData().RemoveVesaniaBonusModifier(data.selfElementalDmg);
        source.GetCharacterData().RemoveTotalDamageBonusModifier(data.selfBonusDmg);
    }

    public override void UpdateStatus()
    {
    }
}