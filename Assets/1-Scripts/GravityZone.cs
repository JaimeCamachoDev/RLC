using UnityEngine;
using System.Collections.Generic;

public class GravityZone : MonoBehaviour
{
    [Tooltip("Intensidad de la gravedad, normalmente 9.81")]
    public float gravityMagnitude = 9.81f;

    [SerializeField] CustomGravityTarget customGravityTarget;

    private static List<GravityZone> activeZones = new List<GravityZone>();
    private void Start()
    {
        activeZones.Clear();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (!activeZones.Contains(this))
            activeZones.Add(this);

        SetGlobalGravityOfLastZone();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (activeZones.Contains(this))
            activeZones.Remove(this);

        SetGlobalGravityOfLastZone();
    }

    private void SetGlobalGravityOfLastZone()
    {
        Vector3 newGravity;
        if (activeZones.Count > 0)
        {
            GravityZone lastZone = activeZones[activeZones.Count - 1];
            newGravity = -lastZone.transform.up * lastZone.gravityMagnitude;
        }
        else
        {
            newGravity = new Vector3(0, -9.81f, 0);
        }
        Physics.gravity = newGravity;
        if (customGravityTarget != null)
            customGravityTarget.SetCustomGravity(newGravity);
    }
}
