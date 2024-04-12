using UnityEngine;
using UnityEngine.UI;

public class WinOrLose : MonoBehaviour
{

    bool isWin;
    [SerializeField] Text Win;
    [SerializeField] Text Lose;

    private void Awake()
    {
        isWin = IsWin.IsWinBool;
    }

    void Start()
    {
        if (isWin == true) 
        {
            Win.gameObject.SetActive(true);
            Lose.gameObject.SetActive(false);
        }
        else
        {
            Win.gameObject.SetActive(false);
            Lose.gameObject.SetActive(true);
        }
    }
}
