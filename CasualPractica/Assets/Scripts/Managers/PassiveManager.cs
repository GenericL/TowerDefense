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

    public Observer<DamageType> onDamageOnEnemyTypeActivated = new Observer<DamageType>(DamageType.NONE);
    public Observer<ElementType> onElementOnEnemyTypeActivated = new Observer<ElementType>(ElementType.NONE);


}
