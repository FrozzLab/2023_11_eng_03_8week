using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

	public void UpdateScene(string playerName)
	{
		var data = All.FirstOrDefault(e=>e.PlayerName.Equals(playerName));
		if(data != null) 
		{
			Debug.Log($"Loading progress of player {playerName}");
			OnePlayerData.LoadDataToScene(data);
		}
		else Debug.LogWarning($"Progress of player {playerName} couldn't be found in a saved file");
	}
}
