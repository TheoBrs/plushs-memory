using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    #region Singleton
    public static InventoryManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangeCallback;

    public int space = 12;

    public List<Item> items = new List<Item>();

    public bool Add(Item item)
    {
        if(!item.isDefaultItem)
        {
            if(items.Count >= space)
            {
                Debug.Log("Not enough space");
                return false ;
            }
            items.Add(item);

            if(onItemChangeCallback != null)
            {
                onItemChangeCallback.Invoke();
            }
        }
        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);

        if (onItemChangeCallback != null)
        {
            onItemChangeCallback.Invoke();
        }
    }

}
