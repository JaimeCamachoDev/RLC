using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class JumpController : MonoBehaviour
{
    public float jumpForce = 8f;
    public float groundCheckDistance = 0.2f;
    public LayerMask groundLayer = ~0;
    public float coyoteTime = 0.18f;
    private Rigidbody rb;
    private float lastGroundedTime = -1f;
    private CustomGravityTarget gravityTarget;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        gravityTarget = GetComponent<CustomGravityTarget>();
    }

    void Update()
    {
        if (IsGrounded())
            lastGroundedTime = Time.time;

        if (Input.GetKeyDown(KeyCode.Space) && (IsGrounded() || CheckIfCanJump()))
            Jump();
    }
    public bool CheckIfCanJump()
    {
        return (Time.time - lastGroundedTime) <= coyoteTime;
    }
    public void Jump()
    {
        Vector3 vel = rb.linearVelocity;
        Vector3 gravityDir = gravityTarget != null ? gravityTarget.GetGravityDirection().normalized : Vector3.down;
        vel = vel - Vector3.Project(vel, -gravityDir); // Quita la velocidad alineada con la gravedad
        rb.linearVelocity = vel;
        rb.AddForce(-gravityDir * jumpForce, ForceMode.Impulse);
    }
    public bool IsGrounded()
    {
        Collider col = GetComponent<Collider>();
        Vector3 gravityDir = gravityTarget != null ? gravityTarget.GetGravityDirection().normalized : Vector3.down;
        Vector3 origin = col.bounds.center - gravityDir * (col.bounds.extents.y - 0.05f);
        float checkDist = groundCheckDistance + 0.05f;

        Debug.DrawRay(origin, gravityDir * checkDist, Color.magenta, 0.05f);
        return Physics.Raycast(origin, gravityDir, checkDist, groundLayer);
    }
}
