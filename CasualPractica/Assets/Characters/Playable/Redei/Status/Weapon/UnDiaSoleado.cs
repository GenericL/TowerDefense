using UnityEngine;

public class UnDiaSoleadoFactory : MultiTargetStatusFactory<ReySinPlebeyosData, ReySinPlebeyos> { }

public struct UnDiaSoleadoData
{

    public StatModifier<StatModifierData> teamAttackDamageBonus;
    public TurnTimer turnDurationTimer => new TurnTimer(true, 2);
    public UnDiaSoleadoData(Character source)
    {
        teamAttackDamageBonus = new StatModifier<StatModifierData>(0.5f, new StatModifierData(StatModifierType.MULT));
    }
}
public class UnDiaSoleado : MultiTargetStatus<UnDiaSoleadoData>
{
    public UnDiaSoleado()
    {
        data = new UnDiaSoleadoData(source);
    }
    public override void ApplyStatus()
    {
        PassiveManager.i.onCharacterBasicUsed.AddListener(OnCharacterBasicAttack);
    }

    public void OnCharacterBasicAttack(Character source)
    {
        if (this.source == source)
        {
            foreach (Character target in targets)
            {
                target.GetCharacterData().AddAttackModifier(data.teamAttackDamageBonus);
            }
            data.turnDurationTimer.ResetTimer();
        }
    }

    public override void RemoveStatus()
    {
        RemoveStatusesFromTargets();
        PassiveManager.i.onCharacterBasicUsed.RemoveListener(OnCharacterBasicAttack);
    }

    private void RemoveStatusesFromTargets()
    {
        foreach (Character target in targets)
        {
            target.GetCharacterData().RemoveAttackModifier(data.teamAttackDamageBonus);
        }
    }

    public override void UpdateStatus()
    {
        if (data.turnDurationTimer.Tick())
        {
            RemoveStatusesFromTargets();
        }
    }
}
