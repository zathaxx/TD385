using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityProperties : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public bool isTall;
    public HealthBar healthBar;
    public Vector3 offset;
    private float cooldown = 1f;
    public float setCooldown;
    public float healthBarTimer;
    public string ammunition;
    public bool Explodes;

    private SpawnManager spawnManager;
    private int lane;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 100;
        healthBar = transform.GetChild(0).GetComponent<HealthBar>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.hide();
        healthBarTimer = 0f;
        spawnManager = GameObject.FindAnyObjectByType<SpawnManager>();
        ammunition = "cannon_ball";
        setCooldown = 3f;
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
        if (cooldown <= 0)
        {
            cooldown = setCooldown;
            GameObject bullet = Instantiate(Resources.Load("Prefabs/"+ ammunition) as GameObject);
            bullet.transform.position = transform.position + offset;
        }
    }
    private void Explode()
    {
        GameObject bullet = Instantiate(Resources.Load("Prefabs/"+ ammunition) as GameObject);
        bullet.transform.position = transform.position + offset;
    }

    /*public void TakeDamage(int damageDealt)
    {
        
    }*/
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Weapon" && transform.tag == "Tower")
        {
            if (healthBar.isHidden())
            {
                healthBar.show();
            }
            healthBarTimer = 3;
            if(currentHealth <= 0)
            {
                Destroy(collision.gameObject);
                Destroy(transform.gameObject);
            }
        }
    }

    public void setLane(int lane)
    {
        this.lane = lane;
    }

  
}
