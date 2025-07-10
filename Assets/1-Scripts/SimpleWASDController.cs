using UnityEngine;
using UnityEngine.Animations;

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

    [Header("Free Camera")]
    public Transform freeCamFollower;
    public PositionConstraint followerConstraint;
    public float freeCamSensitivity = 1.0f;
    public float freeCamSensitivityMin = 0.1f;
    public float freeCamSensitivityMax = 10f;
    public float freeCamSensitivityStep = 0.1f;

    bool isFreeCam = false;

    void Awake()
    {
        _moveForce = moveForce;
        rb = GetComponent<Rigidbody>();
        jumpController = GetComponent<JumpController>();
        if (gravityTarget == null) gravityTarget = GetComponent<CustomGravityTarget>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            isFreeCam = !isFreeCam;
            if (followerConstraint != null)
                followerConstraint.enabled = !isFreeCam;
        }

        if (isFreeCam)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (Mathf.Abs(scroll) > 0.01f)
            {
                freeCamSensitivity += scroll * freeCamSensitivityStep * 10f;
                freeCamSensitivity = Mathf.Clamp(freeCamSensitivity, freeCamSensitivityMin, freeCamSensitivityMax);
            }
        }
    }

    void FixedUpdate()
    {
        if (isFreeCam)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            float y = 0f;

            if (Input.GetKey(KeyCode.E)) y += 1f;
            if (Input.GetKey(KeyCode.Q)) y -= 1f;

            Vector3 gravityDir = gravityTarget != null ? gravityTarget.GetGravityDirection().normalized : Vector3.down;
            Vector3 camForward = Vector3.ProjectOnPlane(cameraTransform.forward, gravityDir).normalized;
            Vector3 camRight = Vector3.ProjectOnPlane(cameraTransform.right, gravityDir).normalized;

            Vector3 move = (camForward * v + camRight * h).normalized;
            Vector3 moveVertical = gravityDir * -y;

            Vector3 totalMove = (move + moveVertical).normalized;

            if (totalMove.sqrMagnitude > 0.001f)
            {
                freeCamFollower.position += totalMove * moveForce * freeCamSensitivity * Time.fixedDeltaTime;
            }
            return;
        }

        if (jumpController.CheckIfCanJump())
        {
            moveForce = _moveForce;
        }
        else
        {
            moveForce = _moveForce * airRestriction;
        }
        float hh = Input.GetAxis("Horizontal");
        float vv = Input.GetAxis("Vertical");

        if (Mathf.Abs(hh) > 0.01f || Mathf.Abs(vv) > 0.01f)
        {
            Vector3 gravityDir = gravityTarget != null ? gravityTarget.GetGravityDirection().normalized : Vector3.down;
            Vector3 camForward = Vector3.ProjectOnPlane(cameraTransform.forward, gravityDir).normalized;
            Vector3 camRight = Vector3.ProjectOnPlane(cameraTransform.right, gravityDir).normalized;
            Vector3 move = (camForward * vv + camRight * hh).normalized;

            rb.AddForce(move * moveForce, ForceMode.Force);
        }
    }
}
