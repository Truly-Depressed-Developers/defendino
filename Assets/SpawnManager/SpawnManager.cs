using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    GameObject _enemyObject;
    [SerializeField]
    float _spawnSpeed = 5f;

    int enemyToSpawn = 0;
    List<GameObject> enemyList;
    bool waveEnd = false;

    float time = 5f;

    // Start is called before the first frame update
    void Start()
    {
        enemyList = new List<GameObject>();
        BeginWave(1);
    }

    // Update is called once per frame
    void Update()
    {
        if(time >= _spawnSpeed && enemyToSpawn > 0)
        {
            enemyList.Add(SpawnEnemy());
            time = 0;
        } else
        {
            time += Time.deltaTime;
        }

        if(enemyToSpawn == 0 && enemyList.Count == 0 && waveEnd == false) {
            waveEnd = true;
            onWaveEnd();
        }
    }

    GameObject SpawnEnemy()
    {
        Camera cam = Camera.main;
        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;

        Vector3 spawnPos = randomPos(new Vector2 (halfWidth + (_enemyObject.transform.localScale.x/2), halfHeight + (_enemyObject.transform.localScale.y / 2)));
        enemyToSpawn -= 1;

        return Instantiate(_enemyObject, spawnPos, Quaternion.identity, transform);
    }

    Vector3 randomPos(Vector2 examplePos)
    {
        float randomAngle = Random.Range(0, 2 * Mathf.PI);

        Vector3 spawnPos = new Vector3(
            Mathf.Cos(randomAngle) * examplePos.x - Mathf.Sin(randomAngle) * examplePos.y,
            Mathf.Sin(randomAngle) * examplePos.x + Mathf.Cos(randomAngle) * examplePos.y,
            0
        );

        return spawnPos;
    }

    void BeginWave(int waveNumber) 
    {
        enemyToSpawn = (int)(10 * Mathf.Pow(2, waveNumber - 1)); 
        waveEnd = false;
    }

    void onWaveEnd() {
        Debug.Log("Wave end");
    }

}
