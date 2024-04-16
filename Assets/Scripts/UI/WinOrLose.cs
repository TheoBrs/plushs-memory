using UnityEngine;
using UnityEngine.UI;

public class WinOrLose : MonoBehaviour
{
    bool isWin;
    [SerializeField] GameObject Win;
    [SerializeField] GameObject Lose;

    private void Awake()
    {
        isWin = IsWin.IsWinBool;
    }

    void Start()
    {
        Win.SetActive(isWin);
        Lose.SetActive(!isWin);
    }
}
