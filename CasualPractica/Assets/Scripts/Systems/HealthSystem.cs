using System.Collections.Generic;
using UnityEngine.Events;

public class HealthSystem
{
    private BasicStatType maxHealth;
    private float health;
    private List<ShieldEffect> shields;

    private Observer<float> onHealthChanged;
    private Observer<float> onMaxHealthChanged;

    public HealthSystem(float health)
    {
        this.health = health;
        onHealthChanged = new Observer<float>(health);
        shields = new List<ShieldEffect>();

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
    public void AddShield(ShieldEffect shieldEffect) 
    {
        ShieldEffect shield = shields.Find(shield => shield.Equals(shieldEffect.GetCharacter()));
        if (shield != null)
        {
            shields.Find(shield => shield.Equals(shieldEffect.GetCharacter())).AddAmount(shieldEffect.GetAmount());
        }
        else
        {
            shields.Add(shieldEffect);
            shields.Sort();
        }
    }
    public void RemoveAllShields() { shields.Clear(); }
    public void RemoveUsedShields() { shields.RemoveAll(shield => shield.GetAmount() < 1); }
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
    public float GetShield() 
    {
        float shield = 0;
        shields.ForEach(shieldEffect => shield += shieldEffect.GetAmount());
        return shield; 
    }

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
    public bool Damage(float damage)
    {
        bool death = false;
        float overflowDamage = -damage;
        shields.ForEach(shieldEffect =>
        {
            if (overflowDamage < 0) {
                overflowDamage = shieldEffect.RemoveAmount(overflowDamage);
            }
            else return;
        });
        if (overflowDamage < 0)
        {
            health += overflowDamage;
            if (health < 0)
            {
                health = 0;
                death = true;
            }
            onHealthChanged?.Invoke(health);
        }
        return death;
    }
    public void Heal(float heal)
    {
        health += heal;
        if (health > maxHealth.FinalValue) health = maxHealth.FinalValue;
        onHealthChanged?.Invoke(health);
    }
}
