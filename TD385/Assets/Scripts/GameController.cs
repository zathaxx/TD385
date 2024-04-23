using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float cooldown;
    public float setCooldown;
    public float[] spawns;
    public int index;

    // Start is called before the first frame update
    void Start()
    {
        setCooldown = 5;
        cooldown = setCooldown;
        float f = 4f;
        spawns = new float[5];
        for (int i = 0; i < 5; i++)
        {
            spawns[i] = f;
            f -= 2f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }

        if (cooldown <= 0)
        {
            cooldown = setCooldown;
            index = Random.Range(0, spawns.Length);
            GameObject enemy = Instantiate(Resources.Load("Prefabs/EnemyPrototype") as GameObject);
            Vector3 pos;
            pos.x = 12f;
            pos.y = spawns[index];
            pos.z = 0f;
            enemy.transform.localPosition = pos;
        }
    }
}
