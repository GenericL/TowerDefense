using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected CharacterData characterData;
    private TurnSystem turnSystem = new TurnSystem();
    private bool isDead = false;
    public CharacterData GetCharacterData() { return characterData; }
    public bool GetTurn() { return turnSystem.CanObtainTurn(characterData.GetFinalSpeed()); }
    public bool AvanceTurn(float avance) { return turnSystem.AvanceAction(avance); }
    public void ResetTurnValue() { turnSystem.Reset(); }
    public int GetTurnValue() { return turnSystem.GetAvanceAproximate(); }
    public bool IsDead() { return isDead; }
    public void Dies() { isDead = true; }
    public void Revives() {  isDead = false; }

    protected void ExecuteAbility(AbilityData abilityData, Character[] targets, int principalTarget)
    {
        foreach (var effect in abilityData.effects)
        {
            effect.Execute(this, targets, principalTarget);
        }
    }
}
