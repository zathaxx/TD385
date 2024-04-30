using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private float speed = 5f;
    public Vector3 target;
    private float turnSpeed = 5f;

    public int damage;

    private float boundary = 15;
    // Start is called before the first frame update
    void Start()
    {
        damage = 25;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime; 

        if (transform.position.x > boundary)
        {
            Destroy(transform.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            EnemyPrototype enemy = collision.GetComponent<EnemyPrototype>();
            enemy.takeDamage(damage);
            Destroy(transform.gameObject);
        }
    }
}
