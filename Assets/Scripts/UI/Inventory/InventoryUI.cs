using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject container;
    public InventorySlot[] slots;

    private void Start()
    {
        InventoryManager.Instance.onItemChangeCallback += UpdateUI;

        slots = container.GetComponentsInChildren<InventorySlot>();
    }

    private void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < InventoryManager.Instance.Items.Count)
            {
                slots[i].AddItem(InventoryManager.Instance.Items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
