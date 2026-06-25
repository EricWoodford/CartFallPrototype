using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CartController : MonoBehaviour
{
    [Header("Movement")]
    public float forwardForce = 40f;
    public float maxSpeed = 30f;
    public float steerTorque = 80f;
    public float brakeForce = 80f;

    [Header("Leaning")]
    public float leanForce = 200f;
    public float leanOffset = 0.5f;

    [Header("Speed Wobble")]
    public float wobbleThreshold = 20f;
    public float wobbleTorque = 10f;
    public float wobbleFrequency = 8f;

    private Rigidbody rb;
    private float inputSteer;
    private float inputBrake;
    private float inputLean;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0f, -0.3f, 0f);
    }

    void Update()
    {
        inputSteer = Input.GetAxis("Horizontal");          // A/D or Left/Right
        inputBrake = Input.GetKey(KeyCode.Space) ? 1f : 0f;
        inputLean = Input.GetAxis("Vertical");            // W/S or Up/Down
    }

    void FixedUpdate()
    {
        ApplyForwardGravityAssist();
        ApplySteering();
        ApplyBraking();
        ApplyLeaning();
        ApplySpeedWobble();
        ClampMaxSpeed();
    }

    void ApplyForwardGravityAssist()
    {
        // Add a small forward force along the slope to keep things moving
        Vector3 forwardOnSlope = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;
        rb.AddForce(forwardOnSlope * forwardForce, ForceMode.Acceleration);
    }

    void ApplySteering()
    {
        if (Mathf.Abs(inputSteer) > 0.01f)
        {
            rb.AddTorque(Vector3.up * inputSteer * steerTorque, ForceMode.Acceleration);
        }
    }

    void ApplyBraking()
    {
        if (inputBrake > 0.1f)
        {
            rb.velocity *= (1f - Time.fixedDeltaTime * (brakeForce / 100f));
        }
    }

    void ApplyLeaning()
    {
        if (Mathf.Abs(inputLean) < 0.01f) return;

        // Lean forward/back by applying force slightly above/below center
        Vector3 leanDir = transform.forward * inputLean;
        Vector3 forcePos = rb.worldCenterOfMass + Vector3.up * leanOffset;
        rb.AddForceAtPosition(leanDir * leanForce, forcePos, ForceMode.Acceleration);
    }

    void ApplySpeedWobble()
    {
        float speed = rb.velocity.magnitude;
        if (speed < wobbleThreshold) return;

        float wobble = Mathf.Sin(Time.time * wobbleFrequency);
        Vector3 torque = transform.up * wobble * wobbleTorque;
        rb.AddTorque(torque, ForceMode.Acceleration);
    }

    void ClampMaxSpeed()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}
