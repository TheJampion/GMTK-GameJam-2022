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
    public int enemySpawnCounter;
    public bool waveActive;
    public bool waveFinished = true;
    public float timeBetweenSpawns;
    public GameObject fightBoundary;

    private float spawnTimer;

    public void StartWave()
    {
        waveActive = true;
        fightBoundary.SetActive(false);
        foreach(Enemy enemy in startingEnemies)
        {
            enemy.onDestroy += (enemy) => DestroyEnemy(enemy);
            enemy.activated = true;
            activeEnemies.Add(enemy);
            totalEnemies++;
            enemySpawnCounter++;
        }
        totalEnemies += enemiesToSpawn.Count;
    }

    private void DestroyEnemy(Enemy enemy)
    {
        activeEnemies.Remove(enemy);
    }

    private void SpawnEnemy()
    {
        spawnTimer = 0;
        int lastEnemyIndex = enemiesToSpawn.Count - 1;

        if (waveActive && activeEnemies.Count < maxActiveEnemies)
        {
            float enemyPosX = enemiesToSpawn[lastEnemyIndex].spawnRightSide ? PlayerCamera.cameraMin.x : PlayerCamera.cameraMax.x;
            float enemyPosY = PlayerCamera.cameraMax.y / PlayerCamera.cameraMin.y;

            GameObject enemyGameObject = Instantiate(enemiesToSpawn[lastEnemyIndex].enemy, new Vector3(enemyPosX, enemyPosY, 0), Quaternion.identity, transform);
            Enemy enemy = enemyGameObject.GetComponent<Enemy>();
            if (enemy)
            {
                enemy.onDestroy += (enemy) => DestroyEnemy(enemy);
                activeEnemies.Add(enemy);
                enemySpawnCounter++;
                enemy.activated = true;
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

        if(activeEnemies.Count == 0 && waveActive && enemySpawnCounter >= totalEnemies)
        {
            waveFinished = true;
            PlayerCamera.cameraLocked = false;
            Destroy(gameObject);
        }
    }
}
