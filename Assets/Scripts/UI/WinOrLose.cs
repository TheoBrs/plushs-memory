using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinOrLose : MonoBehaviour
{

    public bool isWin = false;
    [SerializeField] Text Win;
    [SerializeField] Text Lose;
    // Start is called before the first frame update
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
