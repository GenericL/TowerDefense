
using Sirenix.Utilities;
using System.Collections.Generic;
using UnityEngine.Events;

public class HealthSystem
{
    private BasicStatType maxHealth;
    private float health;
    private List<StatModifier<StatModifierData>> dotDMG;

    private Observer<float> onHealthChanged;
    private Observer<float> onMaxHealthChanged;

    public HealthSystem(float health)
    {
        this.health = health;
        onHealthChanged = new Observer<float>(health);
        dotDMG = new List<StatModifier<StatModifierData>>();

        maxHealth = new BasicStatType(health);
        onMaxHealthChanged = new Observer<float>(maxHealth.FinalValue);
    }

    public void AddModifier(StatModifier<StatModifierData> modifier)
    {
        maxHealth.AddModifier(modifier);
        onMaxHealthChanged?.Invoke(maxHealth.FinalValue);
    }
    public void AddMaxHealthEvent(UnityAction<float> callback)
    {
        onMaxHealthChanged.AddListener(callback);
    }
    public void AddHealthEvent(UnityAction<float> callback)
    {
        onHealthChanged.AddListener(callback);
    }
    public void RemoveModifier(StatModifier<StatModifierData> modifier)
    {
        maxHealth.RemoveModifier(modifier);
        onMaxHealthChanged?.Invoke(maxHealth.FinalValue);
    }
    public void RemoveMaxHealthEvent(UnityAction<float> callback)
    {
        onMaxHealthChanged.RemoveListener(callback);
    }
    public void RemoveHealthEvent(UnityAction<float> callback)
    {
        onHealthChanged.RemoveListener(callback);
    }
    public float GetMaxHealth() 
    {
        return maxHealth.FinalValue;
    }
    public float GetCurrentHealth() { return health; }

    public float GetHealthPercent()
    {
        return health / maxHealth.FinalValue;
    }
    public void SetBaseHealth(float health) 
    { 
        maxHealth.BaseValue = health;
        this.health = maxHealth.FinalValue;
        onMaxHealthChanged?.Invoke(maxHealth.FinalValue);
        onHealthChanged?.Invoke(this.health);
    }
    public bool TickDoT()
    {
        bool death = false;
        if (!dotDMG.IsNullOrEmpty())
        {
            foreach (var dot in dotDMG)
            {
                death = Damage(dot.value);
                dot.UpdateTimer();
            }
        }
        return death;
    }
    public bool Damage(float damage) 
    { 
        health -= damage;
        bool death = false;
        if (health < 0)
        {
            health = 0;
            death = true;
        }
        onHealthChanged?.Invoke(health);
        return death;
    }
    public void Heal(float heal)
    {
        health += heal;
        if (health > maxHealth.FinalValue) health = maxHealth.FinalValue;
        onHealthChanged?.Invoke(health);
    }

}
