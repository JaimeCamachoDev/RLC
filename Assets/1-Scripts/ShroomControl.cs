using UnityEngine;
using UnityEngine.Events;

public class ShroomControl : MonoBehaviour
{
    public UnityEvent onBounce;
    [SerializeField] float maxTime = 0.2f;
    float timeBetween;
    private void Update()
    {
        timeBetween += Time.deltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && timeBetween>= maxTime)
        {
            onBounce.Invoke();
            timeBetween = 0f;
        }
    }
}
