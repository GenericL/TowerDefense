using UnityEngine;

public class AbilityExecutor : MonoBehaviour
{
    [SerializeField] AbilityData ability;
    [SerializeField] GameObject target;

    public void Execute(GameObject target)
    {
        foreach (var effect in ability.effects)
        {
            
        }
    }
}
