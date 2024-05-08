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
    private float boundary = 70;
    private float time = 0f;
    public Vector2 explosionSize;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * speed * Time.smoothDeltaTime; 
        if (transform.position.x > boundary || time > 3f)
        {
            Destroy(transform.gameObject);
        }
        time += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            EnemyPrototype enemy = collision.GetComponent<EnemyPrototype>();
            enemy.takeDamage(damage);
            GameObject explosion = Instantiate(Resources.Load("Prefabs/Explosion") as GameObject);
            explosion.transform.position = transform.position;
            explosion.transform.localScale = explosionSize;
            Destroy(explosion, 1.1f);
            if(stun){
                enemy.Stun();
            }
            Destroy(transform.gameObject);
        }
    }
}
