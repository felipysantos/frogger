using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyBehaviourManagerScript : MonoBehaviour
{
    public GameObject enemy_prefab;
    public float spawn_point;
    public int enemy_direction;
    public int init;
    void Start()
    {
        StartCoroutine(SpawnEnemyWithRandomInterval());
    }

    IEnumerator SpawnEnemyWithRandomInterval()
    {
        while (true)
        {
            SpawnEnemy();
            int ranegInterval = Random.Range(2, 4);
            yield return new WaitForSeconds(ranegInterval);
        }
    }

    void SpawnEnemy()
    {
        float screenRightEdge = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        if (init == 0)
        {
            GameObject enemy = Instantiate(enemy_prefab, new Vector3(screenRightEdge, spawn_point, 0), Quaternion.identity);
            enemy.GetComponent<EnemyBehaviourScript>().SetEnemyDirection(enemy_direction);

        }
        if (init == 1)
        {
            GameObject enemy = Instantiate(enemy_prefab, new Vector3(-screenRightEdge, spawn_point, 0), Quaternion.identity);
            enemy.GetComponent<EnemyBehaviourScript>().SetEnemyDirection(enemy_direction);

        }

    }

}