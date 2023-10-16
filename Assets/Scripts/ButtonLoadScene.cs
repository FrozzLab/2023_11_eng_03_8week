using UnityEngine;

public class ButtonLoadScene : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        LevelManager.instance.LoadScene(sceneName);
    }
}
