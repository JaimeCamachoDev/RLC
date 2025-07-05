using UnityEngine;

public class SimpleWASDController : MonoBehaviour
{
    public float moveSpeed = 8f;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Leer input
        float h = Input.GetAxis("Horizontal"); // A/D o Flechas
        float v = Input.GetAxis("Vertical");   // W/S o Flechas

        // Vector de movimiento en plano XZ
        Vector3 move = new Vector3(h, 0, v) * moveSpeed;

        // Mantener la velocidad vertical (ca�da/gravedad)
        Vector3 velocity = rb.linearVelocity;
        velocity.x = move.x;
        velocity.z = move.z;
        rb.linearVelocity = velocity;
    }
}
