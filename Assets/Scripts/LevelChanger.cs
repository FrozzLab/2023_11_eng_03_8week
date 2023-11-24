using UnityEngine;

public class LevelChanger : MonoBehaviour
{
	public string nextLevel;
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (nextLevel == null)
		{
			return;
		}
		
		if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			LevelManager.instance.LoadScene(nextLevel);
		}
	}
}
