using UnityEngine;

public class UIActions : MonoBehaviour
{
    [SerializeField] private GameObject _ui;
    [SerializeField] private GameObject _background;

    [Header("Buttons")]
    [SerializeField] private GameObject _encyclopediaButton;
    [SerializeField] private GameObject _inventoryButton;
    [SerializeField] private GameObject _returnButton;

    [Header("Components")]
    [SerializeField] private GameObject _encyclopedia;
    [SerializeField] private GameObject _inventory;

    private void Start()
    {
        _ui.SetActive(true);
        _background.SetActive(false);

        // Hide components
        _encyclopedia.SetActive(false);
        _inventory.SetActive(false);

        // Display Buttons
        _encyclopediaButton.SetActive(true);
        _inventoryButton.SetActive(true);
        _returnButton.SetActive(true);
    }

    public void TriggerEncyclopaedia()
    {
        _background.SetActive(!_background.activeSelf);

        _encyclopedia.SetActive(!_encyclopedia.activeSelf);

        _encyclopediaButton.SetActive(!_encyclopediaButton.activeSelf);
        _inventoryButton.SetActive(!_inventoryButton.activeSelf);
        _returnButton.SetActive(!_returnButton.activeSelf);
    }

    public void TriggerInventory()
    {
        _background.SetActive(!_background.activeSelf);

        _inventory.SetActive(!_inventory.activeSelf);

        _encyclopediaButton.SetActive(!_encyclopediaButton.activeSelf);
        _inventoryButton.SetActive(!_inventoryButton.activeSelf);
        _returnButton.SetActive(!_returnButton.activeSelf);
    }

    public void TriggerHideUI()
    {
        _ui.SetActive(!_ui.activeSelf);
    }
}
