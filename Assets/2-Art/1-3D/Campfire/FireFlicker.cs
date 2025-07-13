using UnityEngine;

[RequireComponent(typeof(Light))]
public class FireFlicker : MonoBehaviour
{
    [Header("Intensidad")]
    public float minIntensity = 0.8f;
    public float maxIntensity = 1.2f;

    [Header("Velocidad de parpadeo")]
    public float flickerSpeed = 0.1f;

    [Header("Aleatoriedad")]
    public float intensityNoise = 0.1f;

    private Light fireLight;
    private float baseIntensity;
    private float timer;

    void Start()
    {
        fireLight = GetComponent<Light>();
        baseIntensity = fireLight.intensity;
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= flickerSpeed)
        {
            float random = Random.Range(-intensityNoise, intensityNoise);
            fireLight.intensity = Mathf.Clamp(baseIntensity + random, minIntensity, maxIntensity);
            timer = 0f;
        }
    }
}
