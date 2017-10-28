using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField]
    private GameObject enemyPrefab;
    
    [SerializeField]
    private float spawnCooldown;

    private float lastSpawnTime;
    private WorldObserver worldObserver;

    private void Awake()
    {
        worldObserver = WorldObserver.Instance;
    }

    void Update() {
        if (lastSpawnTime + spawnCooldown < Time.time) { 
            while (worldObserver.Enemies.Count < worldObserver.MaxEnemyCount)
            {
                Vector3 randomPlayerPosition = worldObserver.Players[Random.Range(0, worldObserver.Players.Count)]
                                                            .gameObject
                                                            .transform
                                                            .position;
                Vector3 randomPosition = new Vector3(Random.Range(randomPlayerPosition.x - 20, randomPlayerPosition.x + 20),
                                                     randomPlayerPosition.y,
                                                     Random.Range(randomPlayerPosition.z - 20, randomPlayerPosition.z + 20));
                GameObject enemy = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
                worldObserver.Enemies.Add(enemy.GetComponent<EnemyInfo>());
                lastSpawnTime = Time.time;
            }
        }
	}
}
