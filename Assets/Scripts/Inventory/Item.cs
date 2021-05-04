using UnityEngine;
[CreateAssetMenu(menuName ="ScriptableObjects/Item")]
public class Item : ScriptableObject
{
    public Sprite DisplayImage;
    public int MaxStackAmount, BaseSellPrice;
}