using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

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
        energySystem = new EnergySystem(0);
    }
    
    public override void Dies()
    {
        PassiveManager.i.onDamageOnEnemyTypeActivated.RemoveListener(SoberanoDeLosMonstruos);
        base.Dies();
    }
    public override void AddListenersToPassiveManager()
    {
        PassiveManager.i.onDamageOnEnemyTypeActivated.AddListener(SoberanoDeLosMonstruos);
    }
    public override bool Ability(Character[] targets, int principalTarget)
    {
        if (enhanced)
        {
            ExecuteAbility(enhancedAbilityAttack, targets, principalTarget);
            Debug.Log("Enhanced Ability used");
            tirano++;
            enhanced = false;
        }
        else
        {
            Debug.Log("Ability used");
            ExecuteAbility(abilityAttack, targets, principalTarget); 
        }
        return true;
    }

    public override bool Basic(Character[] targets, int principalTarget)
    {
        if (enhanced)
        {
            ExecuteAbility(enhancedBasicAttack, targets.ToArray(), principalTarget);
            Debug.Log("Enhanced Basic used");
            rey++;
            enhanced = false;
        }
        else
        {
            Debug.Log("Basic used");
            ExecuteAbility(basicAttack, targets.ToArray(), principalTarget);
        }
        return true;
    }

    public override void Definitive(Character[] targets, int principalTarget)
    {
        if (rey == 3 || tirano == 3)
        {
            rey = 0;
            tirano = 0;
            Debug.Log("State changed!");
        }
        Debug.Log("Can't use ultimate");
    }

    public override void InitialPasive()
    {
        PassiveManager.i.onDamageOnEnemyTypeActivated.AddListener(SoberanoDeLosMonstruos);
    }

    private bool activatedSoberanoDeLosMonstruos = false;
    public void SoberanoDeLosMonstruos(DamageType dmgType)
    {
        if (dmgType.Equals(DamageType.COORDINATED_ATTACK_DMG) && !activatedSoberanoDeLosMonstruos)
        {
            enhanced = true;
        }
    }

    public override bool DoTurn(Character[] targets, int principalTarget)
    {
        // AI logic if wanted
        return true;
    }

    
}
