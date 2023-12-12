using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    
    public List<GameObject> enemiesToSpawn;
    public List<GameObject> spawnedEnemies = new List<GameObject>();
    public int spawnedTotal = 0;
    public float spawnRate;
    public int spawnedMax;
    public int maxOutAtOnce;
    public bool canSpawn;
    public bool infSpawn;
    public bool isTrap;
    public float detectRadius = 1;

    

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 0, spawnRate);
        SetRadius(detectRadius);
        
    }
    private void SpawnEnemy()
    {
        if (!canSpawn) return;
        if(spawnedEnemies.Count >= maxOutAtOnce) return;

        GameObject nextSpawn = enemiesToSpawn[Random.Range(0, enemiesToSpawn.Count)];
        //if (!nextSpawn.GetType().IsSubclassOf(typeof(EntityBehavior))) return; // is it a derivative of EntityBehavior?

        nextSpawn.GetComponent<EntityBehavior>().spawnOwner = this;
        spawnedEnemies.Add(Instantiate(nextSpawn, transform.position, transform.rotation));
        spawnedTotal++;

        if((spawnedTotal >= spawnedMax & !infSpawn) || !canSpawn)
        {
            CancelInvoke("SpawnEnemy");
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (spawnedTotal >= spawnedMax) return;
        if (collision.transform.root.CompareTag("Player") & isTrap)
        {
            canSpawn = true;
            InvokeRepeating("SpawnEnemy", 0, spawnRate);
        }
    }

    public void DespawnEnemy(GameObject enemy)
    {
        spawnedEnemies.Remove(enemy);
    }
    public void DespawnEnemy(List<GameObject> gameObjects)
    {
        foreach (GameObject o in gameObjects)
        {
            spawnedEnemies.Remove(o);
        }
    }
    public void ResetSpawner()
    {
        spawnedTotal = 0;
        canSpawn = false;
    }
    public void SetRadius(float radius)
    {
        GetComponent<CircleCollider2D>().radius = radius;
    }
}
