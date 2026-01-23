using UnityEngine;

public class UnDiaNubladoFactory : SingleTargetStatusFactory<UnDiaNubladoData, UnDiaNublado> {}

public struct UnDiaNubladoData
{
    public StatModifier<StatModifierData> selfCritDamageBonusUnDiaNublado => new StatModifier<StatModifierData>(0.07f, new StatModifierData(StatModifierType.MULT));
    public int maxUnDiaNubladoStacks => 8;
    public int currentUnDiaNubladoStacks;
}

public class UnDiaNublado : SingleTargetStatus<UnDiaNubladoData>
{
    public override void ApplyStatus()
    {
        data.currentUnDiaNubladoStacks = 0;
        PassiveManager.i.onCharacterAbilityUsed.AddListener(OnSelfCharacterAbility);
        PassiveManager.i.onCharacterEndTurn.AddListener(OnEndTurn);
    }

    public void OnSelfCharacterAbility(Character source)
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
    public void OnEndTurn(Character source)
    {
        if (this.source.GetCharacterData().GetCharacterName() != source.GetCharacterData().GetCharacterName())
        {
            return;
        }
        RemoveStatus();
    }
    public override void RemoveStatus()
    {
        source.GetCharacterData().RemoveCritDamagerModifier(data.selfCritDamageBonusUnDiaNublado);
        PassiveManager.i.onCharacterAbilityUsed.RemoveListener(OnSelfCharacterAbility);
        PassiveManager.i.onCharacterEndTurn.RemoveListener(OnEndTurn);
    }
    public override void UpdateStatus()
    {
        
    }
}