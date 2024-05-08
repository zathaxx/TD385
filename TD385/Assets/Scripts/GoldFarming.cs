using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldFarming : MonoBehaviour
{
    private int miningRate = 20;
    private float cooldown = 1f;
    private float time = 0f;
    UIController ui;
    // Start is called before the first frame update
    void Start()
    {
        ui = FindFirstObjectByType<UIController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(time > cooldown){
            ui.AddCoins(miningRate);
            time = 0;
        }
        time += Time.deltaTime;
    }
}
