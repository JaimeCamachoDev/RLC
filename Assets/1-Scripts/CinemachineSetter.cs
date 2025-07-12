using System.Collections;
using Unity.Cinemachine;
using Unity.Cinemachine.TargetTracking;
using UnityEngine;

public class CinemachineSetter : MonoBehaviour
{
    [SerializeField] CinemachineCamera cinemachineCamera;
    [SerializeField] TrackerSettings bindingMode;
    [SerializeField] Vector3 targetOffset;
    [SerializeField] float radius;
    [SerializeField] InputAxis horizontalAxis;
    [SerializeField] InputAxis verticalAxis;
    [SerializeField] InputAxis radialAxis;

    IEnumerator Start()
    {
        ForceSwitchOrbital();
        cinemachineCamera.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        cinemachineCamera.gameObject.SetActive(true);
    }
    [ContextMenu("Force Switch Orbital")]
    public void ForceSwitchOrbital()
    {
        RemoveProcedural<CinemachineOrbitalFollow>();
        RemoveProcedural<CinemachineThirdPersonFollow>();

        var tps = cinemachineCamera.gameObject.AddComponent<CinemachineThirdPersonFollow>();

        Destroy(tps);

        var orbital = cinemachineCamera.gameObject.AddComponent<CinemachineOrbitalFollow>();
        orbital.TrackerSettings = bindingMode;
        orbital.TargetOffset = targetOffset;
        orbital.Radius = radius;
        orbital.HorizontalAxis = horizontalAxis;
        orbital.VerticalAxis = verticalAxis;
        orbital.RadialAxis = radialAxis;
    }

    void RemoveProcedural<T>() where T : Component
    {
        var comp = cinemachineCamera.GetComponent<T>();
        if (comp != null)
            DestroyImmediate(comp);
    }
}
