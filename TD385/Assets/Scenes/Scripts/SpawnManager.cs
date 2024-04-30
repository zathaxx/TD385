using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private int spawnX;
    private int[] spawnY;
    private float cooldown;
    private float setCooldown;

    // Start is called before the first frame update
    void Start()
    {
        spawnX = 15;
        spawnY = new int[5];
        int y = -4;
        for (int i = 0; i < spawnY.Length; i++)
        {
            spawnY[i] = y;
            y += 2;
        }
        cooldown = 0;
        setCooldown = 2;
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
            GameObject enemy = Instantiate(Resources.Load("Prefabs/Enemy") as GameObject);
            int index = Random.Range(0, spawnY.Length);
            Vector3 pos;
            pos.x = spawnX;
            pos.y = spawnY[index];
            pos.z = 0;
            enemy.transform.position = pos;
            cooldown = setCooldown;
        }
    }
}
