using UnityEngine;

public class AnimatorIds
{
    public int WalkID => Animator.StringToHash("Walk");
    public int JumpID => Animator.StringToHash("Jump");
    public int RunID => Animator.StringToHash("Run");
    public int IdleID => Animator.StringToHash("Idle");
    public int VerticalID => Animator.StringToHash("Vertical");
    public int HorizontalID => Animator.StringToHash("Horizontal");
}
