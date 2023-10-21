using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] private Image loadImage;
    [SerializeField] private float maxDelayIn, maxDelayOut;

    private bool isFadingIn, isFadingOut, isExitingGame;
    private float currentDelay;
    private Color currentLoadImageColor;
    private string targetSceneName;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        AudioManager.Play(MusicName.MainMenuTheme);
    }

    private void Update()
    {
        if (isFadingIn)
        {
            currentDelay += Time.deltaTime;

            if (currentDelay >= maxDelayIn)
            {
                currentLoadImageColor.a = 1;
                loadImage.color = currentLoadImageColor;

                if (isExitingGame)
                {
                    Application.Quit();
                }
                else
                {
                    SceneManager.LoadSceneAsync(targetSceneName);
                }
                
                currentDelay = 0;
                
                isFadingIn = false;
                isFadingOut = true;
            }
            else
            {
                float alpha = currentDelay / maxDelayIn;
                currentLoadImageColor.a = Mathf.Clamp01(alpha);
                loadImage.color = currentLoadImageColor;
            }
        }
        else if (isFadingOut)
        {
            currentDelay += Time.deltaTime;

            if (currentDelay >= maxDelayOut)
            {
                isFadingOut = false;
            }
            else
            {
                float alpha = currentDelay / maxDelayOut;
                currentLoadImageColor.a = 1 - Mathf.Clamp01(alpha);
                loadImage.color = currentLoadImageColor;
            }
        } 
    }
    
    public void LoadScene(string sceneName)
    {
        if (!isFadingIn && !isFadingOut)
        {
            targetSceneName = sceneName;
            currentDelay = 0;
            
            currentLoadImageColor = loadImage.color;
            currentLoadImageColor.a = 0;
            loadImage.color = currentLoadImageColor;

            isFadingIn = true;
        }
    }

    public void ExitGame()
    {
        if (!isFadingIn && !isFadingOut)
        {
            currentDelay = 0;
            
            currentLoadImageColor = loadImage.color;
            currentLoadImageColor.a = 0;
            loadImage.color = currentLoadImageColor;

            isFadingIn = true;
            isExitingGame = true;
        }
    }
}
