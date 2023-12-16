using UnityEngine;

public class SpikeSpawner : MonoBehaviour
{
	[SerializeField] float spawnDelay = 1f;
	[SerializeField] FallingSpike fallingSpikePrefab;

	void Start()
	{
		InvokeRepeating(nameof(SpawnSpike), 0f, spawnDelay);
	}

	void SpawnSpike()
	{
		Instantiate(fallingSpikePrefab, transform.position, Quaternion.identity);
	}
}
