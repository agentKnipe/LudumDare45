using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float RotationRate;
    public float SpawnRate;

    public Transform SpawnLocation;
    public GameObject[] Enemies;


    float _elapsedTime = 0f;

    // Update is called once per frame
    void Update()
    {
        if(_elapsedTime >= SpawnRate) {
            SpawnEnemy();

            _elapsedTime = 0f;
        }
        else {
            _elapsedTime += Time.deltaTime;
        }

        transform.Rotate(Vector3.forward * (RotationRate * Time.deltaTime));
    }

    void SpawnEnemy() {
        var enemy = Enemies[Random.Range(0, Enemies.Length - 1)];
        var location = new Vector3(SpawnLocation.position.x, SpawnLocation.position.y, -1);

        //var enemyPrefab = Instantiate(enemy, location, SpawnLocation.rotation);
        //enemyPrefab.transform.parent = gameObject.transform;

        var enemyPrefab = ObjectPooler.SharedInstasnce.GetPooledObject(enemy.tag);
        if(enemyPrefab != null) {
            enemyPrefab.transform.position = location;
            enemyPrefab.transform.rotation = SpawnLocation.rotation;
            enemyPrefab.SetActive(true);

            GetComponent<AudioSource>().Play();
        }
    }
}
