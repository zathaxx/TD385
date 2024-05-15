using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class LaneDetonation : MonoBehaviour
{
    public bool readyForDetonation = true;
    private float time = 0;
    private float delay = 3f;
    public Sprite Armed;
    public Sprite Detonated;
    // Start is called before the first frame update
    
    public async void Detonation()
    {
        float i = transform.position.x;
        while(i < 50)
        {
            GameObject explosion = Instantiate(Resources.Load("Prefabs/LaneExplosion") as GameObject);
            Vector2 pos = transform.position;
            pos.x = i;
            explosion.transform.position = pos;
            Destroy(explosion, 1.1f);
            await Task.Delay(100);
            i += 8;
        }
    }
    public void ReArm()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = Armed;
        readyForDetonation = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy" && readyForDetonation){
            StartCoroutine(nameof(Detonation));
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            sprite.sprite = Detonated;
            readyForDetonation = false;
        }
    }
}   
