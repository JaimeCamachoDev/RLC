using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CustomGravityTarget : MonoBehaviour
{
    private Rigidbody rb;
    private bool useCustomGravity = false;
    private Vector3 currentGravity = Physics.gravity;
    private Vector3 defaultGravity;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        defaultGravity = Physics.gravity;
        rb.useGravity = false; 
    }

    void FixedUpdate()
    {
        if (useCustomGravity)
        {
            rb.AddForce(currentGravity, ForceMode.Acceleration);
        }
        else
        {
            rb.AddForce(defaultGravity, ForceMode.Acceleration);
        }
    }

    public void SetCustomGravity(Vector3 gravity)
    {
        currentGravity = gravity;
        useCustomGravity = true;
    }

    public void RestoreDefaultGravity()
    {
        useCustomGravity = false;
    }
    public Vector3 GetGravityDirection()
    {
        return useCustomGravity ? currentGravity.normalized : defaultGravity.normalized;
    }

}
