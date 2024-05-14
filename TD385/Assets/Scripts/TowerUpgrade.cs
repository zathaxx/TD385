using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using TMPro;
using UnityEngine;

public class TowerUpgrade : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public bool isTall;
    private HealthBar healthBar;
    public Vector3 offset;
    private float cooldown = 1f;
    public float setCooldown;
    public float healthBarTimer;
    public string ammunition;
    public bool Explodes;

    private SpawnManager spawnManager;
    public int lane;

    public int upgradeCost;
    private GameObject upgradeInfo;
    private GameObject currentSprite;
    private UIController UI;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = transform.GetChild(0).GetComponent<HealthBar>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.hide();
        healthBarTimer = 0f;
        spawnManager = GameObject.FindAnyObjectByType<SpawnManager>();
        upgradeInfo = GameObject.Find("UpgradeInfo");
        currentSprite = GameObject.Find("CurrentSprite");
        upgradeInfo.SetActive(false);
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
        GameObject bullet = Instantiate(Resources.Load("Prefabs/" + ammunition) as GameObject);
        bullet.transform.position = transform.position + offset;
    }
    private void Explode()
    {
        GameObject explosion = Instantiate(Resources.Load("Prefabs/Explosion") as GameObject);
        explosion.transform.position = transform.position;
        Destroy(explosion, 1.1f);
    }

    /*public void TakeDamage(int damageDealt)
    {
        
    }*/

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Weapon" && transform.tag == "Tower")
        {
            if (healthBar.isHidden())
            {
                healthBar.show();
            }
            healthBarTimer = 3;
            if (currentHealth <= 0)
            {
                Destroy(collision.gameObject);
                Destroy(transform.gameObject);
                if (Explodes) Explode();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && Explodes && currentHealth <= 0)
        {
            Destroy(collision.gameObject);
        }
    }

    public void setLane(int lane)
    {
        this.lane = lane;
    }

    private void OnMouseOver()
    {
        updateUI();
    }

    private void OnMouseDown()
    {
        if (UI.coins > upgradeCost)
        {
            UI.coins -= upgradeCost;
            upgradeCost *= 2;
            // new sprite?
            // New ammo
            currentHealth *= 2;
            cooldown /= 2;
            updateUI();
        }
    }

    private void OnMouseExit()
    {
        upgradeInfo.SetActive(false);
    }

    private void updateUI()
    {
        upgradeInfo.SetActive(true);
        Sprite r = transform.GetComponent<SpriteRenderer>().sprite;
        currentSprite.GetComponent<SpriteRenderer>().sprite = r;
        currentSprite.transform.localScale = new Vector3(transform.localScale.x * 0.05f, transform.localScale.y * 0.125f, transform.localScale.z); ;

        GameObject current = GameObject.Find("CurrentCanvas");
        // Update upgrade info
        current.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = transform.name;
        current.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "" + setCooldown;
        current.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = "" + upgradeCost;
    }


}
