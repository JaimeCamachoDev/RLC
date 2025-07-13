using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    const string VolumeKey = "MasterVolume";
    float minDb = -80f;
    float maxDb = 0f;

    void Start()
    {
        // Lee valor guardado, si no existe por defecto 0dB (1f en slider)
        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 0.8f); // 0.8f = volumen alto pero no max
        volumeSlider.value = savedVolume;
        SetVolume(savedVolume);

        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float value)
    {
        // Slider 0..1, lo convertimos a dB logarítmico
        float dB = Mathf.Lerp(minDb, maxDb, value);
        audioMixer.SetFloat(VolumeKey, dB);

        PlayerPrefs.SetFloat(VolumeKey, value);
        PlayerPrefs.Save();
    }
}
