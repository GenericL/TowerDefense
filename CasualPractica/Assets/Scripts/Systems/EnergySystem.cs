

using System;
using UnityEngine.Events;

public class EnergySystem
{
    private readonly int maxEnergy;
    private int currentEnergy;
    private Observer<int> onEnergyChanged;

    public EnergySystem(int maxEnergy):this(maxEnergy, 0) { }

    public EnergySystem(int maxEnergy, int currentEnergy)
    {
        this.maxEnergy = maxEnergy;
        this.currentEnergy = currentEnergy;
        onEnergyChanged = new Observer<int>(currentEnergy);
    }
    public void AddEnergyEvent(UnityAction<int> callback)
    {
        onEnergyChanged.AddListener(callback);
    }
    public void RemoveEnergyEvent(UnityAction<int> callback)
    {
        onEnergyChanged.RemoveListener(callback);
    }
    public int GetCurrentEnergy() { return currentEnergy; }
    public int GetMaxEnergy() { return maxEnergy; }
    public void RestoreEnergy(int energy)
    {
        currentEnergy += energy;
        if (currentEnergy > maxEnergy) currentEnergy = maxEnergy;
        onEnergyChanged.Invoke(currentEnergy);
    }
    public float GetEnergyPercent()
    {
        return currentEnergy / maxEnergy;
    }
    public bool CanConsumeEnergy(int energy)
    {
        if (currentEnergy >= energy)
        {
            currentEnergy -= energy;
            onEnergyChanged.Invoke(currentEnergy);
            return true;
        }
        return false;
    }
    public bool CanConsumeEnergy()
    {
        if (currentEnergy == maxEnergy)
        {
            currentEnergy = 0;
            onEnergyChanged.Invoke(currentEnergy);
            return true;
        }
        return false;
    }

    internal void SetEnergyEvent(UnityAction<int> setEnergy)
    {
        onEnergyChanged.AddListener(setEnergy);
    }

    internal void ConsumeAllEnergy()
    {
        currentEnergy = 0;
        onEnergyChanged.Invoke(currentEnergy);
    }
}
