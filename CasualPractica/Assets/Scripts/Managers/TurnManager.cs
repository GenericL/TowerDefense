using Sirenix.Utilities;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class AdditionalTurnEvent 
{
    private static AdditionalTurnEvent _i;

    public static AdditionalTurnEvent i
    {
        get
        {
            if (_i == null)
            {
                _i = new AdditionalTurnEvent();
            }
            return _i;
        }
    }
    private AdditionalTurnEvent() {  }

    private event UnityAction additionalTurnQueue;
    public event UnityAction AdditionalTurnQueue
    {
        add { additionalTurnQueue += value; }
        remove { additionalTurnQueue -= value; }
    }
    public void Invoke()
    {
        additionalTurnQueue?.Invoke();
    }
}
public class TurnManager : MonoBehaviour
{
    private BattleSystem battleSystem;

    private Playable[] playableList;
    public Playable[] PlayableList { get { return playableList; } }
    private Enemy[] enemyList;
    public Enemy[] EnemyList { get { return enemyList; } }

    private AttacksButtons attacksButtons;

    private Character active;

    private int primaryTarget;
    private bool battleEnded = false;

    public void SetupManager(BattleSystem battleSystem, AttacksButtons attacksButtons, Playable[] playables, Enemy[] enemies)
    {
        playableList = playables;
        enemyList = enemies;
        this.battleSystem = battleSystem;
        attacksButtons.Initialize(OnBasicAttackButton, OnAbilityAttackButton);
        attacksButtons.DisableButtons();
        this.attacksButtons = attacksButtons;
    }

    public void StartTurnCycle()
    {
        AdditionalTurnEvent.i.Invoke();

        while (!battleEnded)
        {
            foreach (var playable in playableList)
            {
                if (playable.GetTurn() && !playable.IsDead())
                {
                    active = playable;
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

    public void PlayerAction()
    {
        attacksButtons.EnableButtons();
        attacksButtons.SetColors(active.GetCharacterData().GetElementColor());
        Debug.Log("Playable Turn: " + active.name);
        // Camera focus on activePlayable
    }

    public void EnemyTurn()
    {
        Debug.Log("Enemy Turn: " + active.name);
        int target = Random.Range(0, (playableList.Length));
        while (playableList[target].IsDead())
        {
            target = Random.Range(0, (playableList.Length));
        }
        bool endTurn = active.DoTurn(playableList, target);
        if (CheckIfPlayablesDead()) SettingState(BattleState.LOST);
        else if (endTurn) SettingState(BattleState.WAITING_TURN);
    }
    public void OnBasicAttackButton()
    {
        bool endTurn = active.Basic(enemyList.ToArray(), primaryTarget);
        attacksButtons.DisableButtons();
        if(CheckIfEnemiesDead()) SettingState(BattleState.WON);
        else if (endTurn) SettingState(BattleState.WAITING_TURN);
    }

    public void OnAbilityAttackButton()
    {
        bool endTurn = active.Ability(enemyList.ToArray(), primaryTarget);
        attacksButtons.DisableButtons();
        if (CheckIfEnemiesDead()) SettingState(BattleState.WON);
        if (endTurn) SettingState(BattleState.WAITING_TURN);
    }
    public void OnUltimateAttackButton(Playable caster)
    {
        caster.Definitive(enemyList.ToArray(), primaryTarget);
        CheckIfEnemiesDead();
    }

    private bool CheckIfEnemiesDead()
    {
        int deadEnemies = 0;
        for (int i = enemyList.Length-1; i >= 0; i--)
        {
            if (enemyList[i] != null && enemyList[i].IsDead()|| enemyList[i] == null)
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
            if (enemyList[i]!= null && !enemyList[i].IsDead())
            {
                primaryTarget = i;
                return;
            }
        }
    }

    private bool CheckIfPlayablesDead()
    {
        int deadPlayables = 0;
        foreach (var playable in playableList)
        {
            if (playable.IsDead())
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

    private void SettingState(BattleState state)
    {
        battleSystem.SetState(BattleState.WAITING_TURN);
        battleSystem.CheckState();
    }

    public void ExecuteInitialPassives()
    {
        playableList.ForEach(item => {
            item.AddListenersToPassiveManager();
            item.InitialPasive();
            });
    }
}
