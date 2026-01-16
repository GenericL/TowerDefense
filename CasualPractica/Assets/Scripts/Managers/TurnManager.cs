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
public enum TurnState
{
    NONE, PLAYER_TURN, ENEMY_TURN
}
public class TurnManager : MonoBehaviour 
{
    [SerializeField] private BattleSystem battleSystem;

    private Playable[] playableList;
    public Playable[] PlayableList { get { return playableList; } }
    private Enemy[] enemyList;
    public Enemy[] EnemyList { get { return enemyList; } }

    [SerializeField] private AttacksButtons attacksButtons;

    private Character active;

    private int primaryTarget;
    private bool battleEnded = false;

    public void Awake()
    {
        playableList = new Playable[4];
        enemyList = new Enemy[5];
        attacksButtons.Initialize(OnBasicAttackButton, OnAbilityAttackButton);
        attacksButtons.DisableButtons();
    }
    public void SetupTurnManager(Playable[] playables, Enemy[] enemies)
    {
        playableList = playables;
        enemyList = enemies;
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
        CheckIfPlayablesDead();
        if (endTurn) SettingState(BattleState.WAITING_TURN);
    }
    public void OnBasicAttackButton()
    {
        bool endTurn = active.Basic(enemyList.ToArray(), primaryTarget);
        attacksButtons.DisableButtons();
        CheckIfEnemiesDead();
        if (endTurn) StartTurnCycle();
    }

    public void OnAbilityAttackButton()
    {
        bool endTurn = active.Ability(enemyList.ToArray(), primaryTarget);
        attacksButtons.DisableButtons();
        CheckIfEnemiesDead();
        if (endTurn) SettingState(BattleState.WAITING_TURN);
    }
    public void OnUltimateAttackButton(Playable caster)
    {
        caster.Definitive(enemyList.ToArray(), primaryTarget);
        CheckIfEnemiesDead();
    }

    private void CheckIfEnemiesDead()
    {
        int deadEnemies = 0;
        foreach (var enemy in enemyList)
        {
            if (enemy != null && enemy.IsDead())
            {
                deadEnemies++;
            }
        }
        if (deadEnemies == enemyList.Length)
        { 
            SettingState(BattleState.WON);
            battleEnded = true;
        }
    }

    private void CheckIfPlayablesDead()
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
            SettingState(BattleState.LOST);
            battleEnded = true;
        }
    }

    private void SettingState(BattleState state)
    {
        battleSystem.SetState(BattleState.WAITING_TURN);
        battleSystem.CheckState();
    }

    public void ExecuteInitialPassives()
    {
        playableList.ForEach(item => item.InitialPasive());
    }
}
