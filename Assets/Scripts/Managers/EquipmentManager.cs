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

    private Equipment[] _currentEquipment;
    private InventoryManager _inventory;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

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

        if (_currentEquipment[slotIndex] != null)
        {
            oldEquipment = _currentEquipment[slotIndex];
            _inventory.Add(oldEquipment);
        }

        onEquipmentChanged?.Invoke(newEquipment, oldEquipment);

        _currentEquipment[slotIndex] = newEquipment;
    }

    public void UnEquip(int slotIndex)
    {
        if (_currentEquipment[slotIndex] != null)
        {
            Equipment oldEquipment = _currentEquipment[slotIndex];
            _inventory.Add(oldEquipment);

            _currentEquipment[slotIndex] = null;

            onEquipmentChanged?.Invoke(null, oldEquipment);
        }
    }

    public void UnEquipAll()
    {
        for (int i = 0; i < _currentEquipment.Length; i++)
        {
            UnEquip(i);
        }
    }
}
