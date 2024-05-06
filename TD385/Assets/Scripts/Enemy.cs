using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed;
    private int despawnX;
    public float cooldown;
    private float initialSpeed;
    private GameObject weapon;

    private bool stunned = false;

    private SpawnManager spawnManager;
    private int lane;
    private int coinValue;

    public int health = 100;
    // Start is called before the first frame update
    void Start()
    {
        speed = 10;
        initialSpeed = speed;


        health = 100;

        spawnManager = GameObject.FindAnyObjectByType<SpawnManager>();

        coinValue = 100;
    }

    // Update is called once per frame
    void Update()
    {
         if(stunned){
             if(speed < initialSpeed)
             {
                 speed += 1;
             }else stunned = false;
         }
        transform.position -= speed * Time.smoothDeltaTime * transform.right;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Tower")
        {
            speed = 0;
            Vector3 pos = transform.position;
            weapon = Instantiate(Resources.Load("Prefabs/Weapon") as GameObject);
            weapon.transform.position = new Vector3(pos.x - 0.75f, pos.y - .1f, pos.z);

        }
        if (collision.tag == "Gold")
        {
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == "Tower")
        {
            speed = initialSpeed;
            Destroy(weapon);
        }
    }
    public void takeDamage(int damage)
    {
        health -= damage;
        Stun();
        if (health <= 0)
        {
            if (weapon != null)
                Destroy(weapon);
            //spawnManager.enemyDestroyed(lane);
            UIController UI = gameObject.GetComponent<UIController>();
            UI.increaseCoins(coinValue);
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

    public void setStats(string name)
    {


    }
}