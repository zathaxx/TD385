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
    public int lane;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = transform.GetChild(0).GetComponent<HealthBar>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.hide();
        healthBarTimer = 0f;
        spawnManager = GameObject.FindAnyObjectByType<SpawnManager>();
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
             if (spawnManager.checkLane(lane) && ammunition != "")
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
            // GameObject muzzleFlash = Instantiate(Resources.Load("Prefabs/ef_3") as GameObject);
            // muzzleFlash.transform.position = transform.position + offset;
            // Destroy(muzzleFlash, 0.3f);
            GameObject bullet = Instantiate(Resources.Load("Prefabs/"+ ammunition) as GameObject);
            bullet.transform.position = transform.position + offset;
    }
    private void Explode()
    {
        GameObject explosion = Instantiate(Resources.Load("Prefabs/Explosion") as GameObject);
        explosion.transform.position = transform.position;
        Destroy(explosion, 1.1f);
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
                if(Explodes) Explode();
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision){
        if(collision.tag == "Enemy" && Explodes){
            Destroy(collision.gameObject);
        }
    }

    public void setLane(int lane)
    {
        this.lane = lane;
    }

  
}
