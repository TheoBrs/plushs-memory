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
        Debug.Log("Change ally to Kero");
    }

    public void ChangeAllyToBoon()
    {
        _alliesManager._actualAlly = 2;
        Debug.Log("Change ally to Boon");
    }

    public void ChangeAllyToPatto()
    {
        _alliesManager._actualAlly = 3;
        Debug.Log("Change ally to Patto");
    }
}
