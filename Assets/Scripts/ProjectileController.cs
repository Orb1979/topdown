using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    void OnDisable(){
        // Debug.Log("OnDisable");  
        CancelInvoke(); 
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
        // Debug.Log("disabling bullet");  
        gameObject.SetActive(false);
    }


    void OnTriggerEnter2D(Collider2D other){
        switch(other.gameObject.tag){
           
            case "wall":
                Debug.Log("hit wall");
                // dont destroy bullet we are pooling, so dont want to re-create it
                gameObject.SetActive(false);
                break;
                
            case "enemy":
                Debug.Log("projectile hits enemy");
                gameObject.SetActive(false);
                
                //Destroy(other.gameObject);
                var comp = other.gameObject.GetComponent<Health>();
                if (comp != null){
                    comp.TakeDamage(2);
                }
                break;
               
        }
    }
}
 