using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update

    private TextMeshProUGUI textComponent;

    public GameObject CreditsCanvas;
    public GameObject MenuCanvas;
    void Start()
    {
        TextMeshProUGUI title = GameObject.Find("Title").GetComponent<TextMeshProUGUI>();
        title.outlineWidth = 0.4f;
        title.outlineColor = new Color32(0, 0, 0, 255);

        textComponent = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        textComponent.outlineWidth = 0.4f;
        textComponent.outlineColor = new Color32(0, 0, 0, 255);

        for (int i = 1; i < CreditsCanvas.transform.childCount; i++) {
            TextMeshProUGUI text = CreditsCanvas.transform.GetChild(i).GetComponent<TextMeshProUGUI>();
            text.outlineWidth = 0.3f;
            text.outlineColor = new Color32(0, 0, 0, 255);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayButtonClick()
    {
        SceneManager.LoadScene("Final");
    }

    public void OnCreditsButtonClick() {
        MenuCanvas.SetActive(false);
        CreditsCanvas.SetActive(true);
    }

    public void GoBackButtonClick() {
        CreditsCanvas.SetActive(false);
        MenuCanvas.SetActive(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        textComponent.color = new Color32(255, 215, 0, 255);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        textComponent.color = new Color32(255, 255, 255, 255);
    }
}
