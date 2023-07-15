using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _animationSmothTime;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private AnyStateAnimator _anyStateAnimator;
    
    private AnimatorIds _animatorIds;
    private PlayerActions _actions;
    private Vector2 _movementInput;
    private Vector2 _animationVector;
    private float _mouseDeltaX;

    private void Awake()
    {
        _animatorIds = new AnimatorIds();
        _actions = new PlayerActions();
        _actions.Controls.Move.performed += ctx => _movementInput = ctx.ReadValue<Vector2>();
        _actions.Controls.MouseRotation.performed += ctx => _mouseDeltaX = ctx.ReadValue<float>();
    }

    private void OnEnable() => _actions.Enable();

    private void OnDisable() => _actions.Disable();

    private void Update()
    {
        Movement();
        
        Animate();

        Rotate();
    }

    private void Movement()
    {
        Vector3 movement = transform.right * _movementInput.x + transform.forward * _movementInput.y;

        _animationVector = Vector2.MoveTowards(_animationVector, _movementInput, _animationSmothTime * Time.deltaTime);

        _characterController.Move(movement * _movementSpeed * Time.deltaTime);

        _anyStateAnimator.Animator.SetFloat(_animatorIds.VerticalID, _animationVector.y);
        _anyStateAnimator.Animator.SetFloat(_animatorIds.HorizontalID, _animationVector.x);
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
            _anyStateAnimator.TryPlayAnimation(_animatorIds.WalkID);
        else if(_characterController.velocity == Vector3.zero && _animationVector == Vector2.zero)
            _anyStateAnimator.TryPlayAnimation(_animatorIds.IdleID);
    }
}
