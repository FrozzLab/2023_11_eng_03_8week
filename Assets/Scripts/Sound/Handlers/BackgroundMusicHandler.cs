using UnityEngine.SceneManagement;

public partial class AudioManager
{
    public static void PlayMusicOnGameStart()
    {
        var sceneName = SceneManager.GetActiveScene().name;
        var name = GetMusicName(sceneName);
        instance.Play(name);
    }

    public static void ChangeMusicOnLevelChange(string sceneName)
    {
        var name = GetMusicName(sceneName);

        if (instance._currentMusic != name)
        {
            instance.StopAll();
            instance.Play(name);
        }
    }

    private static MusicName GetMusicName(string sceneName)
    {
        return sceneName switch
        {
            "Credits" or "Settings" or "Main Menu" => MusicName.MainMenuTheme,
            "Level 1" => MusicName.Level1Theme,
            "Level 2" => MusicName.Level2Theme,
            "Level 3" => MusicName.Level3Theme,
            _ => MusicName.MainMenuTheme,
        };
    }
}