using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehavior : MonoBehaviour
{
    public float rotateSpeed = 90.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(transform.forward, rotateSpeed * Time.smoothDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Tower") {
            EntityProperties tower = collision.GetComponent<EntityProperties>();
            tower.currentHealth -= 10;
            tower.healthBar.SetHealth(tower.currentHealth);
        }
    }
}
