using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    // we dont want e.g player or bullets have direct references to this object
    public static ObjectPooler current;
    
    public GameObject pooledObject;
    public int pooledAmount;
    public bool willGrow;

    public List<GameObject> pooledObjects;

    void Awake(){
        current = this;
    }

    void Start(){
        pooledObjects = new List<GameObject>();
        for (int i=0; i<pooledAmount; i++){
            GameObject obj = Instantiate(pooledObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject(){
        // make sure we get a pooled object which is not a;ready active in the scene
        for (int i = 0; i < pooledObjects.Count; i++){
            if (!pooledObjects[i].activeInHierarchy){
                return pooledObjects[i];
            }
        }

        // if we did not find any disabled objects in the pool, we get an obj and add it to the poole
        if (willGrow){
            GameObject obj = Instantiate(pooledObject);
            pooledObjects.Add(obj);
            return obj;
        }
        return null;
        
    }
}
