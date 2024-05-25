using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPrototype : MonoBehaviour
{
    public float speed;
    //private float cooldown;
    private float initialSpeed;
    private GameObject weapon;

    private bool stunned = false;

    private SpawnManager spawnManager;
    private int lane;

    public int health = 100;

    public int damage;
    // Awake is called before the first frame update
    void Awake()
    {
        initialSpeed = speed;



        spawnManager = GameObject.FindAnyObjectByType<SpawnManager>();
        //cooldown = 0f;

        damage = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (speed == 0 && !stunned) {
            stunned = true;
        }


        if(stunned){
            if(speed < initialSpeed)
            {
                speed += 0.8f * Time.deltaTime;
            }else stunned = false;
        }
        transform.position -= speed * Time.smoothDeltaTime * transform.right;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Tower")
        {
            EntityProperties e = collision.GetComponent<EntityProperties>();
            BoxCollider2D tower = collision.GetComponent<BoxCollider2D>();
            float towerRight = e.transform.position.x + (tower.bounds.size.x/2);
            BoxCollider2D enemy = GetComponent<BoxCollider2D>();
            float enemyLeft = transform.position.x - (enemy.bounds.size.x/2);
            if (towerRight - 0.5f < enemyLeft) {
                speed = 0;
                Vector3 pos = transform.position;
                weapon = Instantiate(Resources.Load("Prefabs/Weapon") as GameObject);
                weapon.GetComponent<WeaponBehavior>().setDamage(damage);
                weapon.transform.position = new Vector3(pos.x - 0.8f, pos.y - .1f, pos.z);
                weapon.transform.parent = transform;
            }
        }
        
        if(collision.tag == "Gold")
        {
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision){
        if(collision.tag == "Tower" && speed > 0)
        {
            EntityProperties e = collision.GetComponent<EntityProperties>();
            BoxCollider2D tower = collision.GetComponent<BoxCollider2D>();
            float towerRight = e.transform.position.x + (tower.bounds.size.x / 2);
            BoxCollider2D enemy = GetComponent<BoxCollider2D>();
            float enemyLeft = transform.position.x - (enemy.bounds.size.x / 2);

            if (towerRight - 0.5f < enemyLeft && speed > 0) 
            {
                speed = 0;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        
        if (collision.tag == "Tower")
        {
            speed = initialSpeed;
            if (weapon != null)
                Destroy(weapon);
            //cooldown = 0f;
        }
    }
    public void takeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            if (weapon != null)
                Destroy(weapon);
            
            Destroy(transform.gameObject);

                
        }
    }

    void OnDestroy() {
        spawnManager.enemyDestroyed(lane);
    }

    public void Stun()
    {
        speed = 1;
        stunned = true;

    }
    public void setLane(int lane)
    {
        this.lane = lane;
    }

    public void upgradeEnemy(int level)
    {
        health = health * (9 + level) / 10;
        damage = damage + ((level - 1) * 2);
    }
}
