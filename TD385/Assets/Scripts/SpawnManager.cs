using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private int spawnX;
    private float[] spawnY;
    private float cooldown;
    private float setCooldown;

    public int[] numEnemies;

    GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        spawnX = 70;
        spawnY = new float[5];
        float y = 3;
        for (int i = 0; i < spawnY.Length; i++)
        {
            spawnY[i] = y;
            y -= 7.5f;
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
            int enemyIndex = Random.Range(0, enemies.Length);
            GameObject enemy = Instantiate(enemies[enemyIndex]);
            int index = Random.Range(0, spawnY.Length);
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
