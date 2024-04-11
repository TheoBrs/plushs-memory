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
        //Dialogue

        //If player confirm the change
        _alliesManager._actualAlly = 1;
    }

    public void ChangeAllyToBoon()
    {
        //Dialogue

        //If player confirm the change
        _alliesManager._actualAlly = 2;
    }

    public void ChangeAllyToPatto()
    {
        //Dialogue

        //If player confirm the change
        _alliesManager._actualAlly = 3;
    }
}
