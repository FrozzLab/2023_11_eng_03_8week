using UnityEngine;

public class LevelManagerAnimations : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void FadeIn() => animator.SetTrigger("fadeIn");
    public void FadeOut() => animator.SetTrigger("fadeOut");
    public void ShowYouDiedScreen() => animator.SetTrigger("playerDied");
}
