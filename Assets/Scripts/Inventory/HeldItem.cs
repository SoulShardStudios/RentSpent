using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HeldItem : MonoBehaviour
{
    [HideInInspector] public PointerItem PItem;
    [SerializeField] protected Image ItemImage, BGImage;
    [SerializeField] protected TextMeshProUGUI ItemAmountText;
    [SerializeField] protected Vector3 Offset;
    public void Update()
    {
        transform.position = Input.mousePosition + Offset;
        if (PItem != null)
        {
            ItemImage.sprite = PItem.Item?.DisplayImage;
            ItemImage.color = PItem.Item == null ? Color.clear : Color.white;
            BGImage.color = ItemImage.color;
            ItemAmountText.text = PItem.Amount.ToString();
            ItemAmountText.color = PItem.Amount < 1 ? Color.clear : Color.white;
        }
    }
}