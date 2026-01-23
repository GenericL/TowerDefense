using UnityEngine;

public class UnDiaNubladoFactory : SingleTargetStatusFactory<UnDiaNubladoData, UnDiaNublado> {}

public struct UnDiaNubladoData
{
    public StatModifier<StatModifierData> selfCritDamageBonusUnDiaNublado;
    public int maxUnDiaNubladoStacks;
    public int currentUnDiaNubladoStacks;
    public UnDiaNubladoData(Character source)
    {
        this.selfCritDamageBonusUnDiaNublado = new StatModifier<StatModifierData>(0.07f, new StatModifierData(StatModifierType.MULT));
        this.maxUnDiaNubladoStacks = 8;
        this.currentUnDiaNubladoStacks = 0;
    }
}

public class UnDiaNublado : SingleTargetStatus<UnDiaNubladoData>
{
    public UnDiaNublado()
    {
        data = new UnDiaNubladoData(source);
    }
    public override void ApplyStatus()
    {
        PassiveManager.i.onCharacterAbilityUsed.AddListener(OnSelfCharacterAbility);
        PassiveManager.i.onCharacterEndTurn.AddListener(OnEndTurn);
    }

    public void OnSelfCharacterAbility(Character source)
    {
        if (source != this.source)
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
        if (source != this.source)
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