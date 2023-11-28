using System;
using UnityEngine;
public class LevelChanger : MonoBehaviour
{
	public GameObject player;
	public LevelName nextLevel;
	public Transform spawnPosition;
	private Transform playerBody;
	private void Start()
	{
		playerBody = player.transform.Find("Body");
	}
	private void OnTriggerEnter2D(Collider2D other)
	{	
		if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
			LevelManager.LoadScene(nextLevel);
	}
	
	public void SetPlayerPosition()
	{
		playerBody.position = spawnPosition.position;
	}
}