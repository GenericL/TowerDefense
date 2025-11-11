using NUnit.Framework;
using UnityEngine;

public class CombatantManager : MonoBehaviour
{
    [SerializeField] PlacementManager placementManager;
    public Vector3Int temporaryPlacementPosition = Vector3Int.zero;

    public GameObject combatant;


    public void PlaceCombatant(Vector3Int position)
    {
        if (placementManager.CheckIfPositionInBound(position) == false)
            return;
        else if (placementManager.CheckIfPositionFree(position) == false)
            return;
        placementManager.PlaceTemporaryCombatant(position, combatant);
    }
}
