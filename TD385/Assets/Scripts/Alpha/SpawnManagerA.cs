using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpawnManagerA : MonoBehaviour
{
    private const int numLanes = 5;

    // Spawn Coordinates
    private float spawnX;
    private float[] spawnY;

    // Spawn cooldown
    private float cooldown;
    private float spawnTime;

    // Enemy prefabs
    private GameObject[] enemies;

    // Enemies in each lane
    public int[] numEnemies;

    // Start is called before the first frame update
    void Start()
    {
        spawnX = 70;

        spawnY = new float[numLanes];
        float y = 3;
        for (int i = 0; i < spawnY.Length; i++)
        {
            spawnY[i] = y;
            y -= 7.5f;
        }

        cooldown = 2;
        spawnTime = 5;

        enemies = Resources.LoadAll<GameObject>("Prefabs/Enemies");

        numEnemies = new int[numLanes];
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTime > 0)
        {
            spawnTime -= Time.deltaTime;
        }
        else
        {
            spawnTime = cooldown;
            GameObject enemy = Instantiate(enemies[Random.Range(0, enemies.Length)]);

            int lane = Random.Range(0, numLanes);
            Vector3 pos = new Vector3(spawnX, spawnY[lane], 0);
            enemy.transform.position = pos;
            EnemyA enemyObject = enemy.gameObject.GetComponent<EnemyA>();
            enemyObject.setLane(lane);
            numEnemies[lane]++;
        }
    }

    // Checks lane for enemies
    // Returns true if there are enemies in the lane
    public bool checkLane(int lane)
    {
        return numEnemies[lane] > 0;
    }

    public void enemyEliminated(int lane)
    {
        numEnemies[lane]--;
    }

}
