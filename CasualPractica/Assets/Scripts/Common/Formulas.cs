using UnityEngine;

public class Formulas
{
    private static Formulas _i;
    public static Formulas i {  get {
            if (_i == null) _i = new Formulas();
            return _i; 
        } }

    private float CalculateBase(float kitMultiplier, float statValue)
    {
        return kitMultiplier * statValue;
    }

    private float CalculateResistance(float ResistenciaTotal)
    {
        if (ResistenciaTotal < 0) return 1 - (ResistenciaTotal / 2);
        if (ResistenciaTotal >= 0 && ResistenciaTotal < 0.75) return 1 - ResistenciaTotal;
        return 1 / ((4 * ResistenciaTotal) + 1);
    }

    private float CalculateDefenseOfEnemy(int lvlCaster, int lvlTarget, float defIgnorance)
    {
        return lvlCaster + 100 / (1+defIgnorance)*(lvlCaster + 100 + lvlTarget + 100);
    }

    private float CalculateMultiplierBonus(float generalDmgBonus, float elementalDmgBonus, float dmgReduction)
    {
        return 1+(generalDmgBonus +  elementalDmgBonus) - dmgReduction;
    }
    private float CalculateDefenseOfPlayable(float defenseTotal, int lvlPlayable)
    {
        return 1 - defenseTotal / (defenseTotal + 5 * lvlPlayable + 500);
    }

    private float CritDamage( float critDamage, float rawDamage)
    {
        return rawDamage * (1 + critDamage);
    }
    private bool IsCriticalHit(float critRate)
    {
        float chance = Random.Range(0, 100);
        return (critRate < chance);
    }
    public float Damage(Character caster, Character target, float kitMultiplier, ElementType elementDMG, KitMultiplierStatType kitMultiplierStatType) 
    {
        CharacterData casterData = caster.GetCharacterData();
        CharacterData targetData = target.GetCharacterData();
        float elementalRes = GetElementalResistance(targetData, elementDMG);
        float elementalBonus = GetElementalBonus(targetData, elementDMG);
        float res = targetData.GetGeneralRes() * elementalRes + elementalRes;
        float resistence = CalculateResistance(res);
        float multBonus = CalculateMultiplierBonus(casterData.GetGeneralBonus(), elementalBonus, targetData.GetDamageReduction());
        float kitMultStat = GetKitMultiplierStat(casterData, kitMultiplierStatType);
        float kitDamage = CalculateBase(kitMultiplier, kitMultStat);


        if (casterData.GetCharacterType() == CharacterType.PLAYABLE)
        {
            float defTarget = CalculateDefenseOfEnemy(casterData.GetLevel(), targetData.GetLevel(), casterData.GetDefIgnorance());
            float rawDamage = kitDamage * multBonus * defTarget * resistence * casterData.GetTotalDamageBonus();

            if (IsCriticalHit(casterData.GetCritChance() * 100))
            {
                float totalDamage = CritDamage(casterData.GetCritDamage(), rawDamage);
                DamagePopUp.Create(target.transform.position, Mathf.Round(totalDamage), true, casterData.GetElementColor());
                return totalDamage;
            } else
            {
                DamagePopUp.Create(target.transform.position, Mathf.Round(rawDamage), false, casterData.GetElementColor());
                return rawDamage;
            }
        }
        else
        {
            float defTarget = CalculateDefenseOfPlayable(targetData.GetFinalDefense(), targetData.GetLevel());
            float rawDamage = kitDamage * multBonus * defTarget * resistence * casterData.GetTotalDamageBonus();

            if (IsCriticalHit(casterData.GetCritChance() * 100)){
                float totalDamage = CritDamage(casterData.GetCritDamage(), rawDamage);
                return totalDamage;
            } else
            {
                return rawDamage;
            }
        }
    }

