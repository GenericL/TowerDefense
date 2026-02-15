using UnityEngine;

public class CharacterStartingStats
{
    private CharacterStartingStats() { }

    // Playable stats
    public static float PLAYABLE_HP_LOW = 35;
    public static float PLAYABLE_HP_MEDIUM = 53;
    public static float PLAYABLE_HP_HIGH = 91;

    public static float PLAYABLE_ATK_LOW = 12;
    public static float PLAYABLE_ATK_MEDIUM = 23;
    public static float PLAYABLE_ATK_HIGH = 39;

    public static float PLAYABLE_DEF_LOW = 15;
    public static float PLAYABLE_DEF_MEDIUM = 25;
    public static float PLAYABLE_DEF_HIGH = 39;

    public static float PLAYABLE_SPD_LOW = 98;
    public static float PLAYABLE_SPD_MEDIUM = 104;
    public static float PLAYABLE_SPD_HIGH = 120;

    public static int PLAYABLE_ENERGY_REQUIREMENT_LOW = 100;
    public static int PLAYABLE_ENERGY_REQUIREMENT_MEDIUM = 130; 
    public static int PLAYABLE_ENERGY_REQUIREMENT_HIGH = 160;

    // Playable Upgrade Percentage
    public static float PLAYABLE_HP_UPGRADE_LOW = 0.12f;
    public static float PLAYABLE_HP_UPGRADE_MEDIUM = 0.2f;
    public static float PLAYABLE_HP_UPGRADE_HIGH = 0.34f;

    public static float PLAYABLE_ATK_UPGRADE_LOW = 0.18f;
    public static float PLAYABLE_ATK_UPGRADE_MEDIUM = 0.25f;
    public static float PLAYABLE_ATK_UPGRADE_HIGH = 0.4f;

    public static float PLAYABLE_DEF_UPGRADE_LOW = 0.15f;
    public static float PLAYABLE_DEF_UPGRADE_MEDIUM = 0.22f;
    public static float PLAYABLE_DEF_UPGRADE_HIGH = 0.38f;


    // Enemy stats
    public static float ENEMY_HP_LOW = 26;
    public static float ENEMY_HP_MEDIUM = 65;
    public static float ENEMY_HP_HIGH = 102;

    public static float ENEMY_ATK_LOW = 6;
    public static float ENEMY_ATK_MEDIUM = 12;
    public static float ENEMY_ATK_HIGH = 28;

    public static float ENEMY_DEF_LOW = 7;
    public static float ENEMY_DEF_MEDIUM = 25;
    public static float ENEMY_DEF_HIGH = 39;

    public static float ENEMY_SPD_LOW = 90;
    public static float ENEMY_SPD_MEDIUM = 100;
    public static float ENEMY_SPD_HIGH = 120;
}
