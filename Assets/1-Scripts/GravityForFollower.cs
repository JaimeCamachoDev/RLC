using UnityEngine;

public class GravityForFollower : MonoBehaviour
{
    public float lerpSpeed = 5f;

    void Update()
    {
        Vector3 gravityDir = Physics.gravity.normalized;
        Vector3 currentDown = -transform.up;

        if (gravityDir.sqrMagnitude < 0.0001f) return;

        Quaternion targetRot = Quaternion.FromToRotation(currentDown, gravityDir) * transform.rotation;
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * lerpSpeed);
    }
}
