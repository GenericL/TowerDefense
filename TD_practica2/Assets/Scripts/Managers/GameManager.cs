using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] InputManager inputManager;
    [SerializeField] CombatantManager combatantManager;


    private void Start()
    {
        inputManager.OnMouseClick += HandleMouseClick;
    }

    private void HandleMouseClick(Vector3Int position)
    {
        Debug.Log(position);
        combatantManager.PlaceCombatant(position);
    }
}
