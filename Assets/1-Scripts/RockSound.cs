using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class RockSound : MonoBehaviour
{
    [SerializeField] AudioSource rockAS;
    [SerializeField] AudioClip[] rollingSounds;
    bool soundPlayed;
    Rigidbody rb;
    [SerializeField] float velocity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void PlaySound()
    {
        rockAS.PlayOneShot(rollingSounds[Random.Range(0, rollingSounds.Length)]);
        StartCoroutine(RecoverSound());
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (soundPlayed == false && rb.linearVelocity.magnitude>=velocity)
        {
            soundPlayed = true;
            PlaySound();
        }
    }
    IEnumerator RecoverSound()
    {
        yield return new WaitForSeconds(Random.Range(0, 0.8f));
        soundPlayed = false;
    }
}
