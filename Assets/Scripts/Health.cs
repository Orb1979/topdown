using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;

    void Start(){
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount){
        currentHealth -= amount;
        if (currentHealth <= 0) {
            Debug.Log("we dead");
            Destroy(gameObject);
        }
    }

    public void Heal(int amount){
        currentHealth += amount;
        if (currentHealth > maxHealth) { 
            currentHealth = maxHealth; 
        }
    }


}
