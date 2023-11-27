using UnityEngine;

namespace Menu
{
    public class ButtonLevelInteract : MonoBehaviour
    {
        public void LoadScene(LevelName sceneName)
        {
            LevelManager.LoadScene(sceneName);
        }

        public void ExitGame()
        {
            LevelManager.ExitGame();
        }
    }
}
