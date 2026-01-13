using Sirenix.Utilities;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState
{
    PLAYERTURN, ENEMYTURN, WON, LOST, WAITING_TURN
}
public class BattleSystem : MonoBehaviour
{
    [SerializeField] private List<GameObject> playables = new List<GameObject>();
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();

    [SerializeField] private List<Transform> playablesPositions = new List<Transform>();
    [SerializeField] private List<Transform> enemiesPositions = new List<Transform>();

    private Playable[] playableList = new Playable[4];
    private List<Enemy> enemyList = new List<Enemy>();
    [SerializeField] private List<BattleHUD> playablesHUDs = new List<BattleHUD>();

    private Playable activePlayable;

    private int primaryTarget;

    private BattleState state;
    void Start()
    {
        state = BattleState.WAITING_TURN;
        SetupBattle();
    }

    private void SetupBattle()
    {
        for (int i = 0; i < playables.Count || i < playablesPositions.Count; i++)
        {
            GameObject playableGO = Instantiate(playables[i], playablesPositions[i]);
            Playable playableUnit = playableGO.GetComponent<Playable>();
            playableList[i] = playableUnit;
            playablesHUDs[i].SetHUD(playableUnit);
        }

        for (int i = 0; i < enemies.Count || i < enemiesPositions.Count; i++)
        {
            GameObject enemyGO = Instantiate(enemies[i], enemiesPositions[i]);
            enemyList.Add(enemyGO.GetComponent<Enemy>());
        }

        CheckState();
    }

    private void CheckState()
    {

        switch (state)
        {
            case BattleState.WAITING_TURN:
                WaitingTurn();
                break;
            case BattleState.WON:
                WonBattle();
                break;
            case BattleState.LOST:
                LostBattle();
                break;
            default:
                break;
        }
    }

    private void WaitingTurn()
    {
        while (true)
        {
            foreach (var item in playableList)
            {
                if (item.GetTurn() && !item.IsDead())
                {
                    state = BattleState.PLAYERTURN;
                    PlayerTurn(item);
                    return;
                }
            }

            foreach (var item in enemyList)
            {
                if (item.GetTurn() && !item.IsDead())
                {
                    state = BattleState.ENEMYTURN;
                    EnemyTurn(item);
                    return;
                }
            }
        }
    }

    private void PlayerTurn(Playable character)
    {
        activePlayable = character;
        Debug.Log(character.GetCharacterData().GetCharacterName() + " turn");
        // Do the thing for the camera to zoom on character
    }
    public void OnBasicAttackButton()
    {
        if (state != BattleState.PLAYERTURN) return;
        bool endTurn = activePlayable.Basic(enemyList.ToArray(), primaryTarget);
        UpdateOrCheckEnemyHealth(endTurn);
    }

    public void OnAbilityAttackButton()
    {
        if (state != BattleState.PLAYERTURN) return;
        bool endTurn = activePlayable.Ability(enemyList.ToArray(), primaryTarget);
        UpdateOrCheckEnemyHealth(endTurn);
    }

    public void OnUltimateButton()
    {
        if (state != BattleState.PLAYERTURN) return;
        bool endTurn = activePlayable.Definitive(enemyList.ToArray(), primaryTarget);
        UpdateOrCheckEnemyHealth(endTurn);
    }

    private void UpdateOrCheckEnemyHealth(bool endTurn)
    {
        foreach (var item in enemyList)
        {
            item.SetHealth(item.GetCharacterData().GetCurrentHealth());
        }
        CheckIfEnemiesDeath();
        if (!endTurn)
        {
            PlayerTurn(activePlayable);
        }

        CheckState();
    }

    private void EnemyTurn(Enemy enemy)
    {
        int target = Random.Range(0, (playableList.Length));
        while (playableList[target].IsDead())
        {
            target = Random.Range(0, (playableList.Length));
        }
        bool endTurn = enemy.DoTurn(playableList, target);

        for (int i = 0; i < playableList.Length; i++)
        {
            playablesHUDs[i].SetHP(playableList[i].GetCharacterData().GetCurrentHealth());
        }
        CheckIfPlayablesDeath();
        if (!endTurn)
        {
            EnemyTurn(enemy);
        }
        CheckState();
    }
    private void CheckIfEnemiesDeath()
    {
        for (int i = enemyList.Count - 1; i >= 0; i--)
        {
           if (enemyList[i].IsDead())
           {
              if (i == enemyList.Count) primaryTarget--;
              enemyList.RemoveAt(i);
           }
        }
        if (enemyList.IsNullOrEmpty()) state = BattleState.WON;
        else state = BattleState.WAITING_TURN;
    }
    private void CheckIfPlayablesDeath()
    {
        int death = 0;
        playableList.ForEach(item => { if (item.IsDead()) death++; });
        if (death == playableList.Length) state = BattleState.LOST;
        else state = BattleState.WAITING_TURN;
    }

    private void LostBattle()
    {
        Debug.Log("Lost");
    }

    private void WonBattle()
    {
        Debug.Log("Won");
    }
    private void Update()
    {
        if (state == BattleState.PLAYERTURN)
        {
            OnLeft();
            OnRight();
        }
    }

    private void OnLeft()
    {
        if (Input.GetKeyDown(KeyCode.A) && primaryTarget > 0)
        {
            primaryTarget--;
        }
    }
    void OnRight()
    {
        if (Input.GetKeyDown(KeyCode.D) && enemiesPositions.Count - 1 > primaryTarget)
        {
            primaryTarget++;
        }
    }
}
