using UnityEngine;
using static Unity.VisualScripting.Member;

public class RedeiWeaponFactory: MultiTargetStatusFactory<RedeiWeaponData, RedeiWeapon> {}

public struct RedeiWeaponData
{
    public StatModifier<StatModifierData> selfCritDamageBonus => new StatModifier<StatModifierData>("Redei Weapon" ,0.32f, new StatModifierData(StatModifierType.MULT_TOTAL, this));
    public ReySinPlebeyos reySinPlebeyos;
}

public class RedeiWeapon : MultiTargetStatus<RedeiWeaponData>
{
    public override void ApplyStatus(ExtraActionManager extraActionManager)
    {
        
        source.GetCharacterData().AddCritDamagerModifier(data.selfCritDamageBonus);
        data.reySinPlebeyos = (ReySinPlebeyos)new ReySinPlebeyosFactory().GetStatus(targets, source);
        source.AddStatus(data.reySinPlebeyos, extraActionManager);
    }

    public override void RemoveStatus(ExtraActionManager extraActionManager)
    {
        source.GetCharacterData().RemoveCritDamagerModifier(data.selfCritDamageBonus);
        data.reySinPlebeyos.RemoveStatus(extraActionManager);
        source.RemoveStatus(data.reySinPlebeyos, extraActionManager);
    }

    public override void UpdateStatus()
    {
    }
}
