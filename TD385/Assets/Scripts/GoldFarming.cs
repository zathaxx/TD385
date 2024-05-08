using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldFarming : MonoBehaviour
{
    private int miningRate;
    private float cooldown = 10f;
    private float time = 0f;
    UIController ui;
    // Start is called before the first frame update
    void Start()
    {
        ui = FindFirstObjectByType<UIController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(time > cooldown){
            ui.AddCoins(miningRate);
        }
        time =+ Time.deltaTime;
    }
}
