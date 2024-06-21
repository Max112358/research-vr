using UnityEngine;
using TMPro;

public class RuntimeTracker : MonoBehaviour
{
    public TextMeshPro timeText;
    private float timer;

    void Start()
    {
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;
        UpdateTimeText();
    }

    void UpdateTimeText()
    {
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);
        int milliseconds = Mathf.FloorToInt((timer * 1000) % 1000);

        //string timeString = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);
        timeText.text = timeString;
    }
}
