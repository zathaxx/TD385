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
    void Start()
    {
        Vector3 topLeft = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, 1f));
        background = GameObject.Find("Background");
        Bounds bound = background.GetComponent<SpriteRenderer>().bounds;
        transform.position = new Vector3(topLeft.x + (bound.size.x/2), topLeft.y - (bound.size.y/2), transform.position.z);
        textComponent = GameObject.Find("Balance").GetComponent<TextMeshProUGUI>();
        coins = 500;
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
}
