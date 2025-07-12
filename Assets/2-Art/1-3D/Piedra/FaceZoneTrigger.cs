using UnityEngine;

public class FaceZoneTrigger : MonoBehaviour
{
    public int zoneExpressionIndex = 6;

    private void OnTriggerEnter(Collider other)
    {
        var faceController = other.GetComponentInChildren<StoneFaceExpressionController>();
        if (faceController == null)
            faceController = other.GetComponentInParent<StoneFaceExpressionController>();

        if (faceController != null)
        {
            faceController.ForceZoneExpression(zoneExpressionIndex);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var faceController = other.GetComponentInChildren<StoneFaceExpressionController>();
        if (faceController == null)
            faceController = other.GetComponentInParent<StoneFaceExpressionController>();

        if (faceController != null)
        {
            faceController.ClearZoneExpression();
        }
    }
}
