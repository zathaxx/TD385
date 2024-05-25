using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ExplosionBehavior : MonoBehaviour
{
    public AudioSource audio;
    // Start is called before the first frame update
    
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.outputAudioMixerGroup = Resources.Load<AudioMixer>("ExplosionMixer").FindMatchingGroups("Explosions")[0];


        if (audio != null) {
            audio.Play();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Tower")
        {
            Destroy(collision.gameObject, 0.5f);
            
        }
        else if (collision.tag == "Enemy")
        {
            EnemyPrototype enemy = collision.GetComponent<EnemyPrototype>();
            if (enemy != null)
            {
                enemy.takeDamage(enemy.health);
            }
        }
    }
    
}
