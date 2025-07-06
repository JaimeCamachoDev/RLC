using UnityEngine;

public class GravityZone : MonoBehaviour
{
    [Tooltip("Intensidad de la gravedad, normalmente 9.81")]
    public float gravityMagnitude = 9.81f;

    private void OnTriggerEnter(Collider other)
    {
        var customGravity = other.GetComponent<CustomGravityTarget>();
        if (customGravity != null)
        {
            // Define la gravedad local: -Y local del trigger, * magnitud deseada
            Vector3 localGravity = -transform.up * gravityMagnitude;
            customGravity.SetCustomGravity(localGravity);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var customGravity = other.GetComponent<CustomGravityTarget>();
        if (customGravity != null)
        {
            customGravity.RestoreDefaultGravity();
        }
    }
}
