using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
	private bool _paused = false;

	[SerializeField] private GameObject pauseView;
	[SerializeField] private GameObject pauseImage;
	[SerializeField] private GameObject resumeImage;

    // Update is called once per frame
    void Update()
    {		    
	    Debug.Log("hehe");

	    if (Input.GetKeyDown(KeyCode.Escape))
	    {
		    if (_paused)
		    {
			    Resume();
		    }
		    else
		    {
			    Pause();
		    }
	    }
    }

    private void Pause()
    {
	    pauseView.SetActive(true);
	    Time.timeScale = 0f;
	    _paused = true;
	    pauseImage.SetActive(false);
	    resumeImage.SetActive(true);
    }

    private void Resume()
    {
	    pauseView.SetActive(false);
	    Time.timeScale = 1f;
	    _paused = false;
	    pauseImage.SetActive(true);
	    resumeImage.SetActive(false);
    }
}
