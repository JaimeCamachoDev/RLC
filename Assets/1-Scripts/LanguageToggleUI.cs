using UnityEngine;
using UnityEngine.UI;

public class LanguageToggleUI : MonoBehaviour
{
    public Toggle ESPToggle;
    public Toggle ENGToggle;

    void Start()
    {
        // Inicializa el estado según PlayerPrefs o valor por defecto
        int lang = PlayerPrefs.GetInt("Language", 0); // 0=ES, 1=EN
        UpdateToggles(lang);

        ESPToggle.onValueChanged.AddListener((on) => {
            if (on) SetLanguage(0);
        });
        ENGToggle.onValueChanged.AddListener((on) => {
            if (on) SetLanguage(1);
        });
    }

    void SetLanguage(int lang)
    {
        PlayerPrefs.SetInt("Language", lang);
        UpdateToggles(lang);

        // Actualiza el idioma en el LanguageSelector
        if (LanguageSelector.Instance != null)
        {
            LanguageSelector.Instance.language = (lang == 0) ? Languages.ES : Languages.EN;
            LanguageSelector.Instance.LanguageSelectorStatus();
        }
    }

    void UpdateToggles(int lang)
    {
        if (lang == 0)
        {
            ESPToggle.isOn = true;
            ESPToggle.interactable = false;
            ENGToggle.isOn = false;
            ENGToggle.interactable = true;
        }
        else
        {
            ESPToggle.isOn = false;
            ESPToggle.interactable = true;
            ENGToggle.isOn = true;
            ENGToggle.interactable = false;
        }
    }
}
