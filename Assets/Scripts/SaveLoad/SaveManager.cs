using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Linq;

public class SaveLoad : MonoBehaviour
{
	public string PlayerName { get; set; } = "unknown"; //todo: set this when player types their name/chose existing name
	private static readonly string _path = "./Assets/Scripts/SaveLoad/data.json"; //Application.persistentDataPath + "/data";
	private AllPlayersData _data = new();
	private bool _loadProgressOnLevelChanged = false;

	private void Update() //tmp for testing. todo: we should use event like -> onSceneChanged, onCheckpointEntered
	{
		if(Input.GetKeyDown(KeyCode.K)) Save();
		if(Input.GetKeyDown(KeyCode.L)) Load();
	}

	public void Save()
	{
		Debug.Log($"Saving player progress to {_path}");
		//uncomment to store data in a save location. Leave commented for testing (readable data)

		//BinaryFormatter formatter = new BinaryFormatter();
		//FileStream stream = new FileStream(_path,FileMode.Create);

		_data.UpdateData(PlayerName);

		//formatter.Serialize(stream, data);
		string json = JsonConvert.SerializeObject(_data, Formatting.Indented, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });

		File.WriteAllText(_path, json);
	}

	public void Load()
	{
		Debug.Log($"Loading player progress from {_path}");

		if (!File.Exists(_path))
		{
			Debug.LogWarning("Saved progress file not found!!");
			return;
		}


		string json = File.ReadAllText(_path);
		_data = JsonConvert.DeserializeObject<AllPlayersData>(json, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
		var levelName = _data.GetLevelNameForPlayer(PlayerName);

		if(levelName != null)
		{
			_loadProgressOnLevelChanged = true;
			LevelManager.LoadScene((LevelName)levelName);
		}
		else
		{
			Debug.LogWarning("Player progress not found!!");
			LevelManager.LoadScene(LevelManager.CurrentLevel);
		}
	}

	public void DeleteProgress()
	{
		if (File.Exists(_path)) File.Delete(_path);
	}

	public IEnumerable<ProgressOverviewDto> GetProgressOverview() //used by frontend in main menus to display player list to choose save
	{
		//todo: add more details if required
		return _data.All.Select(e => new ProgressOverviewDto() { PlayerName = e.PlayerName });
	}

	public void LevelChangedEventHandler() //always load the progress only after scene is reloaded
    {
		if(!_loadProgressOnLevelChanged) return;
		_loadProgressOnLevelChanged = false;
		var playerData = _data.GetDataForPlayer(PlayerName);

		OnePlayerData.LoadPlayerData(playerData);
		Debug.Log($"Progress loaded");
    }
}
