using UnityEngine;

public class RedeiWeaponFactory: MultiTargetStatusFactory<RedeiWeaponData, RedeiWeapon> {}

public struct RedeiWeaponData
{
    public StatModifier<StatModifierData> selfCritDamageBonus;
    public ReySinPlebeyos reySinPlebeyos;

    public RedeiWeaponData(Character source, Character[] targets)
    {
        this.selfCritDamageBonus = new StatModifier<StatModifierData>( 0.32f,new StatModifierData(StatModifierType.MULT_TOTAL, source));
        this.reySinPlebeyos = (ReySinPlebeyos)new ReySinPlebeyosFactory().GetStatus(targets, source);
    }
}

public class RedeiWeapon : MultiTargetStatus<RedeiWeaponData>
{
    public RedeiWeapon() { 
        data = new RedeiWeaponData(source, targets);
    }
    public override void ApplyStatus()
    {
        source.GetCharacterData().AddCritDamagerModifier(data.selfCritDamageBonus);
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
