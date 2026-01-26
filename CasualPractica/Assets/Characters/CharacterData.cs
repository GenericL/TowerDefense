
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
    private PercentageStatType ignisRes;
    private PercentageStatType glaciesRes;
    private PercentageStatType naturaRes;
    private PercentageStatType ventusRes;
    private PercentageStatType lapisRes;
    private PercentageStatType aquaRes;
    private PercentageStatType fulgurRes;
    private PercentageStatType corporalisRes;
    private PercentageStatType illusionRes;
    private PercentageStatType luxRes;
    private PercentageStatType tenebrisRes;
    private PercentageStatType vesaniaRes;

    private PercentageStatType generalBonus;
    private PercentageStatType ignisBonus;
    private PercentageStatType glaciesBonus;
    private PercentageStatType naturaBonus;
    private PercentageStatType ventusBonus;
    private PercentageStatType lapisBonus;
    private PercentageStatType aquaBonus;
    private PercentageStatType fulgurBonus;
    private PercentageStatType corporalisBonus;
    private PercentageStatType illusionBonus;
    private PercentageStatType luxBonus;
    private PercentageStatType tenebrisBonus;
    private PercentageStatType vesaniaBonus;

    private PercentageStatType damageReduction;
    private PercentageStatType totalDamageBonus;
    private PercentageStatType defenseIgnorance;

    private PercentageStatType critChance;
    private PercentageStatType critDamage;

    private PercentageStatType healingRecievedBonus;
    private PercentageStatType healBonus;

    private PercentageStatType shieldReceivedBonus;
    private PercentageStatType shieldBonus;

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
        ignisRes = new PercentageStatType();
        glaciesRes = new PercentageStatType();
        naturaRes = new PercentageStatType();
        ventusRes = new PercentageStatType();
        lapisRes = new PercentageStatType();
        aquaRes = new PercentageStatType();
        fulgurRes = new PercentageStatType();
        corporalisRes = new PercentageStatType();
        illusionRes = new PercentageStatType();
        luxRes = new PercentageStatType();
        tenebrisRes = new PercentageStatType();
        vesaniaRes = new PercentageStatType();

        generalBonus = new PercentageStatType();
        ignisBonus = new PercentageStatType();
        glaciesBonus = new PercentageStatType();
        naturaBonus = new PercentageStatType();
        ventusBonus = new PercentageStatType();
        lapisBonus = new PercentageStatType();
        aquaBonus = new PercentageStatType();
        fulgurBonus = new PercentageStatType();
        corporalisBonus = new PercentageStatType();
        illusionBonus = new PercentageStatType();
        luxBonus = new PercentageStatType();
        tenebrisBonus = new PercentageStatType();
        vesaniaBonus = new PercentageStatType();

        damageReduction = new PercentageStatType();
        totalDamageBonus = new PercentageStatType();
        defenseIgnorance = new PercentageStatType();

        critChance = new PercentageStatType(0.05f);
        critDamage = new PercentageStatType(0.5f);

        healingRecievedBonus = new PercentageStatType();
        healBonus = new PercentageStatType();

        shieldReceivedBonus = new PercentageStatType();
        shieldBonus = new PercentageStatType();

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

    public bool DamageRecieved(float amount) { return healthSystem.Damage(amount); }
    public void HealingRecieved(float amount) { healthSystem.Heal(amount); }
    public void ShieldRecieved(float amount) { healthSystem.AddShield(amount); }

    // Getters
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
    public float GetIgnisRes() { return ignisRes.FinalValue; }
    public float GetGlaciesRes() {  return glaciesRes.FinalValue; }
    public float GetNaturaRes() { return naturaRes.FinalValue; }
    public float GetVentusRes() { return ventusRes.FinalValue; }
    public float GetLapisRes() { return lapisRes.FinalValue; }
    public float GetAquaRes() { return aquaRes.FinalValue; }
    public float GetFulgurRes() { return fulgurRes.FinalValue; }
    public float GetCorporalisRes() { return corporalisRes.FinalValue; }
    public float GetIllusionRes() { return illusionRes.FinalValue; }
    public float GetLuxRes() { return luxRes.FinalValue; }
    public float GetTenebrisRes() { return tenebrisRes.FinalValue; }
    public float GetVesaniaRes() { return vesaniaRes.FinalValue; }
    public float GetGeneralBonus() { return generalBonus.FinalValue; }
    public float GetIgnisBonus() { return ignisBonus.FinalValue; }
    public float GetGlaciesBonus() { return glaciesBonus.FinalValue; }
    public float GetNaturaBonus() { return naturaBonus.FinalValue; }
    public float GetVentusBonus() { return ventusBonus.FinalValue; }
    public float GetLapisBonus() { return lapisBonus.FinalValue; }
    public float GetAquaBonus() { return aquaBonus.FinalValue; }
    public float GetFulgurBonus() { return fulgurBonus.FinalValue; }
    public float GetCorporalisBonus() { return corporalisBonus.FinalValue; }
    public float GetIllusionBonus() { return illusionBonus.FinalValue; }
    public float GetLuxBonus() { return luxBonus.FinalValue; }
    public float GetTenebrisBonus() { return tenebrisBonus.FinalValue; }
    public float GetVesaniaBonus() { return vesaniaBonus.FinalValue; }
    public float GetDamageReduction() { return damageReduction.FinalValue; }
    public float GetTotalDamageBonus() { return totalDamageBonus.FinalValue; }
    public float GetDefIgnorance() { return defenseIgnorance.FinalValue; }
    public float GetCritChance() { return critChance.FinalValue; }
    public float GetCritDamage() { return critDamage.FinalValue; }
    public float GetHealingRecievedBonus() { return healingRecievedBonus.FinalValue; }
    public float GetHealBonus() { return healBonus.FinalValue; }
    public float GetShieldReceivedBonus() { return shieldReceivedBonus.FinalValue; }
    public float GetShieldBonus() { return shieldBonus.FinalValue; }
    public float GetMaxHealth() { return healthSystem.GetMaxHealth(); }
    public float GetCurrentHealth() { return healthSystem.GetCurrentHealth(); }
    public float GetPercentHealth() { return healthSystem.GetHealthPercent(); }
    public float GetShield() { return healthSystem.GetShield(); }
    public Color GetElementColor() { return elementColor; }

    // Add Modifiers
    public void AddAttackModifier(StatModifier<StatModifierData> modifier) { attack.AddModifier(modifier); }
    public void AddDefenseModifier(StatModifier<StatModifierData> modifier) { defense.AddModifier(modifier); }
    public void AddSpeedModifier(StatModifier<StatModifierData> modifier) { speed.AddModifier(modifier); }
    public void AddGeneralResModifier(StatModifier<StatModifierData> modifier) { generalRes.AddModifier(modifier); }
    public void AddIgnisResModifier(StatModifier<StatModifierData> modifier) { ignisRes.AddModifier(modifier); }
    public void AddGlaciesResModifier(StatModifier<StatModifierData> modifier) { glaciesRes.AddModifier(modifier); }
    public void AddNaturaResModifier(StatModifier<StatModifierData> modifier) { naturaRes.AddModifier(modifier); }
    public void AddVentusResModifier(StatModifier<StatModifierData> modifier) { ventusRes.AddModifier(modifier); }
    public void AddLapisResModifier(StatModifier<StatModifierData> modifier) { lapisRes.AddModifier(modifier); }
    public void AddAquaResModifier(StatModifier<StatModifierData> modifier) { aquaRes.AddModifier(modifier); }
    public void AddFulgurResModifier(StatModifier<StatModifierData> modifier) { fulgurRes.AddModifier(modifier); }
    public void AddCorporalisResModifier(StatModifier<StatModifierData> modifier) { corporalisRes.AddModifier(modifier); }
    public void AddIllusionResModifier(StatModifier<StatModifierData> modifier) { illusionRes.AddModifier(modifier); }
    public void AddLuxResModifier(StatModifier<StatModifierData> modifier) { luxRes.AddModifier(modifier); }
    public void AddTenebrisResModifier(StatModifier<StatModifierData> modifier) { tenebrisRes.AddModifier(modifier); }
    public void AddVesaniaResModifier(StatModifier<StatModifierData> modifier) { vesaniaRes.AddModifier(modifier); }
    public void AddGeneralBonusModifier(StatModifier<StatModifierData> modifier) { generalBonus.AddModifier(modifier); }
    public void AddIgnisBonusModifier(StatModifier<StatModifierData> modifier) { ignisBonus.AddModifier(modifier); }
    public void AddGlaciesBonusModifier(StatModifier<StatModifierData> modifier) { glaciesBonus.AddModifier(modifier); }
    public void AddNaturaBonusModifier(StatModifier<StatModifierData> modifier) { naturaBonus.AddModifier(modifier); }
    public void AddVentusBonusModifier(StatModifier<StatModifierData> modifier) { ventusBonus.AddModifier(modifier); }
    public void AddLapisBonusModifier(StatModifier<StatModifierData> modifier) { lapisBonus.AddModifier(modifier); }
    public void AddAquaBonusModifier(StatModifier<StatModifierData> modifier) { aquaBonus.AddModifier(modifier); }
    public void AddFulgurBonusModifier(StatModifier<StatModifierData> modifier) { fulgurBonus.AddModifier(modifier); }
    public void AddCorporalisBonusModifier(StatModifier<StatModifierData> modifier) { corporalisBonus.AddModifier(modifier); }
    public void AddIllusionBonusModifier(StatModifier<StatModifierData> modifier) { illusionBonus.AddModifier(modifier); }
    public void AddLuxBonusModifier(StatModifier<StatModifierData> modifier) { luxBonus.AddModifier(modifier); }
    public void AddTenebrisBonusModifier(StatModifier<StatModifierData> modifier) { tenebrisBonus.AddModifier(modifier); }
    public void AddVesaniaBonusModifier(StatModifier<StatModifierData> modifier) { vesaniaBonus.AddModifier(modifier); }
    public void AddDamageReductionModifier(StatModifier<StatModifierData> modifier) {  damageReduction.AddModifier(modifier); }
    public void AddTotalDamageBonusModifier(StatModifier<StatModifierData> modifier) {  totalDamageBonus.AddModifier(modifier); }
    public void AddDefIgnoranceModifier(StatModifier<StatModifierData> modifier) {  defenseIgnorance.AddModifier(modifier); }
    public void AddCritChanceModifier(StatModifier<StatModifierData> modifier) { critChance.AddModifier(modifier); }
    public void AddCritDamagerModifier(StatModifier<StatModifierData> modifier) { critDamage.AddModifier(modifier); }
    public void AddHealingRecievedBonusModifier(StatModifier<StatModifierData> modifier) { healingRecievedBonus.AddModifier(modifier); }
    public void AddHealBonusModifier(StatModifier<StatModifierData> modifier) { healBonus.AddModifier(modifier); }
    public void AddShieldReceivedBonusModifier(StatModifier<StatModifierData> modifier) { shieldReceivedBonus.AddModifier(modifier); }
    public void AddShieldBonusModifier(StatModifier<StatModifierData> modifier) { shieldBonus.AddModifier(modifier); }
    public void AddMaxHealthModifier(StatModifier<StatModifierData> modifier) { healthSystem.AddModifier(modifier); }

    // Setters
    public void SetAttackBaseValue(float newBase) { attack.BaseValue = newBase; }
    public void SetDefenseBaseValue(float newBase) { defense.BaseValue = newBase; }
    public void SetSpeedBaseValue(float newBase) { speed.BaseValue = newBase; }
    public void SetGeneralResBaseValue(float newBase) { generalRes.BaseValue = newBase; }
    public void SetIgnisResBaseValue(float newBase) { ignisRes.BaseValue = newBase; }
    public void SetGlaciesResBaseValue(float newBase) { glaciesRes.BaseValue = newBase; }
    public void SetNaturaResBaseValue(float newBase) { naturaRes.BaseValue = newBase; }
    public void SetVentusResBaseValue(float newBase) { ventusRes.BaseValue = newBase; }
    public void SetLapisResBaseValue(float newBase) { lapisRes.BaseValue = newBase; }
    public void SetAquaResBaseValue(float newBase) { aquaRes.BaseValue = newBase; }
    public void SetFulgurResBaseValue(float newBase) { fulgurRes.BaseValue = newBase; }
    public void SetCorporalisResBaseValue(float newBase) { corporalisRes.BaseValue = newBase; }
    public void SetIllusionResBaseValue(float newBase) { illusionRes.BaseValue = newBase; }
    public void SetLuxResBaseValue(float newBase) { luxRes.BaseValue = newBase; }
    public void SetTenebrisResBaseValue(float newBase) { tenebrisRes.BaseValue = newBase; }
    public void SetVesaniaResBaseValue(float newBase) { vesaniaRes.BaseValue = newBase; }
    public void SetGeneralBonusBaseValue(float newBase) { generalBonus.BaseValue = newBase; }
    public void SetIgnisBonusBaseValue(float newBase) { ignisBonus.BaseValue = newBase; }
    public void SetGlaciesBonusBaseValue(float newBase) { glaciesBonus.BaseValue = newBase; }
    public void SetNaturaBonusBaseValue(float newBase) { naturaBonus.BaseValue = newBase; }
    public void SetVentusBonusBaseValue(float newBase) { ventusBonus.BaseValue = newBase; }
    public void SetLapisBonusBaseValue(float newBase) { lapisBonus.BaseValue = newBase; }
    public void SetAquaBonusBaseValue(float newBase) { aquaBonus.BaseValue = newBase; }
    public void SetFulgurBonusBaseValue(float newBase) { fulgurBonus.BaseValue = newBase; }
    public void SetCorporalisBonusBaseValue(float newBase) { corporalisBonus.BaseValue = newBase; }
    public void SetIllusionBonusBaseValue(float newBase) { illusionBonus.BaseValue = newBase; }
    public void SetLuxBonusBaseValue(float newBase) { luxBonus.BaseValue = newBase; }
    public void SetTenebrisBonusBaseValue(float newBase) { tenebrisBonus.BaseValue = newBase; }
    public void SetVesaniaBonusBaseValue(float newBase) { vesaniaBonus.BaseValue = newBase; }
    public void SetHealthBaseValue(float newBase) { healthSystem.SetBaseHealth(newBase); }
    public void SetHealthEvent(UnityAction<float> callback)
    {
        healthSystem.AddHealthEvent(callback);
    }
    public void SetMaxHealthEvent(UnityAction<float> callback)
    {
        healthSystem.AddMaxHealthEvent(callback);
    }

    // Remove Modifiers
    public void RemoveAttackModifier(StatModifier<StatModifierData> modifier) { attack.RemoveModifier(modifier); }
    public void RemoveDefenseModifier(StatModifier<StatModifierData> modifier) { defense.RemoveModifier(modifier); }
    public void RemoveSpeedModifier(StatModifier<StatModifierData> modifier) { speed.RemoveModifier(modifier); }
    public void RemoveGeneralResModifier(StatModifier<StatModifierData> modifier) { generalRes.RemoveModifier(modifier); }
    public void RemoveIgnisResModifier(StatModifier<StatModifierData> modifier) { ignisRes.RemoveModifier(modifier); }
    public void RemoveGlaciesResModifier(StatModifier<StatModifierData> modifier) { glaciesRes.RemoveModifier(modifier); }
    public void RemoveNaturaResModifier(StatModifier<StatModifierData> modifier) { naturaRes.RemoveModifier(modifier); }
    public void RemoveVentusResModifier(StatModifier<StatModifierData> modifier) { ventusRes.RemoveModifier(modifier); }
    public void RemoveLapisResModifier(StatModifier<StatModifierData> modifier) { lapisRes.RemoveModifier(modifier); }
    public void RemoveAquaResModifier(StatModifier<StatModifierData> modifier) { aquaRes.RemoveModifier(modifier); }
    public void RemoveFulgurResModifier(StatModifier<StatModifierData> modifier) { fulgurRes.RemoveModifier(modifier); }
    public void RemoveCorporalisResModifier(StatModifier<StatModifierData> modifier) { corporalisRes.RemoveModifier(modifier); }
    public void RemoveIllusionResModifier(StatModifier<StatModifierData> modifier) { illusionRes.RemoveModifier(modifier); }
    public void RemoveLuxResModifier(StatModifier<StatModifierData> modifier) { luxRes.RemoveModifier(modifier); }
    public void RemoveTenebrisResModifier(StatModifier<StatModifierData> modifier) { tenebrisRes.RemoveModifier(modifier); }
    public void RemoveVesaniaResModifier(StatModifier<StatModifierData> modifier) { vesaniaRes.RemoveModifier(modifier); }
    public void RemoveGeneralBonusModifier(StatModifier<StatModifierData> modifier) { generalBonus.RemoveModifier(modifier); }
    public void RemoveIgnisBonusModifier(StatModifier<StatModifierData> modifier) { ignisBonus.RemoveModifier(modifier); }
    public void RemoveGlaciesBonusModifier(StatModifier<StatModifierData> modifier) { glaciesBonus.RemoveModifier(modifier); }
    public void RemoveNaturaBonusModifier(StatModifier<StatModifierData> modifier) { naturaBonus.RemoveModifier(modifier); }
    public void RemoveVentusBonusModifier(StatModifier<StatModifierData> modifier) { ventusBonus.RemoveModifier(modifier); }
    public void RemoveLapisBonusModifier(StatModifier<StatModifierData> modifier) { lapisBonus.RemoveModifier(modifier); }
    public void RemoveAquaBonusModifier(StatModifier<StatModifierData> modifier) { aquaBonus.RemoveModifier(modifier); }
    public void RemoveFulgurBonusModifier(StatModifier<StatModifierData> modifier) { fulgurBonus.RemoveModifier(modifier); }
    public void RemoveCorporalisBonusModifier(StatModifier<StatModifierData> modifier) { corporalisBonus.RemoveModifier(modifier); }
    public void RemoveIllusionBonusModifier(StatModifier<StatModifierData> modifier) { illusionBonus.RemoveModifier(modifier); }
    public void RemoveLuxBonusModifier(StatModifier<StatModifierData> modifier) { luxBonus.RemoveModifier(modifier); }
    public void RemoveTenebrisBonusModifier(StatModifier<StatModifierData> modifier) { tenebrisBonus.RemoveModifier(modifier); }
    public void RemoveVesaniaBonusModifier(StatModifier<StatModifierData> modifier) { vesaniaBonus.RemoveModifier(modifier); }
    public void RemoveDamageReductionModifier(StatModifier<StatModifierData> modifier) { damageReduction.RemoveModifier(modifier); }
    public void RemoveTotalDamageBonusModifier(StatModifier<StatModifierData> modifier) { totalDamageBonus.RemoveModifier(modifier); }
    public void RemoveDefIgnoranceModifier(StatModifier<StatModifierData> modifier) { defenseIgnorance.RemoveModifier(modifier); }
    public void RemoveCritChanceModifier(StatModifier<StatModifierData> modifier) { critChance.RemoveModifier(modifier); }
    public void RemoveCritDamagerModifier(StatModifier<StatModifierData> modifier) { critDamage.RemoveModifier(modifier); }
    public void RemoveHealingRecievedBonusModifier(StatModifier<StatModifierData> modifier) { healingRecievedBonus.RemoveModifier(modifier); }
    public void RemoveHealBonusModifier(StatModifier<StatModifierData> modifier) { healBonus.RemoveModifier(modifier); }
    public void RemoveShieldReceivedBonusModifier(StatModifier<StatModifierData> modifier) { shieldReceivedBonus.RemoveModifier(modifier); }
    public void RemoveShieldBonusModifier(StatModifier<StatModifierData> modifier) { shieldBonus.RemoveModifier(modifier); }
    public void RemoveMaxHealthModifier(StatModifier<StatModifierData> modifier) { healthSystem.RemoveModifier(modifier); }

    // Remove Events
    public void RemoveHealthEvent(UnityAction<float> callback)
    {
        healthSystem.RemoveHealthEvent(callback);
    }
    public void RemoveMaxHealthEvent(UnityAction<float> callback)
    {
        healthSystem.RemoveMaxHealthEvent(callback);
    }

}
