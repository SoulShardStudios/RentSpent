using UnityEngine;
public class InventoryUIManager : MonoBehaviour
{
    public GameObject UISlot;
    public Inventory MainInventory = new Inventory(new PointerItem[0]);
    public PItemList StartingItemList;
    public HeldItem HeldItem;
    public int Size;
    public static InventoryUIManager S { get; private set; }
    private void Awake()
    {
        S = this;
        Initialize();
    }
    public void Initialize()
    {
        if (StartingItemList != null)
        {
            PointerItem[] PItems = new PointerItem[Size];
            for (int i = 0; i < Size; i++)
            {
                if (i < StartingItemList.List.Length)
                    PItems[i] = StartingItemList.List[i];
                else
                    PItems[i] = new PointerItem();
            }
            MainInventory = new Inventory(PItems);
            for (int i = 0; i < Size; i++)
            {
                GameObject G = Instantiate(UISlot, gameObject.transform);
                G.GetComponent<UISlot>().Initialize(MainInventory.Slots[i]);
            }
        }
        else
            Debug.LogError("set the list of starting items for this level on the InventoryUIManager!");
    }
}