using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	private static LevelManager instance;
	public static LevelName CurrentLevel => LevelNameExtensions.GetLevelName(SceneManager.GetActiveScene().name);
	
	[SerializeField] UnityEvent levelChangeStartedEvent;
	[SerializeField] UnityEvent<LevelName> levelChangedEvent;
	[SerializeField] UnityEvent gameStartedEvent;

	private void Start()
	{
		gameStartedEvent.Invoke();
	}

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
	
	public static void LoadScene(LevelName sceneName) => instance.StartCoroutine(instance.LoadingScene(sceneName));
	public static void LoadScene(string sceneName) => LoadScene(LevelNameExtensions.GetLevelName(sceneName));

	private IEnumerator LoadingScene(LevelName sceneName){

		levelChangeStartedEvent.Invoke();
		var asyncLoadLevel = SceneManager.LoadSceneAsync(sceneName.ToSceneName());
		while (!asyncLoadLevel.isDone)
			yield return null;

		levelChangedEvent.Invoke(sceneName);
	}

	public static void ExitGame() => Application.Quit();
}