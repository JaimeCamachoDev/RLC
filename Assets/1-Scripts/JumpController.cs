using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class JumpController : MonoBehaviour
{
    [Header("Salto")]
    public float jumpForce = 8f;
    public float groundCheckDistance = 0.2f;
    public LayerMask groundLayer = ~0;
    public float coyoteTime = 0.18f; // Tiempo de gracia en segundos

    private Rigidbody rb;
    private float lastGroundedTime = -1f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Actualiza el estado de grounded y el timer de coyote
        if (IsGrounded())
            lastGroundedTime = Time.time;

        // Salta si pulsas espacio y est� en suelo o dentro del coyote time
        if (Input.GetKeyDown(KeyCode.Space) && (IsGrounded() || CheckIfCanJump()))
        {
            Jump();
        }
    }
    public bool CheckIfCanJump()
    {
        return (Time.time - lastGroundedTime) <= coyoteTime;
    }
    /// <summary>
    /// L�gica para el salto f�sico.
    /// </summary>
    public void Jump()
    {
        // Cancela cualquier impulso vertical global previo y aplica el salto
        Vector3 vel = rb.linearVelocity; // OJO: velocity es en world space
        vel.y = 0f;
        rb.linearVelocity = vel;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Siempre hacia arriba global
    }

    /// <summary>
    /// Chequeo robusto de si est� en el suelo usando Raycast desde la base del collider.
    /// </summary>
    public bool IsGrounded()
    {
        Collider col = GetComponent<Collider>();
        Vector3 origin = col.bounds.center;
        origin.y = col.bounds.min.y + 0.05f; // Ligeramente sobre la base
        float checkDist = groundCheckDistance + 0.05f;

        // Puedes a�adir m�s raycasts alrededor si la roca es muy ancha o irregular
        return Physics.Raycast(origin, Vector3.down, checkDist, groundLayer);
    }
}
