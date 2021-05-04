using UnityEngine;
public class ATM : MonoBehaviour
{
    public ATM_UI ToRelay;
    public SpriteRenderer Renderer;
    public float OutlineSize;
    private void OnMouseExit() => Renderer.material.SetFloat("OffsetAmount", 0);
    private void OnMouseEnter() => Renderer.material.SetFloat("OffsetAmount", OutlineSize);
    private void OnMouseDown()
    {
        if (Player.S.IsInInteractionRange(transform.position))
            ToRelay.ToggleATM_UI();
    }
    private void Update()
    {
        if (ToRelay.IsOpen)
            if (!Player.S.IsInInteractionRange(transform.position))
                ToRelay.ToggleATM_UI();
    }
}