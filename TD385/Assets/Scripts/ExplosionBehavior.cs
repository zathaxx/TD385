using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehavior : MonoBehaviour
{
    public AudioSource audio;
    // Start is called before the first frame update
    
    void Start()
    {
        audio = GetComponent<AudioSource>();
        if (audio != null) {
            audio.Play();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy" || collision.tag == "Tower")
        {
            Destroy(collision.gameObject, 0.5f);
        }
    }
    
}
