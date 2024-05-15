using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldFarming : MonoBehaviour
{
    public int miningRate = 20;
    private float cooldown = 1f;
    private float time = 0f;
    UIController ui;

    // Upgrade

    private GameObject upgradeInfo;
    private GameObject currentSprite;

    // Start is called before the first frame update
    void Start()
    {
        ui = FindFirstObjectByType<UIController>();

        // Upgrade
        upgradeInfo = GameObject.Find("UpgradeInfo");
        currentSprite = GameObject.Find("CurrentSprite");
        upgradeInfo.SetActive(false);
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

    // Upgrade Functions
    public void increaseMiningRate()
    {
        miningRate *= 2;
        updateUI();
    }

    private void updateUI()
    {
        GameObject current = GameObject.Find("UpgradeCanvas");
        // Update upgrade info
        current.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = miningRate + " coins/sec";
        current.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "";

    }
}
