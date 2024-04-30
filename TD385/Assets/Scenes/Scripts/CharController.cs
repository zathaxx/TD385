using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    private GameObject ally;
    private GameObject enemy;
    private float lastBullet;
    // Start is called before the first frame update
    void Start()
    {
        ally = GameObject.Find("Ally");
        enemy= GameObject.Find("Enemy");
        lastBullet = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastBullet >= 0.4f) {
            GameObject b1 = Instantiate(Resources.Load("Prefabs/Bullet") as GameObject);
            BulletBehaviour be = b1.AddComponent<BulletBehaviour>();
            b1.transform.position = ally.transform.position;
            be.target = enemy.transform.position;
            lastBullet = Time.time;
        }

    }
}
