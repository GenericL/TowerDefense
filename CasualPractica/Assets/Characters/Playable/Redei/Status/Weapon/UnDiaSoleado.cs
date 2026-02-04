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
    public override void ApplyStatus(ExtraActionManager extraActionManager)
    {
        data.appliedBuff = false;
        extraActionManager.PassiveManager.OnCharacterBasicUsed.AddListener(OnCharacterBasicAttack);
    }

    public void OnCharacterBasicAttack(Character source, ExtraActionManager extraActionManager)
    {
        if (this.source.GetCharacterData().GetCharacterName() == source.GetCharacterData().GetCharacterName())
        {
            if (!data.appliedBuff)
            {
                foreach (Character target in targets)
                {
                    if(target != null)
                        target.GetCharacterData().AddAttackModifier(data.teamAttackDamageBonus);
                }
                data.appliedBuff = true;
            }
            data.turnDurationTimer.ResetTimer();
        }
    }

    public override void RemoveStatus(ExtraActionManager extraActionManager)
    {
        RemoveStatusesFromTargets();
        extraActionManager.PassiveManager.OnCharacterBasicUsed.RemoveListener(OnCharacterBasicAttack);
    }

    private void RemoveStatusesFromTargets()
    {
        foreach (Character target in targets)
        {
            if (target != null)
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
