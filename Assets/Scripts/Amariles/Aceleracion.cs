using UnityEngine;

public class Aceleracion : MonoBehaviour
{
    [Header("Rotación (Giroscopio)")]
    public float rotationSmoothing = 5f;

    [Header("Movimiento (Acelerómetro)")]
    public float moveSpeed = 4f;
    public float movementSmoothing = 8f;

    private float _baseGyroZ;
    private bool _calibrated = false;
    private Rigidbody2D _rb;
    private float _targetAngle; // se lee en Update, se aplica en FixedUpdate

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
            Invoke(nameof(Calibrate), 0.5f);
        }
    }

    void Calibrate()
    {
        _baseGyroZ = Input.gyro.attitude.z;
        _calibrated = true;
    }

    void Update()
    {
        if (!_calibrated) return;
        ReadRotation(); // solo lee el giroscopio y guarda el ángulo
    }

    void FixedUpdate()
    {
        if (!_calibrated) return;
        HandleRotation(); // aplica la rotación al RB
        HandleMovement();
    }

    void ReadRotation()
{
    if (!Input.gyro.enabled) return;

    float gyroZ = Input.gyro.attitude.z - _baseGyroZ;
    _targetAngle = gyroZ * 180f;

    Debug.Log($"gyroZ: {gyroZ} | targetAngle: {_targetAngle} | rb.rotation: {_rb.rotation}");
}

    void HandleRotation()
    {
        float smoothedAngle = Mathf.LerpAngle(
            _rb.rotation,
            _targetAngle,
            Time.fixedDeltaTime * rotationSmoothing
        );

        _rb.MoveRotation(smoothedAngle);
    }

    void HandleMovement()
    {
        Vector2 accel = new Vector2(
            Input.acceleration.x,
            Input.acceleration.y
        );

        Vector2 targetVelocity = accel * moveSpeed;

        _rb.velocity = Vector2.Lerp(
            _rb.velocity,
            targetVelocity,
            Time.fixedDeltaTime * movementSmoothing
        );
    }

}