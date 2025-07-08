using UnityEngine;
using System.Collections.Generic;

public class GravityZone : MonoBehaviour
{
    [Tooltip("Intensidad de la gravedad, normalmente 9.81")]
    public float gravityMagnitude = 9.81f;

    [SerializeField] CustomGravityTarget customGravityTarget;

    // Lista de zonas activas, la última es la que manda
    private static List<GravityZone> activeZones = new List<GravityZone>();

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (!activeZones.Contains(this))
            activeZones.Add(this);

        // Aplica la gravedad de esta zona (la más reciente)
        SetGlobalGravityOfLastZone();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // Quita esta zona de la lista
        if (activeZones.Contains(this))
            activeZones.Remove(this);

        // Aplica la gravedad de la última zona que queda, o vuelve a normal si no hay ninguna
        SetGlobalGravityOfLastZone();
    }

    private void SetGlobalGravityOfLastZone()
    {
        Vector3 newGravity;
        if (activeZones.Count > 0)
        {
            // Usa la gravedad de la última zona activa
            GravityZone lastZone = activeZones[activeZones.Count - 1];
            newGravity = -lastZone.transform.up * lastZone.gravityMagnitude;
        }
        else
        {
            // Vuelve a la gravedad normal
            newGravity = new Vector3(0, -9.81f, 0);
        }

        Physics.gravity = newGravity;
        if (customGravityTarget != null)
            customGravityTarget.SetCustomGravity(newGravity);
    }
}
