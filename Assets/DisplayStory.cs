using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayStory : MonoBehaviour
{
	[SerializeField] bool followPlayer = false;
	[SerializeField] bool canAppearManyTimes = false;
	[SerializeField] Vector3 offset;
	[SerializeField] int lettersPerSecond = 20;
    [SerializeField] TextMeshProUGUI textComponent;
	string textToDisplay;
    Animator animator;
	bool _canHide = false;
	bool _isShown = false;
	bool _wasShown = false;
	Transform player;

	private void Awake() {
		Destroy(GetComponent<Image>());
		textToDisplay = textComponent.text;
		textComponent.text = "";
        animator = GetComponent<Animator>();
		player = FindFirstObjectByType<PlayerMovement>().transform;
    }

    private void OnTriggerEnter2D(Collider2D other) {
		if(!other.CompareTag("Player") || (_wasShown && !canAppearManyTimes) || _isShown) return;

		_isShown = true;
		_wasShown = true;
		animator.SetTrigger("showBackground");
		StartCoroutine(ShowLetterByLetter());
	}

	IEnumerator ShowLetterByLetter()
    {
		for (int i = 0; i < textToDisplay.Length; i++)
		{
			textComponent.text = textToDisplay[..i];
        	yield return new WaitForSeconds(1f/lettersPerSecond);
		}
		animator.SetTrigger("showSkipButton");
		_canHide = true;
    }

	private void Update() {
        if (Input.GetKeyDown(KeyCode.H) && _canHide)
        {
			_isShown = false;
			textComponent.text = "";
			animator.SetTrigger("hide");
        }
	}

	private void FixedUpdate() {
		if(followPlayer && _isShown)
		{
			transform.position = player.position + offset;
		}
	}
}
