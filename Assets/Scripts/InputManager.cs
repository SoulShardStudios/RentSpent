using UnityEngine;
public class InputManager : MonoBehaviour
{
    public delegate void MovementDirectionChanged(Vector2 Direction);
    public static event MovementDirectionChanged OnMovementDirectionChanged;
    public void Update()
    {
        Vector2 direction = Vector2.zero;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            direction += Vector2.up;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            direction += Vector2.down;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            direction += Vector2.left;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            direction += Vector2.right;
        OnMovementDirectionChanged?.Invoke(direction);
    }
}