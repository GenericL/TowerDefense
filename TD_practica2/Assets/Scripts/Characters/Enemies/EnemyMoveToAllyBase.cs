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

    public void setDestination(Transform target)
    {
        this.target = target;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Base")
        {
            Destroy(gameObject);
        }
    }
}
