using UnityEngine;

public class UnDiaNubladoFactory : SingleTargetStatusFactory<UnDiaNubladoData, UnDiaNublado> {}

public struct UnDiaNubladoData
{
    public StatModifier<StatModifierData> selfCritDamageBonusUnDiaNublado => new StatModifier<StatModifierData>("Un dia nublado", 0.07f, new StatModifierData(StatModifierType.MULT,this));
    public int maxUnDiaNubladoStacks => 8;
    public int currentUnDiaNubladoStacks;
}

public class UnDiaNublado : SingleTargetStatus<UnDiaNubladoData>
{
    public override void ApplyStatus(ExtraActionManager extraActionManager)
    {
        data.currentUnDiaNubladoStacks = 0;
        extraActionManager.PassiveManager.OnCharacterAbilityUsed.AddListener(OnSelfCharacterAbility);
        extraActionManager.PassiveManager.OnCharacterEndTurn.AddListener(OnEndTurn);
    }

    public void OnSelfCharacterAbility(Character source, ExtraActionManager extraActionManager)
    {
        if (this.source.GetCharacterData().GetCharacterName() != source.GetCharacterData().GetCharacterName())
        {
            return;
        }
        if (data.currentUnDiaNubladoStacks >= data.maxUnDiaNubladoStacks)
        {
            return;
        }
        data.currentUnDiaNubladoStacks++;
        source.GetCharacterData().AddCritDamagerModifier(data.selfCritDamageBonusUnDiaNublado);
    }
    public void OnEndTurn(Character source, ExtraActionManager extraActionManager)
    {
        if (this.source.GetCharacterData().GetCharacterName() != source.GetCharacterData().GetCharacterName())
        {
            return;
        }
        RemoveStatus(extraActionManager);
    }
    public override void RemoveStatus(ExtraActionManager extraActionManager)
    {
        source.GetCharacterData().RemoveCritDamagerModifier(data.selfCritDamageBonusUnDiaNublado);
        extraActionManager.PassiveManager.OnCharacterAbilityUsed.RemoveListener(OnSelfCharacterAbility);
        extraActionManager.PassiveManager.OnCharacterEndTurn.RemoveListener(OnEndTurn);
    }
    public override void UpdateStatus()
    {
        
    }
}