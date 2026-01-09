using UnityEngine;

public class HealthSystem
{
    private BasicStatType maxHealth;
    private float health;

    public HealthSystem(float health)
    {
        this.health = health;
        maxHealth = new BasicStatType(health);
    }

    public void AddModifier(StatModifier<StatModifierData> modifier)
    {
        maxHealth.AddModifier(modifier);
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
    }
    public bool Damage(float damage) 
    { 
        health -= damage;
        if (health < 0)
        {
            health = 0;
            return true;
        }
        return false;
    }
    public void Heal(float heal)
    {
        health += heal;
        if (health > maxHealth.FinalValue) health = maxHealth.FinalValue;
    }

}
