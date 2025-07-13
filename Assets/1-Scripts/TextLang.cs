using TMPro;
using UnityEngine;

public class TextLang : MonoBehaviour
{
    public Languages myLanguage;
    [SerializeField] string espText; 
    [SerializeField] string engText;
    TextMeshProUGUI text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PlayerPrefs.HasKey("Language"))
        {
            if (PlayerPrefs.GetInt("Language") == 0)
            {
                myLanguage = Languages.ES;
            }
            else if (PlayerPrefs.GetInt("Language") == 1)
            {
                myLanguage = Languages.EN;
            }
        }
        else
        {
            PlayerPrefs.SetInt("Language", 0);
            myLanguage = Languages.ES;
        }
        text = GetComponent<TextMeshProUGUI>();
        LanguageSelector.Instance.AddText(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (myLanguage == Languages.ES)
        {
            text.text = espText;
        }
        else if (myLanguage == Languages.EN)
        {
            text.text = engText;
        }

    }
    public void ChangeLanguage()
    {
        if (myLanguage == Languages.ES)
        {
            myLanguage = Languages.EN;
            PlayerPrefs.SetInt("Language", 1);
        }
        else if (myLanguage == Languages.EN)
        {
            myLanguage = Languages.ES;
            PlayerPrefs.SetInt("Language", 0);
        }
    }
}
