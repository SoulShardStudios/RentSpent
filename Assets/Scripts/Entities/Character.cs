using UnityEngine;
public class Character : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D Rigidbody;
    [SerializeField] protected Animator BodyAnimator;
    [SerializeField] protected float speed;
    public virtual void Move(Vector2 Dir) => Rigidbody.velocity = Dir * speed;
    public virtual void UpdateAnimator(Vector2 Dir)
    {
        Functions.BlendVector2(ref Dir);
        if (Dir != Vector2.zero)
            Functions.SetXYForAnimator(BodyAnimator, Dir);

        if (Functions.IsMoving(Dir))
            Functions.SetAnimatorLayer(BodyAnimator, "Walk");
        else
            Functions.SetAnimatorLayer(BodyAnimator, "Idle");
    }
}