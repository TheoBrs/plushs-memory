using UnityEngine;

public class MenuActions : MonoBehaviour
{
    [SerializeField] Animator animator;
    private void Start()
    {
        animator.SetTrigger("StartFadeOut");
        //AudioManager.Instance.PlayMusic("MusicTest");
    }

    public void ClickButtonSound()
    {
        AudioManager.Instance.PlaySFX("SFXTest");
    }

    public void ExitGame()
    {
        ScenesManager.Instance.ExitGame();
    }
}
