using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class JumpController : MonoBehaviour
{
    [Header("Salto")]
    public float jumpForce = 8f;
    public float groundCheckDistance = 0.2f;
    public LayerMask groundLayer = ~0; // Por defecto, todo

    private Rigidbody rb;
    private bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Solo saltar si está en el suelo y se presiona espacio
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            Jump();
        }
    }

    /// <summary>
    /// Lógica para el salto físico.
    /// </summary>
    public void Jump()
    {
        // Cancela cualquier impulso vertical previo y aplica el salto
        Vector3 vel = rb.linearVelocity;
        vel.y = 0;
        rb.linearVelocity = vel;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    /// <summary>
    /// Chequeo básico de si está en el suelo usando Raycast.
    /// </summary>
    public bool IsGrounded()
    {
        // Raycast desde el centro hacia abajo
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance + 0.1f, groundLayer);
    }
}
