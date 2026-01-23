using UnityEngine;

public class GameAssets : MonoBehaviour
{
    // Initializing Singleton
    private static GameAssets _i;
    public static GameAssets i
    {
        get
        {
            if (_i == null) _i = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            return _i;
        }
    }

    // Put any prebaf that's later needed for other scripts
    public Transform pfDamagePopUp;
}

