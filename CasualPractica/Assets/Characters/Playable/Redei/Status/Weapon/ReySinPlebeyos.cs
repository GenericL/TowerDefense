using UnityEngine;

public class ReySinPlebeyosFactory : MultiTargetStatusFactory<ReySinPlebeyosData, ReySinPlebeyos> { }

public struct ReySinPlebeyosData
{
    public bool enabled;
    public StatModifier<StatModifierData> selfAttackDamageBonus;
    public UnDiaSoleado unDiaSoleado;
    public UnDiaNublado unDiaNublado;

    public ReySinPlebeyosData(Character source, Character[] targets)
    {
        enabled = false;
        selfAttackDamageBonus = new StatModifier<StatModifierData>(0.48f, new StatModifierData(StatModifierType.MULT, source));
        unDiaSoleado = (UnDiaSoleado)new UnDiaSoleadoFactory().GetStatus(targets, source);
        unDiaNublado = (UnDiaNublado)new UnDiaNubladoFactory().GetStatus(source, source);
    }
}

public class ReySinPlebeyos : MultiTargetStatus<ReySinPlebeyosData>
{
    public ReySinPlebeyos()
    {
        data = new ReySinPlebeyosData(source, targets);
    }
    public override void ApplyStatus()
    {
        data.enabled = true;
        PassiveManager.i.onCharacterDefinitiveUsed.AddListener(OnCharacterUltimate);
    }

    public void OnCharacterUltimate(Character source)
    {
        if (this.source == source)
        {
            data.enabled = !data.enabled;
            if (data.enabled)
            {
                source.GetCharacterData().AddAttackModifier(data.selfAttackDamageBonus);
                source.AddStatus(data.unDiaSoleado);
                source.AddStatus(data.unDiaNublado);
            }
            else
            {
                source.GetCharacterData().RemoveAttackModifier(data.selfAttackDamageBonus);
                source.RemoveStatus(data.unDiaSoleado);
                source.RemoveStatus(data.unDiaNublado);
            }
        }
    }

    public override void RemoveStatus()
    {
        data.unDiaSoleado.RemoveStatus();
        data.unDiaNublado.RemoveStatus();
        source.RemoveStatus(data.unDiaSoleado);
        source.RemoveStatus(data.unDiaNublado);
        PassiveManager.i.onCharacterDefinitiveUsed.RemoveListener(OnCharacterUltimate);
    }

    public override void UpdateStatus()
    {
        data.unDiaSoleado.UpdateStatus();
    }
}
