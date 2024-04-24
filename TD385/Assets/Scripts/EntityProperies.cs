using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityProperies : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public bool isTall;
    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*public void TakeDamage(int damageDealt)
    {
        
    }*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Weapon" && transform.tag == "Tower")
        {
            currentHealth -= 10;
            healthBar.SetHealth(currentHealth);
            if(currentHealth <= 0)
            {
                Destroy(collision.gameObject);
                Destroy(transform.gameObject);
            }
        }
    }
}
