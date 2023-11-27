using System;
using UnityEngine;

namespace Menu
{
    public class ButtonLevelInteract : MonoBehaviour
    {
        public void LoadScene(string sceneName)
        {
            LevelManager.LoadScene(sceneName);
        }

        public void ExitGame()
        {
            LevelManager.ExitGame();
        }
    }
}
