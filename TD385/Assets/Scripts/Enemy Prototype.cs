using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class EnemyPrototype : MonoBehaviour
{
    public int speed;
    private float cooldown;
    private int initialSpeed;
    private GameObject weapon;

    private bool stunned = false;

    private SpawnManager spawnManager;
    int lane;

    public int health = 100;
    // Start is called before the first frame update
    void Start()
    {
        initialSpeed = speed;



        spawnManager = GameObject.FindAnyObjectByType<SpawnManager>();
        cooldown = 0f;
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
            speed = 0;
            Vector3 pos = transform.position;
            weapon = Instantiate(Resources.Load("Prefabs/Weapon") as GameObject);
            weapon.transform.position = new Vector3(pos.x - 0.8f, pos.y - .1f, pos.z);
        
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
            cooldown = 0f;
        }
    }
    public void takeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            if (weapon != null)
                Destroy(weapon);
            //spawnManager.enemyDestroyed(lane);
            Destroy(transform.gameObject);
        }
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
}
