using UnityEngine;
using UnityEngine.EventSystems;
public class UIDrag : MonoBehaviour, IDragHandler
{
    [SerializeField] private RectTransform Transform;
    [SerializeField] private Canvas Canvas;
    public int Scale;
    public void OnDrag(PointerEventData eventData) => Transform.anchoredPosition += eventData.delta / Canvas.scaleFactor / Scale;
}