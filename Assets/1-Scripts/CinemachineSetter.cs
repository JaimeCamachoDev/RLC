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

    private void Start()
    {
        ForceSwitchOrbital();
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;
    }
    [ContextMenu("Force Switch Orbital")]
    public void ForceSwitchOrbital()
    {
        RemoveProcedural<CinemachineOrbitalFollow>();
        RemoveProcedural<CinemachineThirdPersonFollow>();

        // Añade ThirdPerson y luego Orbital para forzar el bugfix
        var tps = cinemachineCamera.gameObject.AddComponent<CinemachineThirdPersonFollow>();

        Destroy(tps); // Lo quitamos de inmediato

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
