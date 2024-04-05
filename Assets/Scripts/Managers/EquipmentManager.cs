using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager Instance;

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

    Equipment[] _currentEquipment;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    InventoryManager _inventory;

    private void Start()
    {
        _inventory = InventoryManager.Instance;

        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        _currentEquipment = new Equipment[numSlots];
    }

    public void Equip(Equipment newEquipment)
    {
        int slotIndex = (int)newEquipment.equipSlot;

        Equipment oldEquipment = null;

        if (_currentEquipment[slotIndex] != null )
        {
            oldEquipment = _currentEquipment[slotIndex];
            _inventory.Add(oldEquipment);
        }

        if(onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newEquipment, oldEquipment);
        }

        _currentEquipment[slotIndex] = newEquipment;
    }

    public void Unequip(int slotIndex)
    {
        if (_currentEquipment[slotIndex] != null )
        {
            Equipment oldEquipment  = _currentEquipment[slotIndex];
            _inventory.Add(oldEquipment);

            _currentEquipment[slotIndex] = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldEquipment);
            }
        }
    }

    public void UnequipAll()
    {
        for(int i = 0; i < _currentEquipment.Length; i++)
        {
            Unequip(i);
        }
    }

}
