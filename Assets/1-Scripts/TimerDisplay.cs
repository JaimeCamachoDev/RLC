using UnityEngine;
using TMPro;

public class TimerDisplay : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float timer = 0f;
    public bool counting = true;

    const string BestTimeKey = "BestTime";

    void Update()
    {
        if (counting)
            timer += Time.deltaTime;

        timerText.text = FormatTime(timer);
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

    public string FormatTime(float t)
    {
        int minutes = (int)(t / 60f);
        int seconds = (int)(t % 60f);
        int decimals = (int)((t - Mathf.Floor(t)) * 100f);
        return string.Format("{0:00}:{1:00},{2:00}", minutes, seconds, decimals);
    }

    public void SaveIfBest()
    {
        float best = PlayerPrefs.GetFloat(BestTimeKey, 0f);
        if (best == 0f || timer < best)
        {
            PlayerPrefs.SetFloat(BestTimeKey, timer);
            PlayerPrefs.SetString(BestTimeKey + "_String", FormatTime(timer));
            PlayerPrefs.Save();
        }
    }

    public static string GetBestTimeString()
    {
        return PlayerPrefs.GetString(BestTimeKey + "_String", "--:--,--");
    }
}
