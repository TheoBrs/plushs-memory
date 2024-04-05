using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;

    InventoryManager inventory;

    InventorySlot[] slots;

    void Start()
    {
        inventory = InventoryManager.Instance;
        inventory.onItemChangeCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    void Update()
    {

    }

    void UpdateUI()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if(i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
