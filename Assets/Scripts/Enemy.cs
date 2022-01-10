using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform target;
    public float speed = 2f;

    void Start(){
    }
      
    void Update(){
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null){
            target = player.transform;
            MoveTowardsTarget();
            RotateTowardsTarget();
        }else{
             Debug.Log("enemy prefab can not find plater tag");
        }
    }

    private void MoveTowardsTarget(){
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
  
 private void RotateTowardsTarget(){
     var offset = -90f;
     Vector2 direction = target.position - transform.position;
     direction.Normalize();
     float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;       
     transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
 }
}
