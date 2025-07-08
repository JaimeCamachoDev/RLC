using UnityEngine;

public class ResetAndCheckPoint : MonoBehaviour
{
    private Vector3 lastSavedPosition;
    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastSavedPosition = transform.position+Vector3.up;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.position = lastSavedPosition;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            lastSavedPosition = transform.position + Vector3.up;
        }
    }
}
