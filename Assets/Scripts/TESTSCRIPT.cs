using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTSCRIPT : MonoBehaviour
{
    [SerializeField] Item _item;

    public void AddItem()
    {
        Inventory.Instance.Add(_item);
    }
}
