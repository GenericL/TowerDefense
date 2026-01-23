using UnityEngine;

public class UnDiaSoleadoFactory : MultiTargetStatusFactory<UnDiaSoleadoData, UnDiaSoleado> { }

public struct UnDiaSoleadoData
{

    public StatModifier<StatModifierData> teamAttackDamageBonus => new StatModifier<StatModifierData>(0.5f, new StatModifierData(StatModifierType.MULT));
    public TurnTimer turnDurationTimer => new TurnTimer(true, 2);
}
public class UnDiaSoleado : MultiTargetStatus<UnDiaSoleadoData>
{
    public override void ApplyStatus()
    {
        PassiveManager.i.onCharacterBasicUsed.AddListener(OnCharacterBasicAttack);
    }

    public void OnCharacterBasicAttack(Character source)
    {
        if (this.source.GetCharacterData().GetCharacterName() == source.GetCharacterData().GetCharacterName())
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
