using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityProperies : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public bool isTall;
    public HealthBar healthBar;

    public float cooldown;
    public float setCooldown;

    public float healthBarTimer;

    public SpawnManager spawnManager;
    public int lane;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.hide();
        setCooldown = 1f;
        healthBarTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown >= 0)
        {
            cooldown -= Time.deltaTime;
        }
        else
        {
            if (spawnManager.checkLane(lane))
            {
                shoot();
            }
        }

        if (healthBarTimer > 0)
        {
            healthBarTimer -= Time.deltaTime;
        }
        else
        {
            healthBar.hide();
        }

    }

    private void shoot()
    {
        cooldown = setCooldown;
        GameObject bullet = Instantiate(Resources.Load("Prefabs/Bullet") as GameObject);
        bullet.transform.position = transform.localPosition;

    }

    /*public void TakeDamage(int damageDealt)
    {
        
    }*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Weapon" && transform.tag == "Tower")
        {
            if (healthBar.isHidden())
            {
                healthBar.show();
            }
            healthBarTimer = 3;
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
