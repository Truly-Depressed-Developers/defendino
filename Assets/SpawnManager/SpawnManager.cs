using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    GameObject _enemyObject;
    [SerializeField]
    float _spawnRange = 2.0f;
    [SerializeField]
    float _spawnSpeed = 5f;

    float time = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(time >= _spawnSpeed)
        {
            SpawnEnemy();
            time = 0;
        } else
        {
            time += Time.deltaTime;
        }
    }

    void SpawnEnemy()
    {
        Camera cam = Camera.main;
        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;

        Vector3 spawnPos = randomPos(new Vector2 (halfWidth, halfHeight));

        Instantiate(_enemyObject, spawnPos, Quaternion.identity, transform);
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

}
