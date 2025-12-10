using UnityEngine;


public abstract class TestCharacter : ScriptableObject
{
    public enum Dir
    {
        UP, DOWN, LEFT, RIGHT
    }

    public string characterName;
    public Transform characterModel;
    public float maxHealth;
    public float health;
    public float atk;
    public float attackSpeed;

    public abstract void normalAttack();
}
