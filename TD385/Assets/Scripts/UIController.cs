using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public int coins;
    // Start is called before the first frame update
    GameObject background;
    public GameObject gameOverScreen;

    private GameObject information;
    private GameObject InfoSprite;
    private GameObject UpgradeInfo;
    void Start()
    {
        Vector3 topLeft = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, 1f));
        background = GameObject.Find("Background");
        Bounds bound = background.GetComponent<SpriteRenderer>().bounds;
        transform.position = new Vector3(topLeft.x + (bound.size.x/2), topLeft.y - (bound.size.y/2), transform.position.z);
        textComponent = GameObject.Find("Balance").GetComponent<TextMeshProUGUI>();
        coins = 500;
        information = GameObject.Find("Information");
        InfoSprite = GameObject.Find("InfoSprite");
        information.transform.position = new Vector3(transform.position.x + bound.size.x, transform.position.y, transform.position.z);
        information.SetActive(false);
        UpgradeInfo = GameObject.Find("UpgradeInfo");
        UpgradeInfo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 topLeft = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, 1f));
        Bounds bound = background.GetComponent<SpriteRenderer>().bounds;
        transform.position = new Vector3(topLeft.x + (bound.size.x/2), topLeft.y - (bound.size.y/2), transform.position.z);
        textComponent.text = coins.ToString();

        if (GameObject.Find("Gold").transform.childCount <= 0) {
            gameOverScreen.SetActive(true);
        }
    }


    public void OnButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void AddCoins(int coins)
    {
        this.coins += coins;
    }

    public void UpdateInfoUI(GameObject UITower) {
        information.SetActive(true);
        Sprite r = UITower.GetComponent<SpriteRenderer>().sprite;
        InfoSprite.GetComponent<SpriteRenderer>().sprite = r;
        Vector3 size = new Vector3(UITower.transform.localScale.x * 0.0625f, UITower.transform.localScale.y * 0.125f, UITower.transform.localScale.z);
        InfoSprite.transform.localScale = size;

        GameObject InfoCanvas = GameObject.Find("InfoCanvas");
        InfoCanvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = UITower.name;
        //InfoCanvas.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "100";
        //InfoCanvas.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "4s";
    }
}
