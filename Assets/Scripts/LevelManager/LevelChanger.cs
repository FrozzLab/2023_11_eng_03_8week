using UnityEngine;

public class LevelChanger : MonoBehaviour
{
	public LevelName nextLevel;
	
	private void OnTriggerEnter2D(Collider2D other)
	{	
		if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			LevelManager.LoadScene(nextLevel);
		}
	}
}
