using System.Collections.Generic;
using UnityEngine;
public enum Languages
{
    ES,
    EN
}
public class LanguageSelector : MonoBehaviour
{
    public Languages language;
    public List<TextLang> textToChange;
    public static LanguageSelector Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void LanguageSelectorStatus()
    {
        foreach (TextLang item in textToChange)
        {
            item.ChangeLanguage();
        }
    }
    public void AddText(TextLang text)
    {
        textToChange.Add(text);
    }
}