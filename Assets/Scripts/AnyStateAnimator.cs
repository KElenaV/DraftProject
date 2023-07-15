using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnyStateAnimator : MonoBehaviour
{
    private AnimatorIds _animatorIds;
    private Animator _animator;
    private int _currentAnimationID;

    public Animator Animator { get => _animator; private set => _animator = value; }

    private void Awake()
    {
        _animatorIds = new AnimatorIds();
        Animator = GetComponent<Animator>();
        _currentAnimationID = _animatorIds.WalkID;
    }

    public void TryPlayAnimation(int animationID)
    {
        if(animationID != _currentAnimationID)
        {
            Animator.SetBool(_currentAnimationID, false);
            Animator.SetBool(animationID, true);
            _currentAnimationID = animationID;
        }
    }
}
