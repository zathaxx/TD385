using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPileBehaviour : MonoBehaviour
{
    public Sprite goldSprite;
    private Bounds pileBounds;
    public int goldAmount; 
    // Start is called before the first frame update
    void Start()
    {
        pileBounds = GetComponent<BoxCollider2D>().bounds;
        for (int i = 0; i < goldAmount; i++) {
            GameObject e = new GameObject();
            e.AddComponent<SpriteRenderer>().sprite = goldSprite;
            e.transform.parent = transform;
            float randomX = Random.Range(pileBounds.min.x, pileBounds.max.x);
            float randomY = Random.Range(pileBounds.min.y, pileBounds.max.y);
            e.transform.position = new Vector3(randomX, randomY, 0f);
            e.name = "Gold Bar " + i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy" && transform.childCount > 0) {
            int index = Random.Range(0, transform.childCount);
            Destroy(transform.GetChild(index).gameObject);
            EnemyPrototype enemy = other.GetComponent<EnemyPrototype>();
            if (enemy != null)
            {
                enemy.takeDamage(enemy.health);
            }
        }
    }
}
