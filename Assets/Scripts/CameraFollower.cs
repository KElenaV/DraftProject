using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _defaultDistance;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _orbitSpeed;

    private PlayerActions _actions;
    private float _mouseDeltaX;
    private bool _isRotate = false;
    private float _insignificantOffset = 0.5f;

    private void Awake()
    {
        _actions = new PlayerActions();
        _actions.Controls.MouseRotation.performed += ctx => _mouseDeltaX = ctx.ReadValue<float>();
    }

    private void OnEnable()
    {
        _actions.Enable();
    }

    private void OnDisable()
    {
        _actions.Disable();
    }

    private void LateUpdate()
    {
        if (Mouse.current.rightButton.isPressed)
        {
            RotateAround(_mouseDeltaX, _rotationSpeed);
            _isRotate = true;
        }
        else if(!Mouse.current.rightButton.isPressed && _isRotate)
        {
            RotateToDefaultPosition();
        }
        
        transform.LookAt(_target);
    }

    private void RotateToDefaultPosition()
    {
        Vector3 localPosition = transform.InverseTransformPoint(_defaultDistance.position);

        float xDirection = localPosition.x < 0 ? 1 : -1;

        RotateAround(xDirection, _orbitSpeed);

        if (Vector3.Distance(transform.position, _defaultDistance.position) < _insignificantOffset & _isRotate)
        {
            transform.position = _defaultDistance.position;
            _isRotate = false;
        }
    }

    private void RotateAround(float angle, float speed)
    {
        transform.RotateAround(_target.position, Vector3.up, angle * speed * Time.deltaTime);
    }
}
