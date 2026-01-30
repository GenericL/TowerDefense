
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class CinemachineBattleSwitcher : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private List<CinemachineCamera> playableCams;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SwitchState(CombatUIState state)
    {
        switch (state)
        {
            case CombatUIState.PLAYABLE_1_TURN:
                animator.Play(BattleUIConstants.PLAYABLE_POSITION_1_TURN);
                break;
            case CombatUIState.PLAYABLE_2_TURN:
                animator.Play(BattleUIConstants.PLAYABLE_POSITION_2_TURN);
                break;
            case CombatUIState.PLAYABLE_3_TURN:
                animator.Play(BattleUIConstants.PLAYABLE_POSITION_3_TURN);
                break;
            case CombatUIState.PLAYABLE_4_TURN:
                animator.Play(BattleUIConstants.PLAYABLE_POSITION_4_TURN);
                break;
            case CombatUIState.WAITING_TURN:
            default:
                animator.Play(BattleUIConstants.WAITING_FOR_TURN);
                break;
        }
    }

    public void setCamerasLookAt(GameObject playable, int position)
    {
        playableCams[position].ResolveLookAt(playable.transform);
    }
}
