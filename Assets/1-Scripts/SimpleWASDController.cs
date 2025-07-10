using UnityEngine;

public class SimpleWASDCameraController : MonoBehaviour
{
    public float moveForce = 30f;
    float _moveForce;
    [Tooltip("Esto multiplica a la velocidad de movimiento")]
    public float airRestriction = 0.2f;
    public Transform cameraTransform;
    public CustomGravityTarget gravityTarget; 
    private Rigidbody rb;
    private JumpController jumpController;

    void Awake()
    {
        _moveForce = moveForce;
        rb = GetComponent<Rigidbody>();
        jumpController = GetComponent<JumpController>();
        if (gravityTarget == null) gravityTarget = GetComponent<CustomGravityTarget>();
    }

    void FixedUpdate()
    {
        if (jumpController.CheckIfCanJump())
        {
            moveForce = _moveForce;
        }
        else
        {
            moveForce = _moveForce * airRestriction;
        }
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (Mathf.Abs(h) > 0.01f || Mathf.Abs(v) > 0.01f)
        {
            Vector3 gravityDir = gravityTarget != null ? gravityTarget.GetGravityDirection().normalized : Vector3.down;

            Vector3 camForward = Vector3.ProjectOnPlane(cameraTransform.forward, gravityDir).normalized;
            Vector3 camRight = Vector3.ProjectOnPlane(cameraTransform.right, gravityDir).normalized;

            Vector3 move = (camForward * v + camRight * h).normalized;

            rb.AddForce(move * moveForce, ForceMode.Force);
        }
    }
}
