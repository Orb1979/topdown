using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform tf;
    public float fireForce;

    void OnEnable(){
        if (rb != null){
           MoveProjectile();
        }
        Invoke("Disable", 2f);
    }

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
        MoveProjectile();
    }

    void MoveProjectile(){
         rb.AddForce(tf.up * fireForce, ForceMode2D.Impulse);
    }

    void Disable(){
        Debug.Log("disabling bullet");  
        gameObject.SetActive(false);
    }

    void OnDisable(){
        Debug.Log("OnDisable");  
        CancelInvoke(); 
    }

    void OnTriggerEnter2D(Collider2D other){
        switch(other.gameObject.tag){
           
            case "wall":
                Debug.Log("hit wall");
                // dont destroy bullet we are pooling, thus not creating it
                // Destroy(gameObject); 
                gameObject.SetActive(false);
                break;
                
            case "enemy":
                Debug.Log("hit enemy");
                gameObject.SetActive(false);
                Destroy(other.gameObject);
                break;
                //other.gameObject.GetComponent<MyEnemyScript>.TakaDamage
        }
    }
}
 