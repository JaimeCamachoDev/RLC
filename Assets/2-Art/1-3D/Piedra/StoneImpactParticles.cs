using UnityEngine;

public class StoneImpactParticles : MonoBehaviour
{
    public ParticleSystem impactEffect;
    public float impactThreshold = 6.0f;

    private Rigidbody rb;
    private Vector3 lastVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastVelocity = rb.linearVelocity;
    }

    void Update()
    {
        lastVelocity = rb.linearVelocity;
    }

    void OnCollisionEnter(Collision collision)
    {
        float impactForce = (rb.linearVelocity - lastVelocity).magnitude;

        if (impactForce >= impactThreshold)
        {
            if (impactEffect != null)
            {
                ContactPoint contact = collision.contacts[0];
                impactEffect.transform.position = contact.point;
                impactEffect.transform.rotation = Quaternion.LookRotation(contact.normal);
                impactEffect.Play();
            }
        }
    }
}
