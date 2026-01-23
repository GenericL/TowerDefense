using UnityEngine;
using static Unity.VisualScripting.Member;

public class RedeiWeaponFactory: MultiTargetStatusFactory<RedeiWeaponData, RedeiWeapon> {}

public struct RedeiWeaponData
{
    public StatModifier<StatModifierData> selfCritDamageBonus => new StatModifier<StatModifierData>(0.32f, new StatModifierData(StatModifierType.MULT_TOTAL, this));
    public ReySinPlebeyos reySinPlebeyos;
}

public class RedeiWeapon : MultiTargetStatus<RedeiWeaponData>
{
    public override void ApplyStatus()
    {
        
        source.GetCharacterData().AddCritDamagerModifier(data.selfCritDamageBonus);
        data.reySinPlebeyos = (ReySinPlebeyos)new ReySinPlebeyosFactory().GetStatus(targets, source);
        source.AddStatus(data.reySinPlebeyos);
    }

    public override void RemoveStatus()
    {
        source.GetCharacterData().RemoveCritDamagerModifier(data.selfCritDamageBonus);
        data.reySinPlebeyos.RemoveStatus();
        source.RemoveStatus(data.reySinPlebeyos);
    }

    public override void UpdateStatus()
    {
        data.reySinPlebeyos.UpdateStatus();
    }
}
