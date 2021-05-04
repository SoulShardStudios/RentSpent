using UnityEngine;
public class CameraController : MonoBehaviour
{
    public GameObject ToMoveTo;
    public Range<Vector2> CameraBounds;
    public Vector3 Offset;
    private void Update() => transform.position = (Vector3)Functions.ClampVector2(CameraBounds, ToMoveTo.transform.position) + Offset;
}