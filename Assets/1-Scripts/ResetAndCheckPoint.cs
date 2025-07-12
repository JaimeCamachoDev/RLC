using UnityEngine;

public class ResetAndCheckPoint : MonoBehaviour
{
    public static ResetAndCheckPoint instance;
    private Vector3 lastSavedPosition;
    private Rigidbody rb;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastSavedPosition = transform.position + Vector3.up;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetToCheckpoint();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            UpdateCheckpoint(transform.position);
        }
    }

    public void UpdateCheckpoint(Vector3 where)
    {
        lastSavedPosition = where + Vector3.up;
    }

    public void ResetToCheckpoint()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.position = lastSavedPosition;
        rb.MovePosition(lastSavedPosition);
    }
    public void Unstuck()
    {
        Vector3 aux = rb.transform.position;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.position = aux+Vector3.up*1.5f;
        rb.MovePosition(aux + Vector3.up * 1.5f);
    }
}
