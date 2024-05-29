using System.Collections;
using UnityEngine;

public class LaneDetonation : MonoBehaviour
{
 
    public bool readyForDetonation = true;
    public Sprite Armed;
    public Sprite Detonated;

    private AudioSource audio;

    public void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public IEnumerator Detonation()
    {
        float i = transform.position.x;
        while(i < 50)
        {
            GameObject explosion = Instantiate(Resources.Load("Prefabs/LaneExplosion") as GameObject);
            Vector2 pos = transform.position;
            pos.x = i;
            explosion.transform.position = pos;
            Destroy(explosion, 1.1f);
            yield return new WaitForSeconds(0.1f);
            i += 8;
        }

        yield return null;
    }
    public void ReArm()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = Armed;
        readyForDetonation = true;
        if (audio != null)
        {
            audio.Play();
        }
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
