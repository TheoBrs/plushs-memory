using UnityEngine;

public class WinOrLose : MonoBehaviour
{
    bool isWin;
    [SerializeField] GameObject win;
    [SerializeField] GameObject lose;
    [SerializeField] GameObject continueText;
    [SerializeField] GameObject restartText;
    [SerializeField] GameObject menuButton;

    private void Awake()
    {
        isWin = IsWin.IsWinBool;
    }

    void Start()
    {
        win.SetActive(isWin);
        continueText.SetActive(isWin);

        lose.SetActive(!isWin);
        restartText.SetActive(!isWin);
        menuButton.SetActive(!isWin);
    }
}
