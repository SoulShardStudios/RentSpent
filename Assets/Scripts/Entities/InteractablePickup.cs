using UnityEngine;
using System.Collections;
public class InteractablePickup : MonoBehaviour, IResetable
{
    public SpriteRenderer Renderer;
    public Animator Anim;
    public string AnimName;
    public float OutlineSize;
    private bool Interacted;
    public PointerItem[] PItems;
    private void Start() => AddToSingleton();
    private void OnMouseExit() => Renderer.material.SetFloat("OffsetAmount", 0);
    private void OnMouseEnter() => Renderer.material.SetFloat("OffsetAmount", OutlineSize);
    private void OnMouseDown() => StartCoroutine(InteractionAnimation());
    private IEnumerator InteractionAnimation()
    {
        if (!Interacted)
        {
            Anim.SetBool(AnimName, true);
            yield return new WaitForEndOfFrame();
            Anim.SetBool(AnimName, false);
            InventoryUIManager.S.MainInventory.AddItem(PItems[Random.Range(0,PItems.Length)]);
            Interacted = true;
        }
    }
    public void AddToSingleton() => RentCollector.Resetables.Add(GetComponent<IResetable>());
    public void ResetIt() => Interacted = false;
}