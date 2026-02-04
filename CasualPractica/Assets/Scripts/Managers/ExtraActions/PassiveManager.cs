using UnityEngine;

public class PassiveManager
{
    public PassiveManager(ExtraActionManager extraActionManager) 
    {
        onDamageOnEnemyTypeActivated = new Observer<DamageType, ExtraActionManager>(extraActionManager, DamageType.NONE);
        onElementOnEnemyTypeActivated = new Observer<ElementType, ExtraActionManager>(extraActionManager, ElementType.NONE);

        onCharacterAbilityUsed = new Observer<Character, ExtraActionManager>(extraActionManager);
        onCharacterBasicUsed = new Observer<Character, ExtraActionManager>(extraActionManager);
        onCharacterDefinitiveUsed = new Observer<Character, ExtraActionManager>(extraActionManager);
        onCharacterStartTurn = new Observer<Character, ExtraActionManager>(extraActionManager);
        onCharacterEndTurn = new Observer<Character, ExtraActionManager>(extraActionManager);
    }

    private Observer<DamageType,ExtraActionManager> onDamageOnEnemyTypeActivated;
    private Observer<ElementType, ExtraActionManager> onElementOnEnemyTypeActivated;

    public Observer<Character, ExtraActionManager> onCharacterAbilityUsed;
    public Observer<Character, ExtraActionManager> onCharacterBasicUsed;
    public Observer<Character, ExtraActionManager> onCharacterDefinitiveUsed;

    public Observer<Character, ExtraActionManager> onCharacterStartTurn;
    public Observer<Character, ExtraActionManager> onCharacterEndTurn;

    public Observer<DamageType, ExtraActionManager> OnDamageOnEnemyTypeActivated { get { return onDamageOnEnemyTypeActivated; } }
    public Observer<ElementType, ExtraActionManager> OnElementOnEnemyTypeActivated { get { return onElementOnEnemyTypeActivated; } }

    public Observer<Character, ExtraActionManager> OnCharacterAbilityUsed { get { return onCharacterAbilityUsed; } }
    public Observer<Character, ExtraActionManager> OnCharacterBasicUsed { get { return onCharacterBasicUsed; } }
    public Observer<Character, ExtraActionManager> OnCharacterDefinitiveUsed { get { return onCharacterDefinitiveUsed; } }

    public Observer<Character, ExtraActionManager> OnCharacterStartTurn { get { return onCharacterStartTurn; } }
    public Observer<Character, ExtraActionManager> OnCharacterEndTurn { get { return onCharacterEndTurn; } }

}
