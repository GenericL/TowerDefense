using UnityEngine;
[CreateAssetMenu(fileName = "Scripts/ScriptableObjects/TestCharacter", menuName = "Character Data")]
public abstract class TestCharacter : ScriptableObject
{
    public string characterName;
    public float maxHealth;
    public float health;
    public float atk;
    public float dmgCrit = 50f;
    public float probCrit = 5f;
    public float attackSpeed;

    public abstract void normalAttack();
    public abstract void specialAttack();
}
