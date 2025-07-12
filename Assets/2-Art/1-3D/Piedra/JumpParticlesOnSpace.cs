using UnityEngine;

[RequireComponent(typeof(JumpController))]
public class JumpParticlesOnSpace : MonoBehaviour
{
    public ParticleSystem jumpParticles;
    private JumpController jumpController;

    void Awake()
    {
        jumpController = GetComponent<JumpController>();
    }

    void Update()
    {
        if (jumpParticles == null || jumpController == null) return;

        // Detectar entrada de salto igual que JumpController
        if (Input.GetKeyDown(KeyCode.Space) &&
            (jumpController.IsGrounded() || jumpController.CheckIfCanJump()))
        {
            jumpParticles.Play();
        }
    }
}
