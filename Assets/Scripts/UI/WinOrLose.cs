using UnityEngine;

public class WinOrLose : MonoBehaviour
{
    private bool _isWin;
    [SerializeField] private GameObject _win;
    [SerializeField] private GameObject _lose;
    [SerializeField] private GameObject _continueText;
    [SerializeField] private GameObject _restartText;
    [SerializeField] private GameObject _menuButton;

    private void Awake()
    {
        _isWin = IsWin.IsWinBool;
    }

    void Start()
    {
        _win.SetActive(_isWin);
        _continueText.SetActive(_isWin);

        _lose.SetActive(!_isWin);
        _restartText.SetActive(!_isWin);
        _menuButton.SetActive(!_isWin);
    }
}
