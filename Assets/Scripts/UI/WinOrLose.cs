using UnityEngine;
using UnityEngine.UI;

public class WinOrLose : MonoBehaviour
{

    public bool isWin = false;
    [SerializeField] Text Win;
    [SerializeField] Text Lose;

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
