using UnityEngine;
using UnityEngine.Events;

public class EndTrigger : MonoBehaviour
{
    public UnityEvent onEnter;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            onEnter.Invoke();
        }

    }
}
