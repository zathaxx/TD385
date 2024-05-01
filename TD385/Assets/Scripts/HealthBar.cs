using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    private bool hidden;

    private void Start()
    {
        hidden = false;
    }
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void hide()
    {
        hidden = true;
        transform.gameObject.SetActive(false);
    }

    public void show()
    {
        hidden = false;
        transform.gameObject.SetActive(true);
    }

    public bool isHidden()
    {
        return hidden;
    }
}
