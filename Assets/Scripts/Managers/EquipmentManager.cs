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



    //Inventory inventory;

    private void Start()
    {
        //inventory = Inventory.instance;

        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        _currentEquipment = new Equipment[numSlots];
    }

    public void Equip(Equipment newEquipment)
    {
        int slotIndex = (int)newEquipment.equipSlot;

        Equipment oldItem = null;

        if (_currentEquipment[slotIndex] != null )
        {
            oldItem = _currentEquipment[slotIndex];
            //inventory.Add(oldItem);
        }

        _currentEquipment[slotIndex] = newEquipment;
    }

    public void Unequip(int slotIndex)
    {
        if (_currentEquipment[slotIndex] != null )
        {
            Equipment oldItem  = _currentEquipment[slotIndex];
            //inventory.Add(oldItem);

            _currentEquipment[slotIndex] = null;
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
