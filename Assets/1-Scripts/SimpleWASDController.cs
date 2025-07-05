using UnityEngine;

public class SimpleWASDCameraController : MonoBehaviour
{
    public float moveSpeed = 8f;
    public Transform cameraTransform; // Arrastra aquí tu cámara (MainCamera o la Cinemachine)

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal"); // A/D o Flechas
        float v = Input.GetAxis("Vertical");   // W/S o Flechas

        // Obtener forward y right de la cámara en plano XZ
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        // Ignorar el eje Y (evitar que suba o baje)
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        // Calcular movimiento relativo a la cámara
        Vector3 move = (camForward * v + camRight * h) * moveSpeed;

        // Mantener la velocidad vertical (gravedad)
        Vector3 velocity = rb.linearVelocity;
        velocity.x = move.x;
        velocity.z = move.z;
        rb.linearVelocity = velocity;
    }
}
