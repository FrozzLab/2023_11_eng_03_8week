using System;

public class PlayerData : EntityData
{
	public int Health { get => GetComponent<Health>().Current; }

	public override EntitySavedData GetData()
	{
		var data = base.GetData();

		return new PlayerSavedData 
		{
			Id = data.Id,
			Position = data.Position,
			Health = Health,
		};
	}

	public override void LoadData(EntitySavedData data)
	{
		base.LoadData(data);
		var concreteData = (PlayerSavedData)data;
		GetComponent<Health>().Current = concreteData.Health;
	}
}

[Serializable]
public class PlayerSavedData : EntitySavedData
{
	public int Health {get;  set;}
}