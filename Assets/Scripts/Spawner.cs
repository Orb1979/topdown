using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float timeToSpawn;
    private float currentTimeToSpawn;
    private float randomSide;
    
    [SerializeField]
	private Transform tl, tr, bl, br;

    void Start(){
       StartCoroutine(SpawnRandomInterval());
    }

    void Update(){
        SpawnFixedInterval();
    }

    void SpawnFixedInterval(){
        // fixed counter, we subtract passed delta time
        if(currentTimeToSpawn > 0){
            currentTimeToSpawn -= Time.deltaTime;
        } else {
            spawnOnLocation();
            currentTimeToSpawn = timeToSpawn;
        }
    }

    IEnumerator SpawnRandomInterval(){
        // random spwam between 1f-2f
        while (true){
            yield return new WaitForSeconds(Random.Range(0.5f,1f));
            spawnOnLocation();
        }
    }

     void spawnOnLocation(){
        randomSide = Random.Range(0,4);
        Vector2 spawnPosition = transform.position;
        switch (randomSide){
            case 0: spawnPosition = tl.position; 
                break;
            case 1: spawnPosition = tr.position; 
                break;
            case 2: spawnPosition = bl.position; 
                break;
            case 3: spawnPosition = br.position; 
                break;
        }

       Instantiate(objectToSpawn, spawnPosition, transform.rotation);  
    }
}
  