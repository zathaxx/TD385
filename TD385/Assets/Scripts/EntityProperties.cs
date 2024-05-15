using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EntityProperties : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public bool isTall;
    public HealthBar healthBar;
    public Vector3 offset;
    private float cooldown = 1f;
    public float setCooldown;
    public float healthBarTimer;
    public string ammunition;
    public bool Explodes;
    public int damage;

    private SpawnManager spawnManager;
    public int lane;
    private AudioSource audio;

    // Upgrade
    public int upgradeCost;
    private GameObject upgrades;
    private GameObject upgradeSprite;
    private UIController UI;
    public string name;
    public string Title1;
    public string Title2;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = transform.GetChild(0).GetComponent<HealthBar>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.hide();
        healthBarTimer = 0f;
        spawnManager = GameObject.FindAnyObjectByType<SpawnManager>();
        audio = GetComponent<AudioSource>();

        // Upgrade
        upgrades = GameObject.Find("Upgrades");
        upgradeSprite = GameObject.Find("UpgradeSprite");
        upgrades.SetActive(true);
        upgrades.SetActive(false);
        UI = GameObject.FindAnyObjectByType<UIController>().GetComponent<UIController>();
    }

    // Update is called once per frame
    void Update()
    {   
        if (cooldown >= 0)
        {
            cooldown -= Time.deltaTime;
        }
        else
         {
             if (spawnManager.checkLane(lane) && ammunition != "")
             {
                 shoot();
             }
        }

        if (healthBarTimer > 0)
        {
            healthBarTimer -= Time.deltaTime;
            healthBar.show();
        }
        else
        {
            healthBar.hide();
        }

    }

    private void shoot()
    {

            cooldown = setCooldown;
            // GameObject muzzleFlash = Instantiate(Resources.Load("Prefabs/ef_3") as GameObject);
            // muzzleFlash.transform.position = transform.position + offset;
            // Destroy(muzzleFlash, 0.3f);
            if (audio != null) {
                audio.Play();
            }
            GameObject bullet = Instantiate(Resources.Load("Prefabs/"+ ammunition) as GameObject);
            bullet.transform.position = transform.position + offset;
            BulletBehaviour bulletBehavior = bullet.GetComponent<BulletBehaviour>();
            bulletBehavior.SetDamage(damage);
            
    }
    private void Explode()
    {
        GameObject explosion = Instantiate(Resources.Load("Prefabs/LaneExplosion") as GameObject);
        explosion.transform.position = transform.position;
        Destroy(explosion, 1.1f);
    }

    /*public void TakeDamage(int damageDealt)
    {
        
    }*/
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Weapon" && transform.tag == "Tower")
        {
            if (healthBar.isHidden())
            {
                healthBar.show();
            }
            healthBarTimer = 3;
            if(currentHealth <= 0)
            {
                Destroy(collision.gameObject);
                Destroy(transform.gameObject);
                if(Explodes) Explode();
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision){
        if(collision.tag == "Enemy" && Explodes && currentHealth <= 0){
            Destroy(collision.gameObject);
        }
    }

    public void setLane(int lane)
    {
        this.lane = lane;
    }

    // Upgrade Functions
    private void OnMouseOver()
    {
        updateUI();
    }

    private void OnMouseDown()
    {
        if (UI.coins > upgradeCost)
        {
            UI.coins -= upgradeCost;
            upgradeCost = upgradeCost * 3 / 2;
            damage = damage * 11 / 10;
            currentHealth = currentHealth * 11 / 10;
            maxHealth = maxHealth * 11 / 10;
            healthBar.SetHealth(currentHealth);
            healthBar.SetMaxHealth(maxHealth);
            healthBar.show();
            setCooldown = setCooldown / 11 * 10;
            updateUI();
            GoldFarming goldFarming = transform.GetComponent<GoldFarming>();
            if (goldFarming != null)
            {
                goldFarming.increaseMiningRate();
            }
        }
    }

    private void OnMouseExit()
    {
        healthBarTimer = 0; ;
    }

    private void updateUI()
    {
        upgrades.SetActive(true);
        healthBarTimer = 10;
        Sprite r = transform.GetComponent<SpriteRenderer>().sprite;
        upgradeSprite.GetComponent<SpriteRenderer>().sprite = r;
        upgradeSprite.transform.localScale = new Vector3(transform.localScale.x * 0.05f, transform.localScale.y * 0.125f, transform.localScale.z); ;

        GameObject upgradeCanvas = GameObject.Find("UpgradeCanvas");
        // Update upgrade info
        upgradeCanvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = name;
        upgradeCanvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = Title1;
        if (damage > 0)
        {
            upgradeCanvas.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = damage + "";
        }
        
        upgradeCanvas.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = Title2;

        if (cooldown > 0)
        {
            upgradeCanvas.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "" + setCooldown;

        }
        upgradeCanvas.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "" + upgradeCost;

    }
}
