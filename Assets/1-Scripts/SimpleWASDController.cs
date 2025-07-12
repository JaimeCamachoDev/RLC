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
    [SerializeField] GameObject playerCam;
    [SerializeField] GameObject freeCam;
    [SerializeField] GameObject cross;

    float freeCamYaw = 0f;
    float freeCamPitch = 0f;

    void Awake()
    {
        _moveForce = moveForce;
        rb = GetComponent<Rigidbody>();
        jumpController = GetComponent<JumpController>();
        if (gravityTarget == null) gravityTarget = GetComponent<CustomGravityTarget>();
    }

    void Start()
    {
        if (freeCamFollower != null)
        {
            Vector3 euler = freeCamFollower.eulerAngles;
            freeCamYaw = euler.y;
            freeCamPitch = euler.x;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            isFreeCam = !isFreeCam;
            cross.SetActive(isFreeCam);
            if (isFreeCam)
            {
                playerCam.SetActive(false);
                freeCam.SetActive(true);
            }
            else
            {
                freeCam.SetActive(false);
                playerCam.SetActive(true);
            }
            if (followerConstraint != null)
                followerConstraint.enabled = !isFreeCam;
        }

        if (isFreeCam && freeCamFollower != null)
        {
            float mouseX = Input.GetAxis("Mouse X") * freeCamSensitivity * 5f;
            float mouseY = Input.GetAxis("Mouse Y") * freeCamSensitivity * 5f;

            freeCamYaw += mouseX;
            freeCamPitch -= mouseY;
            freeCamPitch = Mathf.Clamp(freeCamPitch, -89f, 89f);

            freeCamFollower.rotation = Quaternion.Euler(freeCamPitch, freeCamYaw, 0f);

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
        if (isFreeCam && freeCamFollower != null)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            float y = 0f;

            if (Input.GetKey(KeyCode.E)) y += 1f;
            if (Input.GetKey(KeyCode.Q)) y -= 1f;

            Vector3 move = (freeCamFollower.forward * v + freeCamFollower.right * h + Vector3.up * y).normalized;

            if (move.sqrMagnitude > 0.001f)
            {
                freeCamFollower.position += move * moveForce * freeCamSensitivity * Time.fixedDeltaTime;
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
