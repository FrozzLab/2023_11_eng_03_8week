using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class OnePlayerData
{
	public string PlayerName {get; set;}
	public LevelName LevelName {get; set;}
	public List<BaseEntitySavedData> Entities {get; set;} = new();

	public OnePlayerData(string playerName) 
	{ 
		PlayerName = playerName; 
		LevelName = LevelManager.CurrentLevel; 
	}

	public static OnePlayerData GetDataFromScene(string playerName)
	{
		var data = new OnePlayerData(playerName);

		var entities = Object.FindObjectsOfType<BaseEntityData>();
		foreach (var entity in entities)
			data.Entities.Add(entity.GetData());

		return data;
	}

	public static void LoadPlayerData(OnePlayerData data)
	{
		var entities = Object.FindObjectsOfType<BaseEntityData>();
		foreach (var entity in entities)
		{
			var entityData = data.Entities.SingleOrDefault(e => e.Id.Equals(entity.Id));
			if(entityData == null) //in case we added a new entity to the scene or change position or tag of existing entity
			{
				Debug.LogWarning($"Progress of entity with Id: {entity.Id} couldn't be found in a saved file");
				continue; //depside modification of a scene, loadable progress should still be loaded, so we don't throw
			}

			entity.LoadData(entityData);
		}
	}
}
