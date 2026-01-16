
using UnityEngine;
using UnityEngine.Events;

public struct CharacterData
{
    [SerializeField] private readonly string characterName;
    [SerializeField] private ElementType elementType;
    private Color elementColor;
    [SerializeField] private CharacterType characterType;
    private int level;

    private HealthSystem healthSystem;
    private BasicStatType attack;
    private BasicStatType defense;
    private BasicStatType speed;

    private PercentageStatType generalRes;

    private PercentageStatType critChance;
    private PercentageStatType critDamage;

    public CharacterData(string name, ElementType elementType, CharacterType characterType, float maxHp, float atk, float def, float speed, int level = 1)
    {
        characterName = name;
        this.elementType = elementType;
        this.characterType = characterType;
        this.level = level;

        healthSystem = new HealthSystem(maxHp);
        attack = new BasicStatType(atk);
        defense = new BasicStatType(def);
        this.speed = new BasicStatType(speed);

        generalRes = new PercentageStatType();
        critChance = new PercentageStatType(0.05f);
        critDamage = new PercentageStatType(0.5f);

        elementColor = ElementTypeExtensions.GetElementColor(elementType);
    }

    public string GetCharacterName() { return characterName; }
    public CharacterType GetCharacterType() { return characterType; }
    public ElementType GetElementType() { return elementType; }

    public int GetLevel()
    {
        return level;
    }
    public bool LevelUp(int level = 0)
    {
        if ((level > 0 && this.level + level < 40))
        {
            this.level += level;
            return true;
        }
        return false;
    }

    public bool DamageRecieved(float damage) { return healthSystem.Damage(damage); }
    public void HealingRecieved(float heal) { healthSystem.Heal(heal); }

    public float GetFinalAttack()
    {
        return attack.FinalValue;
    }
    public float GetFinalDefense()
    {
        return defense.FinalValue;
    }
    public float GetFinalSpeed()
    {
        return speed.FinalValue;
    }
    public float GetGeneralRes()
    {
        return generalRes.FinalValue;
    }
    public float GetCritChance() { return critChance.FinalValue; }
    public float GetCritDamage() { return critDamage.FinalValue; }
    public float GetMaxHealth() { return healthSystem.GetMaxHealth(); }
    public float GetCurrentHealth() { return healthSystem.GetCurrentHealth(); }
    public float GetPercentHealth() { return healthSystem.GetHealthPercent(); }
    public Color GetElementColor() { return elementColor; }

    public void AddAttackModifier(StatModifier<StatModifierData> modifier) { attack.AddModifier(modifier); }
    public void AddDefenseModifier(StatModifier<StatModifierData> modifier) { defense.AddModifier(modifier); }
    public void AddSpeedModifier(StatModifier<StatModifierData> modifier) { speed.AddModifier(modifier); }
    public void AddGeneralResModifier(StatModifier<StatModifierData> modifier) { generalRes.AddModifier(modifier); }
    public void AddCritChanceModifier(StatModifier<StatModifierData> modifier) { critChance.AddModifier(modifier); }
    public void AddCritDamagerModifier(StatModifier<StatModifierData> modifier) { critDamage.AddModifier(modifier); }
    public void AddMaxHealthModifier(StatModifier<StatModifierData> modifier) { healthSystem.AddModifier(modifier); }

    public void SetAttackBaseValue(float newBase) { attack.BaseValue = newBase; }
    public void SetDefenseBaseValue(float newBase) { defense.BaseValue = newBase; }
    public void SetSpeedBaseValue(float newBase) { speed.BaseValue = newBase; }
    public void SetGeneralResBaseValue(float newBase) { generalRes.BaseValue = newBase; }
    public void SetHealthBaseValue(float newBase) { healthSystem.SetBaseHealth(newBase); }
    public void SetHealthEvent(UnityAction<float> callback)
    {
        healthSystem.AddHealthEvent(callback);
    }
    public void SetMaxHealthEvent(UnityAction<float> callback)
    {
        healthSystem.AddMaxHealthEvent(callback);
    }

    public void RemoveHealthEvent(UnityAction<float> callback)
    {
        healthSystem.RemoveHealthEvent(callback);
    }
    public void RemoveMaxHealthEvent(UnityAction<float> callback)
    {
        healthSystem.RemoveMaxHealthEvent(callback);
    }
}
