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
    public virtual void Dies() { isDead = true; }
    public void Revives() {  isDead = false; }

    public virtual bool Basic(Character[] targets, int principalTarget)
    {
        PassiveManager.i.onCharacterBasicUsed.Invoke(this);
        return true;
    }
    public virtual bool Ability(Character[] targets, int principalTarget)
    {
        PassiveManager.i.onCharacterAbilityUsed.Invoke(this);
        return true;
    }
    public virtual void Definitive(Character[] targets, int principalTarget)
    {
        PassiveManager.i.onCharacterDefinitiveUsed.Invoke(this);
    }
    public abstract bool DoTurn(Character[] targets, int principalTarget);
    public abstract void AddListenersToPassiveManager();

    public void AddStatus(Status status)
    {
        status.ApplyStatus();
        statuses.Add(status);
    }
    public void RemoveStatus(Status status)
    {
        statuses.Find(current => current == status).RemoveStatus();
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

    protected void ExecuteAbility(AbilityData abilityData, Character[] targets, int principalTarget)
    {
        foreach (var effect in abilityData.effects)
        {
            effect.Execute(this, targets, principalTarget);
        }
    }
}
