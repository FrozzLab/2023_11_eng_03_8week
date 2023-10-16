using UnityEngine;

public class ButtonLevelInteract : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        LevelManager.instance.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        LevelManager.instance.ExitGame();
    }
}
