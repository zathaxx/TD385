using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float speed = 5f;
    public Vector3 target;
    public int damage = 25;
    public bool stun;
    private float boundary = 15;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * speed * Time.smoothDeltaTime; 

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
            if(stun){
                enemy.Stun();
            }
            Destroy(transform.gameObject);
        }
    }
}
