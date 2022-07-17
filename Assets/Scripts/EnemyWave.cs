using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class EnemyData
{
    public bool spawnRightSide;
    public GameObject enemy;
}
public class EnemyWave : MonoBehaviour
{
    public List<Enemy> startingEnemies;
    public List<EnemyData> enemiesToSpawn;
    public List<Enemy> activeEnemies;
    public int maxActiveEnemies;
    public int totalEnemies;
    public int enemiesAlreadySpawned;
    public bool waveActive;
    public bool waveFinished = true;
    public float timeBetweenSpawns;
    public GameObject fightBoundary;

    private float spawnTimer;

    private void Start()
    {
        foreach (Enemy enemy in startingEnemies)
        {
            enemy.onDestroy += (enemy) => DestroyEnemy(enemy);
            activeEnemies.Add(enemy);
            totalEnemies++;
            enemiesAlreadySpawned++;
        }
        startingEnemies.Clear();
    }
    public void StartWave()
    {
        EnemyManager.instance.waveStarted = true;
        fightBoundary.SetActive(false);
        totalEnemies += enemiesToSpawn.Count;
        foreach(Enemy enemy in activeEnemies)
        {
            EnemyManager.instance.AddEnemyToQueue(enemy);
        }
        SpawnEnemy();
        waveActive = true;
    }

    private void DestroyEnemy(Enemy enemy)
    {
        activeEnemies.Remove(enemy);
    }

    private void SpawnEnemy()
    {
        if (waveActive && activeEnemies.Count < maxActiveEnemies)
        {
            spawnTimer = 0;
            int lastEnemyIndex = enemiesToSpawn.Count - 1;

            float enemyPosX = enemiesToSpawn[lastEnemyIndex].spawnRightSide ? PlayerCamera.cameraMin.x : PlayerCamera.cameraMax.x;
            float enemyPosY = PlayerCamera.cameraMax.y / PlayerCamera.cameraMin.y;

            GameObject enemyGameObject = Instantiate(enemiesToSpawn[lastEnemyIndex].enemy, new Vector3(enemyPosX, enemyPosY, 0), Quaternion.identity, transform);
            Enemy enemy = enemyGameObject.GetComponent<Enemy>();
            if (enemy)
            {
                enemy.onDestroy += (enemy) => DestroyEnemy(enemy);
                activeEnemies.Add(enemy);
                enemiesAlreadySpawned++;
                enemy.activated = true;
                EnemyManager.instance.AddEnemyToQueue(enemy);
            }
            enemiesToSpawn.RemoveAt(lastEnemyIndex);
        }
    }

    private void Update()
    {
        if (waveActive)
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer > timeBetweenSpawns && enemiesToSpawn.Count != 0)
            {
                SpawnEnemy();
            }

        }

        if(activeEnemies.Count == 0 && waveActive && enemiesAlreadySpawned >= totalEnemies)
        {
            waveFinished = true;
            EnemyManager.instance.waveStarted = false;
            PlayerCamera.cameraLocked = false;
            Destroy(gameObject);
        }
    }
}
