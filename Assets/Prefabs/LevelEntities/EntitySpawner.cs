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
        if(canSpawn) InvokeRepeating("SpawnEnemy", 0, spawnRate);
        SetRadius(detectRadius);
        
    }
    private void SpawnEnemy()
    {
        if (!canSpawn) return;
        if(spawnedEnemies.Count >= maxOutAtOnce) return;

        GameObject nextSpawn = enemiesToSpawn[Random.Range(0, enemiesToSpawn.Count)];
        if (!nextSpawn.TryGetComponent<CharacterBehavior>(out CharacterBehavior temp))
        {
            Debug.LogWarning($"Tried to spawn entity ({temp.name}) that does not have a CharacterBehavior!");
            return; // is it a derivative of EntityBehavior?
        }
            


        GameObject lastSpawned = Instantiate(nextSpawn, transform.position, transform.rotation);
        lastSpawned.GetComponent<CharacterBehavior>().spawnOwner = this;
        spawnedEnemies.Add(lastSpawned);
        spawnedTotal++;

        if (infSpawn) return;
        if(spawnedTotal >= spawnedMax || !canSpawn)
        {
            CancelInvoke("SpawnEnemy");
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (spawnedTotal >= spawnedMax && !infSpawn) return;
        if (collision.transform.root.CompareTag("Player") && isTrap && !canSpawn)
        {
            canSpawn = true;
            InvokeRepeating("SpawnEnemy", 0, spawnRate);
        }
    }

    public void DespawnEnemy(GameObject enemy)
    {
        spawnedEnemies.Remove(enemy);
        Destroy(enemy);
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
