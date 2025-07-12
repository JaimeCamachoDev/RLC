using UnityEngine;
using TMPro;

public class TimerDisplay : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float timer = 0f;
    public bool counting = true;

    void Update()
    {
        if (counting)
            timer += Time.deltaTime;

        int minutes = (int)(timer / 60f);
        int seconds = (int)(timer % 60f);
        int decimals = (int)((timer - Mathf.Floor(timer)) * 100f);

        timerText.text = string.Format("{0:00}:{1:00},{2:00}", minutes, seconds, decimals);
    }

    public void ResetTimer()
    {
        timer = 0f;
    }

    public void StopTimer()
    {
        counting = false;
    }

    public void StartTimer()
    {
        counting = true;
    }
}
