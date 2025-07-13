using UnityEngine;

public class RockSound : MonoBehaviour
{
    [SerializeField] AudioSource rockAS;
    [SerializeField] float baseVolume = 0.2f;
    [SerializeField] float maxVolume = 1.0f;
    [SerializeField] float volumeLerpSpeed = 5f;
    [SerializeField] float minTriggerVelocity = 2f;

    Rigidbody rb;
    float targetVolume;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rockAS.loop = true;
        rockAS.volume = baseVolume;
        targetVolume = baseVolume;
        if (!rockAS.isPlaying)
            rockAS.Play();
    }

    void Update()
    {
        // Baja el volumen suavemente al baseVolume si ha subido antes
        if (rockAS.volume != targetVolume)
        {
            rockAS.volume = Mathf.Lerp(rockAS.volume, targetVolume, Time.deltaTime * volumeLerpSpeed);
            if (Mathf.Abs(rockAS.volume - targetVolume) < 0.01f)
                rockAS.volume = targetVolume;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        float vel = rb.linearVelocity.magnitude;
        if (vel >= minTriggerVelocity)
        {
            // Sube el volumen según la velocidad de la roca (proporcional, pero nunca menos que baseVolume)
            float newTarget = Mathf.Lerp(baseVolume, maxVolume, Mathf.InverseLerp(minTriggerVelocity, minTriggerVelocity * 4f, vel));
            SetPeakVolume(newTarget);
        }
    }

    void SetPeakVolume(float vol)
    {
        targetVolume = Mathf.Clamp(vol, baseVolume, maxVolume);
        // Baja de nuevo al baseVolume pasado un breve tiempo
        CancelInvoke(nameof(RestoreBaseVolume));
        Invoke(nameof(RestoreBaseVolume), 0.2f);
    }

    void RestoreBaseVolume()
    {
        targetVolume = baseVolume;
    }
}
