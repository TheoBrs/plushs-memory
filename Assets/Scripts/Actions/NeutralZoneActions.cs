using UnityEngine;

public class NeutralZoneActions : MonoBehaviour
{
    private AlliesManager _alliesManager;

    void Start()
    {
        _alliesManager = AlliesManager.Instance;
    }

    public void ChangeAllyToKero()
    {
        _alliesManager._actualAlly = 1;
    }

    public void ChangeAllyToBoon()
    {
        _alliesManager._actualAlly = 2;
    }

    public void ChangeAllyToPatto()
    {
        _alliesManager._actualAlly = 3;
    }
}
