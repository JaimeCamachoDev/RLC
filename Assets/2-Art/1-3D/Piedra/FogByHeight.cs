using UnityEngine;

public class FogByHeight : MonoBehaviour
{
    [Header("Jugador a seguir")]
    public Transform player;

    [Header("Puntos de color por altura")]
    public Gradient fogGradient;

    [Header("Altura mínima y máxima")]
    public float minHeight = 0f;
    public float maxHeight = 300f;

    void Update()
    {
        if (player == null) return;

        float t = Mathf.InverseLerp(minHeight, maxHeight, player.position.y);
        RenderSettings.fogColor = fogGradient.Evaluate(t);
    }
}
