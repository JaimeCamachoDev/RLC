using UnityEngine;
using UnityEngine.Events;

public class ShroomControl : MonoBehaviour
{
    public UnityEvent onBounce;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            onBounce.Invoke();
        }
    }
}
