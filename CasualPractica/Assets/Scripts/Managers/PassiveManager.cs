using UnityEngine;

public class PassiveManager
{
    private static PassiveManager _i;

    public static PassiveManager i
    {
        get
        {
            if (_i == null)
            {
                _i = new PassiveManager();
            }
            return _i;
        }
    }
    private PassiveManager() 
    {
        onDamageOnEnemyTypeActivated = new Observer<DamageType>(DamageType.NONE);
        onElementOnEnemyTypeActivated = new Observer<ElementType>(ElementType.NONE);

        onCharacterAbilityUsed = new Observer<Character>();
        onCharacterBasicUsed = new Observer<Character>();
        onCharacterDefinitiveUsed = new Observer<Character>();
        onCharacterStartTurn = new Observer<Character>();
        onCharacterEndTurn = new Observer<Character>();
    }

    private Observer<DamageType> onDamageOnEnemyTypeActivated;
    private Observer<ElementType> onElementOnEnemyTypeActivated;

    public Observer<Character> onCharacterAbilityUsed;
    public Observer<Character> onCharacterBasicUsed;
    public Observer<Character> onCharacterDefinitiveUsed;

    public Observer<Character> onCharacterStartTurn;
    public Observer<Character> onCharacterEndTurn;

    public Observer<DamageType> OnDamageOnEnemyTypeActivated { get { return onDamageOnEnemyTypeActivated; } }
    public Observer<ElementType> OnElementOnEnemyTypeActivated { get { return onElementOnEnemyTypeActivated; } }

    public Observer<Character> OnCharacterAbilityUsed { get { return onCharacterAbilityUsed; } }
    public Observer<Character> OnCharacterBasicUsed { get { return onCharacterBasicUsed; } }
    public Observer<Character> OnCharacterDefinitiveUsed { get { return onCharacterDefinitiveUsed; } }

    public Observer<Character> OnCharacterStartTurn { get { return onCharacterStartTurn; } }
    public Observer<Character> OnCharacterEndTurn { get { return onCharacterEndTurn; } }

}