    private float GetKitMultiplierStat(CharacterData caster, KitMultiplierStatType kitMultiplierStat)
    {
        switch (kitMultiplierStat)
        {
            case KitMultiplierStatType.HP:
                return caster.GetMaxHealth();
            case KitMultiplierStatType.DEF:
                return caster.GetFinalDefense();
            case KitMultiplierStatType.SPD:
                return caster.GetFinalSpeed();
            case KitMultiplierStatType.ATK:
            default:
                return caster.GetFinalAttack();
        }
    }

    private float GetElementalResistance(CharacterData target, ElementType element) {
        switch (element)
        {
            case ElementType.IGNIS:
                return target.GetIgnisRes();
            case ElementType.GLACIES:
                return target.GetGlaciesRes();
            case ElementType.NATURA:
                return target.GetNaturaRes();
            case ElementType.VENTUS:
                return target.GetVentusRes();
            case ElementType.LAPIS:
                return target.GetLapisRes();
            case ElementType.AQUA:
                return target.GetAquaRes();
            case ElementType.FULGUR:
                return target.GetFulgurRes();
            case ElementType.CORPORALIS:
                return target.GetCorporalisRes();
            case ElementType.ILLUSION:
                return target.GetIllusionRes();
            case ElementType.LUX:
                return target.GetLuxRes();
            case ElementType.TENEBRIS:
                return target.GetTenebrisRes();
            case ElementType.VESANIA:
                return target.GetVesaniaRes();
            default:
                return 0;

        }
    }
    private float GetElementalBonus(CharacterData target, ElementType element)
    {
        switch (element)
        {
            case ElementType.IGNIS:
                return target.GetIgnisBonus();
            case ElementType.GLACIES:
                return target.GetGlaciesBonus();
            case ElementType.NATURA:
                return target.GetNaturaBonus();
            case ElementType.VENTUS:
                return target.GetVentusBonus();
            case ElementType.LAPIS:
                return target.GetLapisBonus();
            case ElementType.AQUA:
                return target.GetAquaBonus();
            case ElementType.FULGUR:
                return target.GetFulgurBonus();
            case ElementType.CORPORALIS:
                return target.GetCorporalisBonus();
            case ElementType.ILLUSION:
                return target.GetIllusionBonus();
            case ElementType.LUX:
                return target.GetLuxBonus();
            case ElementType.TENEBRIS:
                return target.GetTenebrisBonus();
            case ElementType.VESANIA:
                return target.GetVesaniaBonus();
            default:
                return 0;

        }
    }

    private float CalculateHealBonus(float healReceivedBonus, float healBonus)
    {
        return 1 + healReceivedBonus + healBonus;
    }


    public float Heal(Character caster, Character target, float kitMultiplier, float flatHealing, KitMultiplierStatType kitMultiplierStatType)
    {
        CharacterData casterData = caster.GetCharacterData();
        CharacterData targetData = target.GetCharacterData();
        float kitMultStat = GetKitMultiplierStat(casterData, kitMultiplierStatType);
        float kitHeal = CalculateBase(kitMultiplier, kitMultStat);
        float healBonus = CalculateHealBonus(targetData.GetHealingRecievedBonus(), casterData.GetHealBonus());

        return kitHeal + flatHealing * healBonus;
    }

    private float CalculateShieldBonus(float shieldReceivedBonus, float shieldBonus)
    {
        return 1 + shieldReceivedBonus + shieldBonus;
    }


    public float Shield(Character caster, Character target, float kitMultiplier, float flatShield, KitMultiplierStatType kitMultiplierStatType)
    {
        CharacterData casterData = caster.GetCharacterData();
        CharacterData targetData = target.GetCharacterData();
        float kitMultStat = GetKitMultiplierStat(casterData, kitMultiplierStatType);
        float kitShield = CalculateBase(kitMultiplier, kitMultStat);
        float shieldBonus = CalculateShieldBonus(targetData.GetShieldReceivedBonus(), casterData.GetShieldBonus());

        return kitShield + flatShield * shieldBonus;
    }
}
