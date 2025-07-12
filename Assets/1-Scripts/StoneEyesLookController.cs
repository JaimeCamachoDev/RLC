using UnityEngine;

public class StoneEyesLookController : MonoBehaviour
{
    public Transform eyesTransform;
    public Camera cameraToUse;
    public string targetTag = "Player";
    public float eyeMoveSpeed = 16f;
    private Collider rockCollider;

    void Awake()
    {
        if (cameraToUse == null) cameraToUse = Camera.main;
        rockCollider = GetComponent<Collider>();
    }

    void Update()
    {
        if (eyesTransform == null || rockCollider == null || cameraToUse == null) return;

        Vector3 origin = cameraToUse.transform.position;
        Vector3 target = rockCollider.bounds.center;
        Vector3 dir = (target - origin).normalized;
        float maxDist = Vector3.Distance(origin, target) + rockCollider.bounds.extents.magnitude + 2f;

        RaycastHit hit;
        Vector3 eyeTargetPos = eyesTransform.position;

        Debug.DrawRay(origin, dir * maxDist, Color.cyan);

        if (Physics.Raycast(origin, dir, out hit, maxDist))
        {
            Debug.DrawLine(origin, hit.point, Color.magenta, 0.2f);
            if (hit.collider == rockCollider || hit.collider.CompareTag(targetTag))
            {
                eyeTargetPos = hit.point;
            }
        }

        eyesTransform.position = Vector3.Lerp(eyesTransform.position, eyeTargetPos, Time.deltaTime * eyeMoveSpeed);
        Vector3 lookAtCam = (cameraToUse.transform.position - eyesTransform.position).normalized;
        eyesTransform.rotation = Quaternion.Slerp(eyesTransform.rotation, Quaternion.LookRotation(lookAtCam, Vector3.up), Time.deltaTime * eyeMoveSpeed);
    }
}
