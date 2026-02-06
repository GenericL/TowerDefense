using System;
using UnityEngine;

public class DisplayCombatantInventoryHUD : MonoBehaviour
{
    [SerializeField] private Transform characterHUDRoot;
    [SerializeField] private GameObject characterHUDPrefab;

    public void SetupHUD(Playable[] characters)
    {
        foreach (Playable character in characters)
        {
            if (character != null)
            {
                Debug.Log(character);
                var hudInstance = Instantiate(characterHUDPrefab, Vector3.zero, Quaternion.identity, characterHUDRoot);
            }
        }
    }
}
