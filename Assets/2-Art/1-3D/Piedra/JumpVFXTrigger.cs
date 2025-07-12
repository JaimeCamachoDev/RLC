using UnityEngine;

public class JumpVFXTrigger : MonoBehaviour
{
    public ParticleSystem jumpParticles;
    public JumpController jumpController;
    public bool wasGroundedLastFrame = true;

    void Start()
    {
        jumpController = GetComponent<JumpController>();

        if (jumpController == null)
            Debug.LogError("JumpController no encontrado en el mismo GameObject.");

        if (jumpParticles == null)
            Debug.LogWarning("No se ha asignado un sistema de partículas al JumpVFXTrigger.");
    }

    void Update()
    {
        if (jumpController == null || jumpParticles == null) return;

        bool isGrounded = jumpController.IsGrounded();

        // Detectar momento del salto: estaba en el suelo y ahora ya no (salto realizado)
        if (wasGroundedLastFrame && !isGrounded && !jumpController.CheckIfCanJump())
        {
            jumpParticles.Play();
            Debug.Log("holiii");
        }

        wasGroundedLastFrame = isGrounded;
    }
}
