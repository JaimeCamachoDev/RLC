using UnityEngine;

public class SimpleWASDCameraController : MonoBehaviour
{
    public float moveForce = 30f;
    public Transform cameraTransform;
    public CustomGravityTarget gravityTarget; // Referencia al script que lleva la gravedad actual

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (gravityTarget == null) gravityTarget = GetComponent<CustomGravityTarget>();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // Solo aplica fuerza si hay input
        if (Mathf.Abs(h) > 0.01f || Mathf.Abs(v) > 0.01f)
        {
            // Dirección de la gravedad actual (en negativo, para saber qué es “abajo”)
            Vector3 gravityDir = gravityTarget != null ? gravityTarget.GetGravityDirection().normalized : Vector3.down;

            // Calcula el plano tangente (suelo local)
            Vector3 camForward = Vector3.ProjectOnPlane(cameraTransform.forward, gravityDir).normalized;
            Vector3 camRight = Vector3.ProjectOnPlane(cameraTransform.right, gravityDir).normalized;

            Vector3 move = (camForward * v + camRight * h).normalized;

            // Aplica fuerza en el plano tangente
            rb.AddForce(move * moveForce, ForceMode.Force);
        }
        // Si no hay input, no tocamos la velocidad
    }
}
