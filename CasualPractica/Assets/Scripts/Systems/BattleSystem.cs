using Sirenix.Utilities;
using System.Collections.Generic;
using System.Linq;
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

    private Playable[] playableList;
    private Enemy[] enemyList;
    [SerializeField] private DisplayBattleHUD displayBattleHUD;
    [SerializeField] private AttacksButtons attackButtons;
    [SerializeField] private CinemachineBattleSwitcher cinemachineBattleSwitcher;
    private ExtraActionManager extraActionManager;

    private Character active;
    private int primaryTarget;
    private AbilityPointSystem abilityPoints;

    private BattleState state;
    private bool battleEnded = false;
    void Start()
    {
        extraActionManager = new ExtraActionManager();
        abilityPoints = new AbilityPointSystem();
        state = BattleState.WAITING_TURN;
        SetupBattle();
    }

    private void SetupBattle()
    {
        var playableList = new Playable[4];
        for (int i = 0; i < playables.Count && i < playablesPositions.Count; i++)
        {
            GameObject playableGO = Instantiate(playables[i], playablesPositions[i]);
            cinemachineBattleSwitcher.setCamerasLookAt(playableGO, i);
            Playable playableUnit = playableGO.GetComponent<Playable>();
            playableList[i] = playableUnit;
        }
        this.playableList = playableList;

        enemyList = new Enemy[5];
        for (int i = 0; i < enemies.Count && i < enemiesPositions.Count; i++)
        {
            GameObject enemyGO = Instantiate(enemies[i], enemiesPositions[i]);
            enemyList[i] = enemyGO.GetComponent<Enemy>();
        }
        attackButtons.Initialize(OnBasicAttackButton, OnAbilityAttackButton, abilityPoints);
        attackButtons.DisableButtons();
        displayBattleHUD.SetupHUD(this.playableList, OnUltimateAttackButton);
        ExecuteInitialSetup();

        CheckState();
    }

    private void ExecuteInitialSetup()
    {
        playableList.ForEach(item => {
            if (item != null)
            {
                item.InitialSetup(enemyList, playableList, extraActionManager);
            }
        });
        playableList.ForEach(item => {
            if (item != null)
            {
                item.InitialPasive(enemyList, playableList, abilityPoints, extraActionManager);
            }
        });
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
        extraActionManager.AdditionalTurnManager.Invoke();

        while (!battleEnded)
        {
            for (int i = 0; i < playableList.Length; i++)
            {
                if (playableList[i] != null && playableList[i].GetTurn() && !playableList[i].IsDead())
                {
                    active = playableList[i];
                    cinemachineBattleSwitcher.SwitchState(FindSwitchState(i));
                    PlayerAction();
                    return;
                }
            }
            foreach (var enemy in enemyList)
            {
                if (enemy != null && enemy.GetTurn() && !enemy.IsDead())
                {
                    active = enemy;
                    EnemyTurn();
                    return;
                }
            }
        }
    }

    private CombatUIState FindSwitchState(int i)
    {
        switch (i)
        {
            case 0:
                return CombatUIState.PLAYABLE_1_TURN;
            case 1:
                return CombatUIState.PLAYABLE_2_TURN;
            case 2:
                return CombatUIState.PLAYABLE_3_TURN;
            case 3:
                return CombatUIState.PLAYABLE_4_TURN;
            default:
                return CombatUIState.WAITING_TURN;
        }
    }

    public void PlayerAction()
    {
        attackButtons.EnableButtons();
        attackButtons.SetColors(active.GetCharacterData().GetElementColor());
        Debug.Log("Playable Turn: " + active.GetCharacterData().GetCharacterName());
        active.UpdateStatuses();
        // Camera focus on activePlayable
    }

    public void OnBasicAttackButton()
    {
        bool endTurn = active.Basic(enemyList.ToArray(), primaryTarget, abilityPoints, extraActionManager);
        attackButtons.DisableButtons();
        if (CheckIfEnemiesDead()) SettingState(BattleState.WON);
        else if (endTurn)
        {
            attackButtons.DisableButtons();
            SettingState(BattleState.WAITING_TURN);
        }
    }

    public void OnAbilityAttackButton()
    {
        bool endTurn = active.Ability(enemyList.ToArray(), primaryTarget, abilityPoints, extraActionManager);
        if (CheckIfEnemiesDead()) SettingState(BattleState.WON);
        else if (endTurn)
        {
            attackButtons.DisableButtons();
            SettingState(BattleState.WAITING_TURN);
        }
    }
    public void OnUltimateAttackButton(Playable caster)
    {
        caster.Definitive(enemyList.ToArray(), primaryTarget, abilityPoints, extraActionManager);
        CheckIfEnemiesDead();
    }

    private bool CheckIfEnemiesDead()
    {
        int deadEnemies = 0;
        for (int i = enemyList.Length - 1; i >= 0; i--)
        {
            if (enemyList[i] != null && enemyList[i].IsDead() || enemyList[i] == null)
            {
                deadEnemies++;
            }
        }
        if (deadEnemies == enemyList.Length)
        {
            Debug.Log("All enemies defeated!");
            battleEnded = true;
            return true;
        }
        CheckIfTargetNotExist();
        return false;
    }
    private void CheckIfTargetNotExist()
    {
        for (int i = 0; i < enemyList.Length; i++)
        {
            if (enemyList[i] != null && !enemyList[i].IsDead())
            {
                primaryTarget = i;
                return;
            }
        }
    }

    public void EnemyTurn()
    {
        Debug.Log("Enemy Turn: " + active.name);
        int target = Random.Range(0, (playableList.Length));
        while (playableList[target] == null || playableList[target].IsDead())
        {
            target = Random.Range(0, (playableList.Length));
        }
        bool endTurn = active.DoTurn(playableList, target, abilityPoints, extraActionManager);
        if (CheckIfPlayablesDead()) SettingState(BattleState.LOST);
        else if (endTurn) {
            SettingState(BattleState.WAITING_TURN); 
        }
    }
    private bool CheckIfPlayablesDead()
    {
        int deadPlayables = 0;
        foreach (var playable in playableList)
        {
            if (playable == null || playable.IsDead())
            {
                deadPlayables++;
            }
        }
        if (deadPlayables == playableList.Length)
        {
            Debug.Log("All playables defeated!");
            battleEnded = true;
            return true;
        }
        return false;
    }

    private void LostBattle()
    {
        Debug.Log("Lost");
    }

    private void WonBattle()
    {
        Debug.Log("Won");
    }
    private void SettingState(BattleState state)
    {
        this.state = state;
        CheckState();
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
