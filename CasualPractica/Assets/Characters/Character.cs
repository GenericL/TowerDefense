using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected CharacterData characterData;
    private TurnSystem turnSystem = new TurnSystem();
    private bool isDead = false;
    private List<Status> statuses = new List<Status>();
    public CharacterData GetCharacterData() { return characterData; }

    public bool GetTurn() { return turnSystem.CanObtainTurn(characterData.GetFinalSpeed()); }
    public int GetTurnValue() { return turnSystem.GetAvanceAproximate(); }
    public bool AvanceTurn(float avance) { return turnSystem.AvanceAction(avance); }
    public void ResetTurnValue() { turnSystem.Reset(); }

    public bool IsDead() { return isDead; }
    public virtual void Dies(ExtraActionManager extraActionManager) { isDead = true; }
    public void Revives() {  isDead = false; }

    public virtual bool Basic(Character[] targets, int principalTarget, AbilityPointSystem abilityPoints, ExtraActionManager extraActionManager)
    {
        extraActionManager.PassiveManager.OnCharacterBasicUsed.Invoke(this);
        return true;
    }
    public virtual bool Ability(Character[] targets, int principalTarget, AbilityPointSystem abilityPoints, ExtraActionManager extraActionManager)
    {
        extraActionManager.PassiveManager.OnCharacterAbilityUsed.Invoke(this);
        return true;
    }
    public virtual void Definitive(Character[] targets, int principalTarget, AbilityPointSystem abilityPoints, ExtraActionManager extraActionManager)
    {
        extraActionManager.PassiveManager.OnCharacterDefinitiveUsed.Invoke(this);
    }
    public abstract bool DoTurn(Character[] targets, int principalTarget, AbilityPointSystem abilityPoints, ExtraActionManager extraActionManager);

    public void AddStatus(Status status, ExtraActionManager extraActionManager)
    {
        statuses.Add(status);
        status.ApplyStatus(extraActionManager);
    }
    public void RemoveStatus(Status status, ExtraActionManager extraActionManager)
    {
        statuses.Find(current => current == status).RemoveStatus(extraActionManager);
        statuses.Remove(status);
    }
    public void UpdateStatuses()
    {
        statuses.ForEach(status => status.UpdateStatus());
    }
    public void RemoveAllStatus()
    {
        statuses.Clear();
    }
    public List<Status> GetStatuses()
    {
        return statuses;
    }

    protected void ExecuteAbility(AbilityData abilityData, Character[] targets, int principalTarget, ExtraActionManager extraActionManager)
    {
        foreach (var effect in abilityData.effects)
        {
            effect.Execute(this, targets, principalTarget, extraActionManager);
        }
    }
}
