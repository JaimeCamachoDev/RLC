using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] Transform checkpoint;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ResetAndCheckPoint.instance.UpdateCheckpoint(checkpoint.position);
        }
    }
}
