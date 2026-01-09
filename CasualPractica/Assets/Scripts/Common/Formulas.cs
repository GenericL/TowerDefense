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

    private float CritDamage(float critRate, float critDamage, float rawDamage)
    {
        float chance = Random.Range(0,100);
        if(critRate < chance) return rawDamage;
        return rawDamage * (1 + critDamage);
    }

    public float Damage(CharacterData caster, CharacterData target, float kitMultiplier) 
    {
        if(caster.GetCharacterType() == CharacterType.PLAYABLE)
        {
            float defTarget = CalculateDefenseOfEnemy(caster.GetLevel(), target.GetLevel());
            float resistence = CalculateResistance(target.GetGeneralRes());
            float kitDamage = CalculateBaseDamage(kitMultiplier, caster.GetFinalAttack());

            //caster.GetCritChance(), caster.GetCritDamage(), kitMultiplier * defTarget * resistence
            return CritDamage(caster.GetCritChance(), caster.GetCritDamage(), kitDamage * defTarget * resistence);
        }
        else
        {
            // TODO: actualizar a (kitMult(1+BaseDMGMultiplier)+flatDMG)*MultBonus*DefTarget*RES*DMGMult
            float defTarget = CalculateDefenseOfPlayable(target.GetFinalDefense(), target.GetLevel());
            float resistence = CalculateResistance(target.GetGeneralRes());
            float kitDamage = CalculateBaseDamage(kitMultiplier, caster.GetFinalAttack());
            return CritDamage(caster.GetCritChance()*100, caster.GetCritDamage(), kitDamage * defTarget * resistence);
        }
    }
}
