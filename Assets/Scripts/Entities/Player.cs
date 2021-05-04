using UnityEngine;
public class Player : Character, IResetable
{
    public float InteractionDistance;
    public AudioSource WalkSource;
    private Vector2 Pos;
    public static Player S { get; private set; }
    private void OnEnable()
    {
        AddToSingleton();
        Pos = transform.position;
        S = this;
        InputManager.OnMovementDirectionChanged += Move;
        InputManager.OnMovementDirectionChanged += UpdateAnimator;
    }
    private void OnDisable()
    {
        InputManager.OnMovementDirectionChanged -= Move;
        InputManager.OnMovementDirectionChanged -= UpdateAnimator;
    }
    public bool IsInInteractionRange(Vector3 Position) { return (transform.position - Position).magnitude < InteractionDistance; }
    public void PlayWalkSound() => WalkSource.Play();
    public void ResetIt() => transform.position = Pos;
    public void AddToSingleton() => RentCollector.Resetables.Add(GetComponent<IResetable>());
}