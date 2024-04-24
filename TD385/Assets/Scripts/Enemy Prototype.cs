using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPrototype : MonoBehaviour
{
    public int speed;
    public float cooldown;
    private int initialSpeed;
    // Start is called before the first frame update
    void Start()
    {
        initialSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= speed * Time.smoothDeltaTime * transform.right;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Tower")
        {
            speed = 0;
            Vector3 pos = transform.position;
            GameObject weapon = Instantiate(Resources.Load("Prefabs/Weapon") as GameObject);
            weapon.transform.position = new Vector3(pos.x, pos.y + 0.5f, pos.z);  
        }
        if(collision.tag == "Gold")
        {
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision){
        speed = initialSpeed;
    }
}
