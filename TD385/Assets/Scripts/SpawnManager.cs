using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private int spawnX;
    private float[] spawnY;
    private float cooldown;
    private float setCooldown;

    public int[] numEnemies;

    private GameObject[] enemies;

    public float setDifficultyCooldown;
    private float difficultyCooldown;
    private int difficulty;

    // Waves
    private int wave;
    private float waveCooldown;
    private float betweenWaves;
    private int waveCheckpoint;
    private int totalEnemies;

    // Waves UI
    private GameObject waveCanvas;
    private GameObject waveCooldownCanvas;
    private GameObject waveC;

    // Start is called before the first frame update
    void Start()
    {
        spawnX = 70;
        spawnY = new float[5];
        float y = 4;
        for (int i = 0; i < spawnY.Length; i++)
        {
            spawnY[i] = y;
            y -= 8;
        }
        setCooldown = 2;

        numEnemies = new int[spawnY.Length];
        for (int i = 0;i < numEnemies.Length;i++)
        {
            numEnemies[i] = 0;
        }

        enemies = Resources.LoadAll<GameObject>("Prefabs/Enemies");

        setDifficultyCooldown = 3f;
        difficultyCooldown = setDifficultyCooldown;
        difficulty = 1;

        // Waves
        wave = 1;
        betweenWaves = 10;
        waveCooldown = betweenWaves;
        waveCooldown = 10;
        totalEnemies = 0;
        waveCheckpoint = 5;

        // Waves UI
        waveCanvas = GameObject.Find("WaveCanvas");
        waveCooldownCanvas = GameObject.Find("WaveCooldownCanvas");
        waveC = GameObject.Find("WaveCooldown");
    }

    // Update is called once per frame
    void Update()
    {
        if (waveCooldown > 0)
        {
            if (totalEnemies <= 0)
            {
                waveCanvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = wave + "";
                waveC.SetActive(true);
                waveCooldown -= Time.deltaTime;
                int wc = (int)waveCooldown;
                waveCooldownCanvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = wc + "";
                
            }
        }
        else
        {
            waveC.SetActive(false);
            if (cooldown > 0)
            {
                cooldown -= Time.deltaTime;
            }
            else
            {
                // Limits enemies spawned per wave to avoid drop in perfromance
                int waveEnemies;
                if (difficulty >= 100)
                {
                    waveEnemies = 100;
                }
                else
                {
                    waveEnemies = difficulty;
                }
                for (int i = 0; i < waveEnemies; i++)
                {
                    int enemyIndex = Random.Range(0, enemies.Length);
                    GameObject enemy = Instantiate(enemies[enemyIndex]);
                    int index = Random.Range(0, spawnY.Length);
                    Vector3 pos = new Vector3(spawnX, spawnY[index], 0);
                    enemy.transform.position = pos;
                    cooldown = setCooldown;
                    EnemyPrototype ep = enemy.GetComponent<EnemyPrototype>();
                    ep.setLane(index);
                    ep.upgradeEnemy(wave);
                    lock(this)
                    {
                        numEnemies[index]++;
                        totalEnemies++;
                    }
                    
                    
                }
                if (difficulty % waveCheckpoint == 0)
                {
                    waveCooldown = betweenWaves;
                    wave++;
                    difficulty++;
                }
            }

            if (difficultyCooldown > 0)
            {
                difficultyCooldown -= Time.deltaTime;
            }
            else
            {
                difficultyCooldown = setDifficultyCooldown;
                difficulty++;
            }
        }
    }

    public bool checkLane(int lane)
    {
        return numEnemies[lane] > 0;
    }

    public void enemyDestroyed(int lane)
    {
        lock(this) {
            if (numEnemies[lane] > 0) {
                numEnemies[lane]--;
            }
            
            if (totalEnemies > 0)
            {
                totalEnemies--;
            }
        }
    }

}
