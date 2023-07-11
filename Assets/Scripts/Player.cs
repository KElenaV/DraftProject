using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private AnyStateAnimator _animator;

    private PlayerActions _actions;
    private Vector2 _movementInput;
    private float _mouseDeltaX;

    private void Awake()
    {
        _actions = new PlayerActions();
        _actions.Controls.Move.performed += ctx => _movementInput = ctx.ReadValue<Vector2>();
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

    private void Update()
    {
        Movement();
        
        Animate();

        Rotate();
    }

    private void Movement()
    {
        Vector3 movement = transform.right * _movementInput.x + transform.forward * _movementInput.y;

        _characterController.Move(movement * _movementSpeed * Time.deltaTime);
    }

    private void Rotate()
    {
        if (!Mouse.current.rightButton.isPressed)
        {
            float angle = _mouseDeltaX * _rotationSpeed * Time.deltaTime;

            transform.Rotate(Vector3.up * angle);
        }
    }

    private void Animate()
    {
        if (_movementInput.x != 0 || _movementInput.y != 0)
            _animator.PlayAnimation(_animator.WalkID);
        else
            _animator.PlayAnimation(_animator.IdleID);
    }
}
