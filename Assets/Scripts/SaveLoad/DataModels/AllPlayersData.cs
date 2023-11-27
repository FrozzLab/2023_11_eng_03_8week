using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class AllPlayersData
{
	public DateTime Updated {get; set;} = DateTime.Now;
	public List<OnePlayerData> All {get; set;} = new();

	public void UpdateData(string playerName)
	{
		var oldData = All.FirstOrDefault(e=>e.PlayerName.Equals(playerName));
		if(oldData != null) All.Remove(oldData);

		var newData = OnePlayerData.GetDataFromScene(playerName);
		All.Add(newData);
	}

	public OnePlayerData GetDataForPlayer(string playerName) => All.FirstOrDefault(e=>e.PlayerName.Equals(playerName));

	public LevelName? GetLevelNameForPlayer(string playerName) => All.FirstOrDefault(e=>e.PlayerName.Equals(playerName))?.LevelName;
}
