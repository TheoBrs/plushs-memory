using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image itemIcon;
    Item item;

    public void AddItem(Item newItem)
    { 
        item = newItem; 
        itemIcon.sprite = item.icon;
        itemIcon.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;
        itemIcon.sprite = null;
        itemIcon.enabled = false;
    }

    public void OnRemoveItem()
    {
        InventoryManager.Instance.Remove(item);
    }

    public void UseItem()
    {
        if(item != null)
        {
            item.Use();
        }
    }
}
