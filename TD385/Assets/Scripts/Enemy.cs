using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed;
    private int despawnX;

    // Start is called before the first frame update
    void Start()
    {
        speed = 5;
        despawnX = -12;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > despawnX)
        {
            Vector3 pos = transform.position;
            pos.x -= speed * Time.deltaTime;
            transform.position = pos;
        }
        else
        {
            Destroy(transform.gameObject);
        }
    }

    public void setSpeed(float speed)
    {
        this.speed = speed;
    }
}
