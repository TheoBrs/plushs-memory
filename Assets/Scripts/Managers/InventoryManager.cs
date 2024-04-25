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

    [SerializeField] private int _space = 12;

    public List<Item> Items = new();

    public bool Add(Item item)
    {
        if (!item.isDefaultItem)
        {
            if (Items.Count >= _space)
            {
                return false;
            }
            Items.Add(item);

            onItemChangeCallback?.Invoke();
        }
        return true;
    }

    public void Remove(Item item)
    {
        Items.Remove(item);

        onItemChangeCallback?.Invoke();
    }
}
