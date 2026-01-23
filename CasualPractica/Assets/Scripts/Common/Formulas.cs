using UnityEngine;

public class Formulas
{
    private static Formulas _i;
    public static Formulas i {  get {
            if (_i == null) _i = new Formulas();
            return _i; 
        } }

    private float CalculateBaseDamage(float kitMultiplier, float atk)
    {
        return kitMultiplier * atk;
    }

    private float CalculateResistance(float ResistenciaTotal)
    {
        if (ResistenciaTotal < 0) return 1 - (ResistenciaTotal / 2);
        if (ResistenciaTotal >= 0 && ResistenciaTotal < 0.75) return 1 - ResistenciaTotal;
        return 1 / ((4 * ResistenciaTotal) + 1);
    }

    private float CalculateDefenseOfEnemy(int lvlCaster, int lvlTarget)
    {
        // TODO: Actualizarla a 1 + (lvlCaster+100/(((1-debuff)+(1+defIgnorance))*(lvlCaster+100)+(lvlTarget+100)))
        return lvlCaster + 100 / (lvlCaster + 100 + lvlTarget + 100);
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
    public float Damage(Character caster, Character target, float kitMultiplier) 
    {
        CharacterData casterData = caster.GetCharacterData();
        CharacterData targetData = target.GetCharacterData();
        if(casterData.GetCharacterType() == CharacterType.PLAYABLE)
        {
            float defTarget = CalculateDefenseOfEnemy(casterData.GetLevel(), targetData.GetLevel());
            float resistence = CalculateResistance(targetData.GetGeneralRes());
            float kitDamage = CalculateBaseDamage(kitMultiplier, casterData.GetFinalAttack());

            //caster.GetCritChance(), caster.GetCritDamage(), kitMultiplier * defTarget * resistence
            float rawDamage = kitDamage * defTarget * resistence;
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
            // TODO: actualizar a (kitMult(1+BaseDMGMultiplier)+flatDMG)*MultBonus*DefTarget*RES*DMGMult
            float defTarget = CalculateDefenseOfPlayable(targetData.GetFinalDefense(), targetData.GetLevel());
            float resistence = CalculateResistance(targetData.GetGeneralRes());
            float kitDamage = CalculateBaseDamage(kitMultiplier, casterData.GetFinalAttack());
            float rawDamage = kitDamage * defTarget * resistence;
            if (IsCriticalHit(casterData.GetCritChance() * 100)){
                float totalDamage = CritDamage(casterData.GetCritDamage(), rawDamage);
                return totalDamage;
            } else
            {
                return rawDamage;
            }
        }
    }
}
