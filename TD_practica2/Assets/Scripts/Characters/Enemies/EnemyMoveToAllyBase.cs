using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveToAllyBase : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform target;
    private void Update()
    {
        agent.SetDestination(target.position);
    }
}
