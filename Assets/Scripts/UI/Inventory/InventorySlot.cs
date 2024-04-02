using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image itemIcon;
    // public Image slotIcon;   |   Indication visuelle pour savoir si l'objet est une armure ou une arme
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
        Inventory.Instance.Remove(item);
    }

    public void UseItem()
    {
        if(item != null)
        {
            item.Use();
        }
    }
}
