using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum BattleState
{
    WON, LOST, WAITING_TURN
}
public class BattleSystem : MonoBehaviour
{
    [SerializeField] private List<GameObject> playables = new List<GameObject>();
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();

    [SerializeField] private List<Transform> playablesPositions = new List<Transform>();
    [SerializeField] private List<Transform> enemiesPositions = new List<Transform>();

    [SerializeField] private TurnManager turnManager;
    [SerializeField] private DisplayBattleHUD displayBattleHUD;
    [SerializeField] private AttacksButtons attackButtons;

    private BattleState state;
    void Start()
    {
        state = BattleState.WAITING_TURN;
        SetupBattle();
    }

    private void SetupBattle()
    {
        var playableList = new List<Playable>();
        for (int i = 0; i < playables.Count || i < playablesPositions.Count; i++)
        {
            GameObject playableGO = Instantiate(playables[i], playablesPositions[i]);
            Playable playableUnit = playableGO.GetComponent<Playable>();
            playableList.Add(playableUnit);
        }

        var enemyList = new Enemy[5];
        for (int i = 0; i < enemies.Count || i < enemiesPositions.Count; i++)
        {
            GameObject enemyGO = Instantiate(enemies[i], enemiesPositions[i]);
            enemyList[i] = enemyGO.GetComponent<Enemy>();
        }
        turnManager.SetupManager(this, attackButtons, playableList.ToArray(),enemyList);
        displayBattleHUD.SetupHUD(turnManager.PlayableList, turnManager.OnUltimateAttackButton);
        turnManager.ExecuteInitialPassives();

        CheckState();
    }

    public void CheckState()
    {

        switch (state)
        {
            case BattleState.WAITING_TURN:
                turnManager.StartTurnCycle();
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

    private void LostBattle()
    {
        SceneManager.LoadScene(1);
    }

    private void WonBattle()
    {
        SceneManager.LoadScene(1);
    }

    public void SetState(BattleState newState)
    {
        state = newState;
    }
}
