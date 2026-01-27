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
    private PassiveManager() { }

    private Observer<DamageType> onDamageOnEnemyTypeActivated = new Observer<DamageType>(DamageType.NONE);
    public Observer<ElementType> onElementOnEnemyTypeActivated = new Observer<ElementType>(ElementType.NONE);

    public Observer<Character> onCharacterAbilityUsed = new Observer<Character>();
    public Observer<Character> onCharacterBasicUsed = new Observer<Character>();
    public Observer<Character> onCharacterDefinitiveUsed = new Observer<Character>();

    public Observer<Character> onCharacterStartTurn = new Observer<Character>();
    public Observer<Character> onCharacterEndTurn = new Observer<Character>();

    public Observer<DamageType> OnDamageOnEnemyTypeActivated { get { return onDamageOnEnemyTypeActivated; } }
    public Observer<ElementType> OnElementOnEnemyTypeActivated { get { return onElementOnEnemyTypeActivated; } }

    public Observer<Character> OnCharacterAbilityUsed { get { return onCharacterAbilityUsed; } }
    public Observer<Character> OnCharacterBasicUsed { get { return onCharacterBasicUsed; } }
    public Observer<Character> OnCharacterDefinitiveUsed { get { return onCharacterDefinitiveUsed; } }

    public Observer<Character> OnCharacterStartTurn { get { return onCharacterStartTurn; } }
    public Observer<Character> OnCharacterEndTurn { get { return onCharacterEndTurn; } }

}
