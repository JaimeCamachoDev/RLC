using UnityEngine;

public class StoneDustController : MonoBehaviour
{
    public ParticleSystem dustParticles;
    public Rigidbody rb;
    public float minSpeedToEmit = 1.0f;

    private bool isOnGround = false;
    JumpController jumpController;
    void Start()
    {
        jumpController = GetComponent<JumpController>();
        if (rb == null) rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (dustParticles == null || rb == null) return;
        isOnGround = jumpController.IsGrounded();
        bool shouldEmit = isOnGround && rb.linearVelocity.magnitude > minSpeedToEmit;

        var emission = dustParticles.emission;
        emission.enabled = shouldEmit;
    }
}
