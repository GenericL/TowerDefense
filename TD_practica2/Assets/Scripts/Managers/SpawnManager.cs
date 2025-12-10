using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject endPoint;
    private float spawnInterval = 10f;
    private float spawncd = 0;

    private void Awake()
    {
        enemyPrefab.GetComponent<EnemyMoveToAllyBase>().setDestination(endPoint.transform);
    }
    private void Update()
    {
        if (spawncd < 0)
        {
            Instantiate(enemyPrefab);
            spawncd = spawnInterval;
        }
        else
        {
            spawncd -= Time.deltaTime;
        }
    }
}
