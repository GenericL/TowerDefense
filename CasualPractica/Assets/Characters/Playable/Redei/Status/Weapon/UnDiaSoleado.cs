using UnityEngine;

public class UnDiaSoleadoFactory : MultiTargetStatusFactory<UnDiaSoleadoData, UnDiaSoleado> { }

public struct UnDiaSoleadoData
{

    public StatModifier<StatModifierData> teamAttackDamageBonus => new StatModifier<StatModifierData>("Un día soleado", 0.5f, new StatModifierData(StatModifierType.MULT, this));
    public TurnTimer turnDurationTimer => new TurnTimer(true, 2);
    public bool appliedBuff;
}
public class UnDiaSoleado : MultiTargetStatus<UnDiaSoleadoData>
{
    public override void ApplyStatus()
    {
        data.appliedBuff = false;
        PassiveManager.i.OnCharacterBasicUsed.AddListener(OnCharacterBasicAttack);
    }

    public void OnCharacterBasicAttack(Character source)
    {
        if (this.source.GetCharacterData().GetCharacterName() == source.GetCharacterData().GetCharacterName())
        {
            if (!data.appliedBuff)
            {
                foreach (Character target in targets)
                {
                    target.GetCharacterData().AddAttackModifier(data.teamAttackDamageBonus);
                }
                data.appliedBuff = true;
            }
            data.turnDurationTimer.ResetTimer();
        }
    }

    public override void RemoveStatus()
    {
        RemoveStatusesFromTargets();
        PassiveManager.i.OnCharacterBasicUsed.RemoveListener(OnCharacterBasicAttack);
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
            data.appliedBuff = false;
        }
    }
}
