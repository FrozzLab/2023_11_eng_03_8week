using UnityEngine;

namespace Menu
{
    public class ButtonLevelInteract : MonoBehaviour
    {
	    public LevelName nextLevel;
	    
        public void LoadScene()
        {
            LevelManager.LoadScene(nextLevel);
        }

        public void ExitGame()
        {
            LevelManager.ExitGame();
        }
    }
}
