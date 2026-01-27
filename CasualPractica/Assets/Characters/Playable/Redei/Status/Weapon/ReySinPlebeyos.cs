using UnityEngine;
using static Unity.VisualScripting.Member;

public class ReySinPlebeyosFactory : MultiTargetStatusFactory<ReySinPlebeyosData, ReySinPlebeyos> { }

public struct ReySinPlebeyosData
{
    public bool enabled;
    public StatModifier<StatModifierData> selfAttackDamageBonus => new StatModifier<StatModifierData>("Rey sin plebeyos", 0.48f, new StatModifierData(StatModifierType.MULT, this));
    public UnDiaSoleado unDiaSoleado;
    public UnDiaNublado unDiaNublado;
}

public class ReySinPlebeyos : MultiTargetStatus<ReySinPlebeyosData>
{
    public override void ApplyStatus()
    {
        data.enabled = false;
        data.unDiaSoleado = (UnDiaSoleado)new UnDiaSoleadoFactory().GetStatus(targets, source);
        data.unDiaNublado = (UnDiaNublado)new UnDiaNubladoFactory().GetStatus(source, source);
        PassiveManager.i.OnCharacterDefinitiveUsed.AddListener(OnCharacterUltimate);
    }

    public void OnCharacterUltimate(Character source)
    {
        if (this.source.GetCharacterData().GetCharacterName() == source.GetCharacterData().GetCharacterName())
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
                bool success = source.GetCharacterData().RemoveAttackModifier(data.selfAttackDamageBonus);
                Debug.Log("SUCCESSFUL REMOVAL: " + success);
                source.RemoveStatus(data.unDiaSoleado);
                source.RemoveStatus(data.unDiaNublado);
            }
            Debug.Log("REY SIN PLEBEYOS APPLIED: " + data.enabled);
            Debug.Log("CHARACTER ATK: " + source.GetCharacterData().GetFinalAttack());
            Debug.Log("CHARACTER STATUS: " + source.GetStatuses());
            Debug.Log("CHARACTER ATK MODIFIER COUNT: " + source.GetCharacterData().GetAttackModifierCount());
        }
    }

    public override void RemoveStatus()
    {
        data.unDiaSoleado.RemoveStatus();
        data.unDiaNublado.RemoveStatus();
        source.RemoveStatus(data.unDiaSoleado);
        source.RemoveStatus(data.unDiaNublado);
        PassiveManager.i.OnCharacterDefinitiveUsed.RemoveListener(OnCharacterUltimate);
    }

    public override void UpdateStatus()
    {
    }
}
