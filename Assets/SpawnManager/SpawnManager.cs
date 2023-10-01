using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DamageSystem;
using Entities.Player;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    [SerializeField] private GameObject _enemyObject;
    [SerializeField] private float _spawnSpeed = 5f;

    private int enemiesToSpawn = 0;
    private int enemyCounter = 0;
    private bool canSpawnEnemies = false;

    private float time = 5f;

    public event Action onWaveEnd;

    public static SpawnManager instance;

    private void Awake() {
        if(instance == null) {
            instance = this;
        }
    }

    // Update is called once per frame
    private void Update() {
         if (time >= _spawnSpeed && enemiesToSpawn > 0 && canSpawnEnemies == true) {
            SpawnEnemy();
            time = 0;

            if(enemiesToSpawn == 0) {
                canSpawnEnemies = false;
            }
        } else {
            time += Time.deltaTime;
        }
    }

    private void SpawnEnemy() {
        Debug.Log($"Spawning enemy, EC = {enemyCounter}, ETS = {enemiesToSpawn}");

        Camera cam = Camera.main;
        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;

        Vector3 spawnPos = randomPos(new Vector2(halfWidth + (_enemyObject.transform.localScale.x / 2), halfHeight + (_enemyObject.transform.localScale.y / 2)));

        GameObject enemy = Instantiate(_enemyObject, spawnPos, Quaternion.identity, transform);
        DamageReceiver enemyDamageReceiver = enemy.GetComponent<DamageReceiver>();

        enemyDamageReceiver.OnDeath.AddListener((exp) => {
            OnEnemyDeath(exp);
        });

        enemyCounter += 1;
        enemiesToSpawn -= 1;
    }

    Vector3 randomPos(Vector2 examplePos) {
        float randomAngle = UnityEngine.Random.Range(0, 2 * Mathf.PI);

        Vector3 spawnPos = new Vector3(
            (Mathf.Cos(randomAngle) * examplePos.x) - (Mathf.Sin(randomAngle) * examplePos.y),
            (Mathf.Sin(randomAngle) * examplePos.x) + (Mathf.Cos(randomAngle) * examplePos.y),
            0
        );

        return spawnPos;
    }

    public void BeginWave(int waveNumber) {
        Debug.Log($"Starting wave {waveNumber}");
        enemiesToSpawn = (int)(10 * Mathf.Pow(2, waveNumber - 1));
        Debug.Log($"Spawning {enemiesToSpawn} enemies");
        canSpawnEnemies = true;
    }

    private void OnEnemyDeath(float exp) {
        enemyCounter -= 1;
        Debug.Log($"Enemy died, {enemyCounter} enemies left");

        ProgressManager.instance.addExp(exp);

        if (enemyCounter == 0) {
            onWaveEnd();
        }
    }
}
