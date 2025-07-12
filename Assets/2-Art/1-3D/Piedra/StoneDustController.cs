using UnityEngine;

public class StoneDustController : MonoBehaviour
{
    public ParticleSystem dustParticles;
    public Rigidbody rb;
    public string groundTag = "Ground";
    public float minSpeedToEmit = 1.0f;

    private bool isOnGround = false;

    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (dustParticles == null || rb == null) return;

        bool shouldEmit = isOnGround && rb.linearVelocity.magnitude > minSpeedToEmit;

        var emission = dustParticles.emission;
        emission.enabled = shouldEmit;
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag(groundTag))
        {
            isOnGround = true;
            Debug.Log("TOCANDO SUELO: " + collision.collider.name);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag(groundTag))
        {
            isOnGround = false;
            Debug.Log("SALIO DEL SUELO: " + collision.collider.name);
        }
    }
}
