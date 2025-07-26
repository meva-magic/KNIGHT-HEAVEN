using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private VariableJoystick _joystick;

    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _acceleration = 5f;  // How quickly player speeds up
    [SerializeField] private float _deceleration = 8f;  // How quickly player slows down

    private Vector3 _currentVelocity;
    public bool isRunning;

    private void FixedUpdate()
    {
        // Get input direction (normalized to prevent diagonal speed boost)
        Vector3 inputDirection = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical).normalized;
        Vector3 targetVelocity = inputDirection * _moveSpeed;

        // Smoothly accelerate/decelerate towards target velocity
        if (inputDirection.magnitude > 0.1f)  // If there's input, accelerate
        {
            _currentVelocity = Vector3.Lerp(
                _currentVelocity,
                targetVelocity,
                _acceleration * Time.fixedDeltaTime
            );
            isRunning = true;
        }
        else  // If no input, decelerate
        {
            _currentVelocity = Vector3.Lerp(
                _currentVelocity,
                Vector3.zero,
                _deceleration * Time.fixedDeltaTime
            );
            isRunning = false;
        }

        // Apply velocity (preserve Y for gravity)
        _currentVelocity.y = _rigidbody.linearVelocity.y;
        _rigidbody.linearVelocity = _currentVelocity;
    }
}
