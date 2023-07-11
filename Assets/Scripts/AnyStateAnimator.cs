using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnyStateAnimator : MonoBehaviour
{
    private Animator _animator;
    private int _currentAnimationID;

    public int WalkID => Animator.StringToHash("Walk");
    public int JumpID => Animator.StringToHash("Jump");
    public int RunID => Animator.StringToHash("Run");
    public int IdleID => Animator.StringToHash("Idle");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _currentAnimationID = IdleID;
    }

    public void PlayAnimation(int animation)
    {
        _animator.SetBool(_currentAnimationID, false);
        _animator.SetBool(animation, true);
        _currentAnimationID = animation;
    }
}
