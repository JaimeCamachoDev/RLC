using UnityEngine;

public class SpeedZone : MonoBehaviour
{
    [Tooltip("Fuerza a aplicar cada FixedUpdate")]
    public Vector3 turboForce = new Vector3(0, 0, 50f);

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null)
        {
            rb.AddForce(transform.TransformDirection(turboForce), ForceMode.Force);
        }
    }
}
