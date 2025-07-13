using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] Transform checkpoint;
    bool activated;
    [SerializeField] GameObject[] vfx;
    private void Start()
    {
        activated = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && activated==false)
        {
            activated = true;
            foreach (GameObject item in vfx)
            {
                item.SetActive(true);
            }
            ResetAndCheckPoint.instance.UpdateCheckpoint(checkpoint.position);
        }
    }
}
