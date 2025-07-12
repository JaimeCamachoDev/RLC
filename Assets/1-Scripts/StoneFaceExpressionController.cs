using UnityEngine;

public class StoneFaceExpressionController : MonoBehaviour
{
    public StoneFaceBlinker faceBlinker;
    public Rigidbody rb;

    public float sadVelocityThreshold = 2.5f;
    public float dizzyVelocityThreshold = 8.5f;
    public float reactionDuration = 1.0f;
    public float idleTime = 3.0f;

    public int idleExpressionA = 4;
    public int idleExpressionB = 5;

    private int targetExpression = 1;
    private float reactionTimer = 0f;
    private float idleTimer = 0f;
    private Vector3 lastVelocity;
    private bool isIdle = false;

    void Awake()
    {
        if (faceBlinker == null) faceBlinker = GetComponent<StoneFaceBlinker>();
        if (rb == null) rb = GetComponent<Rigidbody>();
        lastVelocity = rb.linearVelocity;
    }

    void Update()
    {
        Vector3 vel = rb.linearVelocity;
        float speedChange = (vel - lastVelocity).magnitude;
        lastVelocity = vel;

        if (speedChange > dizzyVelocityThreshold)
        {
            SetExpression(3, reactionDuration);
            isIdle = false;
            idleTimer = 0f;
        }
        else if (speedChange > sadVelocityThreshold)
        {
            SetExpression(2, reactionDuration);
            isIdle = false;
            idleTimer = 0f;
        }
        else if (vel.magnitude < 0.15f)
        {
            idleTimer += Time.deltaTime;
            if (idleTimer >= idleTime && !isIdle)
            {
                targetExpression = Random.value > 0.5f ? idleExpressionA : idleExpressionB;
                isIdle = true;
            }
        }
        else
        {
            // Si estaba en idle y detecta movimiento, vuelve instantáneo a la normal
            if (isIdle)
            {
                targetExpression = 1;
                isIdle = false;
            }
            idleTimer = 0f;
            if (reactionTimer <= 0f)
                targetExpression = 1;
        }

        if (reactionTimer > 0f)
        {
            reactionTimer -= Time.deltaTime;
            if (reactionTimer <= 0f && !isIdle)
                targetExpression = 1;
        }

        if (faceBlinker.currentExpression != targetExpression)
            faceBlinker.currentExpression = targetExpression;
    }

    void SetExpression(int expr, float duration)
    {
        targetExpression = expr;
        reactionTimer = duration;
    }
}
