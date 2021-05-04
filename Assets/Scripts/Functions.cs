using UnityEngine;
public static class Functions
{
    private static readonly Vector2[] DirectionVectors = { Vector2.up, Vector2.down, Vector2.left, Vector2.right, Vector2.zero };
    public static void SetAnimatorLayer(Animator Anim, string LayerName)
    {
        for (int i = 0; i < Anim.layerCount; i++)
            Anim.SetLayerWeight(i, 0);
        Anim.SetLayerWeight(Anim.GetLayerIndex(LayerName), 1);
    }
    public static void SetXYForAnimator(Animator Anim, Vector2 Dir)
    {
        Anim.SetFloat("X", Dir.x);
        Anim.SetFloat("Y", Dir.y);
    }
    public static bool IsMoving(Vector2 vector) { return vector.x != 0 || vector.y != 0; }
    public static Vector2 ClampVector2(Range<Vector2> Range, Vector2 ToClamp)
    {
        float X = Mathf.Clamp(ToClamp.x, Range.Min.x, Range.Max.x);
        float Y = Mathf.Clamp(ToClamp.y, Range.Min.y, Range.Max.y);
        return new Vector2(X, Y);
    }
    public static void ClampVector2(Range<Vector2> Range,ref Vector2 ToClamp) => ToClamp = ClampVector2(Range, ToClamp);
    public static Vector2 BlendVector2(Vector2 ToBlend)
    {
        float SmallestDist = 10;
        Vector2 StoredDir = new Vector2(0, 0);
        foreach (Vector2 V in DirectionVectors)
        {
            float dist = Vector2.Distance(ToBlend, V);
            if (SmallestDist > dist)
            {
                SmallestDist = dist;
                StoredDir = V;
            }
        }
        return StoredDir;
    }
    public static void BlendVector2(ref Vector2 Vector) => Vector = BlendVector2(Vector);
}