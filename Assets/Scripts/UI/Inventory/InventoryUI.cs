using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public InventoryManager inventory;
    public InventorySlot[] slots;

    private void Start()
    {
        inventory = InventoryManager.Instance;
        inventory.onItemChangeCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    private void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.Items.Count)
            {
                slots[i].AddItem(inventory.Items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
