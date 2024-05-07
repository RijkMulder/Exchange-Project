using UnityEngine;
using Events;
using UnityEngine.UI;

public class RodHealthBar : MonoBehaviour
{
    Image healthBar;
    private void Start()
    {
        healthBar = GetComponent<Image>();
    }
    private void OnEnable()
    {
        EventManager.PlayerHealthUpdate += UpdateHealthBar;
    }
    private void OnDisable()
    {
        EventManager.PlayerHealthUpdate -= UpdateHealthBar; 
    }
    private void UpdateHealthBar(float value, float max)
    {
        healthBar.fillAmount = Mathf.Clamp(value / max, 0, 1);
    }
}
