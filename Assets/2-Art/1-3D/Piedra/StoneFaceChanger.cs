using UnityEngine;

[ExecuteInEditMode]
public class StoneFaceBlinker : MonoBehaviour
{
    [Header("Configura las caras (0 siempre es Pestañeo)")]
    public Sprite[] faceSprites;

    [Header("Sprite Renderer")]
    public SpriteRenderer targetRenderer;

    [Header("Expresión Actual (sin contar parpadeo)")]
    [Range(0, 10)]
    public int currentExpression = 1;  // Por defecto no pestañeo

    [Header("Parpadeo Automático")]
    public bool enableBlink = true;
    public Vector2 blinkInterval = new Vector2(3f, 7f);  // Rango entre parpadeos (segundos)

    private float blinkTimer = 0f;
    private float nextBlinkTime = 0f;
    private bool isBlinking = false;
    private int previousExpression = -1;

    void Start()
    {
        ScheduleNextBlink();
        UpdateFace();
    }

    void Update()
    {
        if (!Application.isPlaying)  // Editor Mode
        {
            UpdateFace();
            return;
        }

        if (enableBlink)
        {
            HandleBlinking();
        }
    }

    void HandleBlinking()
    {
        blinkTimer += Time.deltaTime;

        if (!isBlinking && blinkTimer >= nextBlinkTime)
        {
            StartBlink();
        }
    }

    void StartBlink()
    {
        isBlinking = true;
        previousExpression = currentExpression;
        targetRenderer.sprite = faceSprites[0];  // Pestañeo (índice 0)
        Invoke(nameof(EndBlink), 0.1f);  // Duración corta del parpadeo
    }

    void EndBlink()
    {
        isBlinking = false;
        targetRenderer.sprite = faceSprites[previousExpression];
        blinkTimer = 0f;
        ScheduleNextBlink();
    }

    void ScheduleNextBlink()
    {
        nextBlinkTime = Random.Range(blinkInterval.x, blinkInterval.y);
    }

    void UpdateFace()
    {
        if (targetRenderer == null || faceSprites.Length == 0)
            return;

        if (!isBlinking && currentExpression >= 0 && currentExpression < faceSprites.Length)
        {
            targetRenderer.sprite = faceSprites[currentExpression];
        }
    }

    // Editor: actualiza sprite al cambiar valor en inspector
    void OnValidate()
    {
        if (targetRenderer != null && faceSprites.Length > 0)
        {
            UpdateFace();
        }
    }
}
