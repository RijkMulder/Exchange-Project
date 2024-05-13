using UnityEngine;
using Events;
using TMPro;

public class Clock : MonoBehaviour
{
    private TMP_Text text;
    private void Start()
    {
        text = GetComponent<TMP_Text>();
    }
    private void OnEnable()
    {
        EventManager.TimeChanged += UpdateTime;
    }
    private void OnDisable()
    {
        EventManager.TimeChanged -= UpdateTime;
    }
    private void UpdateTime(string newTime)
    {
        text.text = newTime;
    }
}
