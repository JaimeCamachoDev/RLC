using Unity.Cinemachine;
using Unity.Cinemachine.TargetTracking;
using UnityEngine;

public class CinemachineSetter : MonoBehaviour
{
    [SerializeField] CinemachineCamera cinemachineCamera;
    [SerializeField] CinemachineOrbitalFollow cinemachineOrbitalFollow;
    [SerializeField] TrackerSettings bindingMode;
    [SerializeField] Vector3 targetOffset;
    [SerializeField] float radius;
    [SerializeField] InputAxis horizontalAxis;
    [SerializeField] InputAxis verticalAxis;
    [SerializeField] InputAxis radialAxis;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    [ContextMenu("Setter")]
    void SetValues()
    {
        cinemachineOrbitalFollow.TrackerSettings = bindingMode;
        cinemachineOrbitalFollow.TargetOffset = targetOffset;
        cinemachineOrbitalFollow.Radius = radius;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
