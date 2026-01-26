using UnityEngine;
using UnityEngine.Events;

public class AbilityPointSystem
{
    private int maxAbilityPoints;
    private int currentAbilityPoints;
    private Observer<int> onAbilityPointChanged;

    public AbilityPointSystem() : this(5,2) { }
    public AbilityPointSystem(int maxAbilityPoints, int currentAbilityPoints)
    {
        this.maxAbilityPoints = maxAbilityPoints;
        this.currentAbilityPoints = currentAbilityPoints;
        onAbilityPointChanged = new Observer<int>();
    }

    public void AddAbilityPointEvent(UnityAction<int> callback)
    {
        onAbilityPointChanged.AddListener(callback);
        onAbilityPointChanged.Invoke(currentAbilityPoints);
    }
    public void RemoveAbilityPointEvent(UnityAction<int> callback)
    {
        onAbilityPointChanged.RemoveListener(callback);
    }
    public void RemoveAbilityPointAllListeners() { onAbilityPointChanged.RemoveAllListeners(); }

    public void IncrementAbilityPoint() {
        if (currentAbilityPoints == maxAbilityPoints) return;
        currentAbilityPoints++; 
        onAbilityPointChanged.Invoke(currentAbilityPoints);
    }
    public void IncrementAbilityPoint(int points) { 
        if (currentAbilityPoints+points > maxAbilityPoints)
        {
            currentAbilityPoints = maxAbilityPoints;
            return;
        }
        currentAbilityPoints+= points;
        onAbilityPointChanged.Invoke(currentAbilityPoints);
    }
    public bool DecreaseAbilityPoint() { 
        if (currentAbilityPoints == 0) return false;
        currentAbilityPoints--;
        onAbilityPointChanged.Invoke(currentAbilityPoints);
        return true;
    }
    public bool DecreaseAbilityPoint(int points) { 
        if (currentAbilityPoints-points >= 0)
        {
            currentAbilityPoints -= points;
            onAbilityPointChanged.Invoke(currentAbilityPoints);
            return true;
        }
        return false;
    }
    public int GetCurrentAbilityPoints() { return currentAbilityPoints; }
    public int GetMaxAbilityPoints() {return maxAbilityPoints;}
    public void IncrementMaxAbilityPoints() { maxAbilityPoints++;}
    public void IncrementMaxAbilityPoints(int points) { maxAbilityPoints+=points; }
    public void DecreaseMaxAbilityPoints()
    {
        maxAbilityPoints--;
        if (maxAbilityPoints < 1)
        {
            maxAbilityPoints = 1;
            if (currentAbilityPoints > maxAbilityPoints) currentAbilityPoints = maxAbilityPoints;
            return;
        }
    }
    public void DecreaseMaxAbilityPoints(int points) {
        maxAbilityPoints-=points;
        if (maxAbilityPoints < 1)
        {
            maxAbilityPoints = 1;
            if (currentAbilityPoints > maxAbilityPoints) currentAbilityPoints = maxAbilityPoints;
            return;
        }
    }
}
