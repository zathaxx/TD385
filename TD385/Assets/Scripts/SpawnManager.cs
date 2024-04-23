using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float cooldown;
    public float delay;
    public float[] spawns;
    public int spawnIndex;
    public int enemyIndex;
    public float[] speeds;
    public GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        cooldown = 2.5f;
        spawns = new float[5];
        float f = 4f;
        for (int i = 0; i < spawns.Length; i++)
        {
            spawns[i] = f;
            f -= 2f;
        }

        delay = 2f;
        InvokeRepeating("spawnRandomEnemy", delay, cooldown);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnRandomEnemy()
    {
        spawnIndex = Random.Range(0, spawns.Length);
        enemyIndex = Random.Range(0, enemies.Length);
        Vector3 spawnPos;
        spawnPos.x = 12f;
        spawnPos.y = spawns[spawnIndex];
        spawnPos.z = 0f;
        Instantiate(enemies[enemyIndex], spawnPos,
            enemies[enemyIndex].transform.rotation);
    }
}
