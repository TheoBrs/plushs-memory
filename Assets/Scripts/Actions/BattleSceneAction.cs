using UnityEngine;

public class BattleSceneAction : MonoBehaviour
{
    [SerializeField] Animator animator;
    private void Start()
    {
        animator.SetTrigger("StartFadeOut");
    }
}
