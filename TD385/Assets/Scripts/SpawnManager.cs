using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private int spawnX;
    private int[] spawnY;
    private float cooldown;
    private float setCooldown;
    public GameObject[] enemies;

    public int[] numEnemies;

    // Start is called before the first frame update
    void Start()
    {
        spawnX = 15;
        spawnY = new int[5];
        int y = 4;
        for (int i = 0; i < spawnY.Length; i++)
        {
            spawnY[i] = y;
            y -= 2;
        }
        cooldown = 3;
        setCooldown = 2;

        numEnemies = new int[spawnY.Length];
        for (int i = 0;i < numEnemies.Length;i++)
        {
            numEnemies[i] = 0;
        }

        enemies = Resources.LoadAll<GameObject>("Prefabs/Enemies");
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        else
        {
            //GameObject enemy = Instantiate(Resources.Load("Prefabs/Enemy") as GameObject);
            int index = UnityEngine.Random.Range(0, spawnY.Length);
            int enemyIndex = UnityEngine.Random.Range(0, enemies.Length);
            GameObject enemy = Instantiate(enemies[enemyIndex]);
            Vector3 pos;
            pos.x = spawnX;
            pos.y = spawnY[index];
            pos.z = 0;
            enemy.transform.position = pos;
            cooldown = setCooldown;
            numEnemies[index]++;

            EnemyPrototype ep = enemy.GetComponent<EnemyPrototype>();
            ep.setLane(index);
        }
    }

    public bool checkLane(int lane)
    {
        return numEnemies[lane] > 0;
    }

    public void enemyDestroyed(int lane)
    {
        numEnemies[lane]--;
    }
}
