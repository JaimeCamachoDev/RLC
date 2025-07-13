using UnityEngine;
using TMPro;

public class BestTimeMenuUI : MonoBehaviour
{
    public TextMeshProUGUI bestTimeText;

    void Start()
    {
        string bestTime = PlayerPrefs.GetString("BestTime" + "_String", "--:--,--"); 
        bestTimeText.text = bestTime;
    }
}
