using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RedeiScript : Playable
{

    [SerializeField] private AbilityData basicAttack;
    [SerializeField] private AbilityData enhancedBasicAttack;

    [SerializeField] private AbilityData abilityAttack;
    [SerializeField] private AbilityData enhancedAbilityAttack;

    private int rey = 0;
    private int tirano = 0;
    private bool enhanced = false;

    private void Awake()
    {
        characterData = new CharacterData("Redei", ElementType.ILLUSION, CharacterType.PLAYABLE, 210, 21, 12, 102);
    }
    

    public override bool Ability(Character[] targets, int principalTarget)
    {
        if (enhanced)
        {
            ExecuteAbility(enhancedAbilityAttack, targets, principalTarget);
            tirano++;
            enhanced = false;
        }
        else { 
            ExecuteAbility(abilityAttack, targets, principalTarget); 
        }
        return true;
    }

    public override bool Basic(Character[] targets, int principalTarget)
    {
        if (enhanced)
        {
            ExecuteAbility(enhancedBasicAttack, targets.ToArray(), principalTarget);
            rey++;
            enhanced = false;
        }
        else
        {
            ExecuteAbility(basicAttack, targets.ToArray(), principalTarget);
        }
        return true;
    }

    public override bool Definitive(Character[] targets, int principalTarget)
    {
        if (rey == 3 || tirano == 3)
        {
            rey = 0;
            tirano = 0;
            Debug.Log("State changed!");
            return true;
        }
        Debug.Log("Can't use ultimate");
        return false;
    }

    public override void InitialPasive()
    {
        
    }

    private bool activatedSoberanoDeLosMonstruos = false;
    public void SoberanoDeLosMonstruos(DamageType dmgType)
    {
        if (dmgType.Equals(DamageType.COORDINATED_ATTACK_DMG) && !activatedSoberanoDeLosMonstruos)
        {
            enhanced = true;
        }
    }
}
