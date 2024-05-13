using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPrototype : MonoBehaviour
{
    public int speed;
    //private float cooldown;
    private int initialSpeed;
    private GameObject weapon;

    //private bool stunned = false;

    private SpawnManager spawnManager;
    private int lane;

    public int health = 100;

    private bool dead;
    // Start is called before the first frame update
    void Start()
    {
        initialSpeed = speed;



        spawnManager = GameObject.FindAnyObjectByType<SpawnManager>();
        //cooldown = 0f;

        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        // if(stunned){
        //     if(speed < initialSpeed)
        //     {
        //         speed += 1;
        //     }else stunned = false;
        // }
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
                weapon.transform.position = new Vector3(pos.x - 0.8f, pos.y - .1f, pos.z);
            }
        }
        
        if(collision.tag == "Gold")
        {
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision){
        
        if (collision.tag == "Tower")
        {
            speed = initialSpeed;
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
            if (!dead)
                spawnManager.enemyDestroyed(lane);
            dead = true;
            Destroy(transform.gameObject);
        }
    }

    public void Stun()
    {
        speed = 1;
        //stunned = true;

    }
    public void setLane(int lane)
    {
        this.lane = lane;
    }
}
