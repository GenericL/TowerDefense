using System;
using UnityEngine;

public class DisplayBattleHUD : MonoBehaviour
{
    [SerializeField] private Transform characterHUDRoot;
    [SerializeField] private GameObject characterHUDPrefab;

    public void SetupHUD(Playable[] characters, Action<Playable> onUltimateAttackButton)
    {
        Debug.Log(characters[0]);
        foreach (Playable character in characters)
        {
            Debug.Log(character);
            var hudInstance = Instantiate(characterHUDPrefab, Vector3.zero, Quaternion.identity, characterHUDRoot);

            var hudScript = hudInstance.GetComponent<BattleHUD>();

            hudScript.SetHUD(character, onUltimateAttackButton);
        }
    }
}
