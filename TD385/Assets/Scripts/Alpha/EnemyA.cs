using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyA : MonoBehaviour
{
    private float speed;
    private float maxSpeed;
    private int health;

    private GameObject weapon;

    private SpawnManagerA spawnManager;
    private int lane;

    private UIA UI;
    private int value;

    // Start is called before the first frame update
    void Start()
    {
        maxSpeed = 10;
        speed = maxSpeed;
        health = 100;
        spawnManager = GameObject.FindAnyObjectByType<SpawnManagerA>();
        UI = GameObject.FindAnyObjectByType<UIA>();
        value = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (speed < maxSpeed)
        {
            speed++;
        }

        transform.position -= transform.right * speed * Time.smoothDeltaTime;
    }

    public void setLane(int lane)
    {
        this.lane = lane;
    }

    public void hit(int damage, bool stun)
    {
        health -= damage;
        if (health <= 0)
        {
            if (weapon != null)
            {
                Destroy(weapon);
            }
            spawnManager.enemyEliminated(lane);
            Destroy(transform.gameObject);
        }
        
        if (stun)
        {
            if (speed > 0)
            {
                speed = 1;
            }
        }
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
            speed = maxSpeed;
            Destroy(weapon);
        }
    }

}
