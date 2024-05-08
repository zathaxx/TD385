using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehavior : MonoBehaviour
{
    public float rotateSpeed = 90.0f;

    //private float cooldown;
    // Start is called before the first frame update
    void Start()
    {
        //cooldown = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(transform.forward, rotateSpeed * Time.smoothDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Tower") {
            // if (cooldown <= 0f) {
                EntityProperties tower = collision.GetComponent<EntityProperties>();
                tower.currentHealth -= 10;
                //cooldown = 1f;
                tower.healthBar.SetHealth(tower.currentHealth);
            // } else {
            //     cooldown -= Time.deltaTime;
            // }
        }
    }
}
