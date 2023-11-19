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
			health = Health,
		};
	}

	public override void LoadData(EntitySavedData data)
	{
		base.LoadData(data);
		var concreteData = (PlayerSavedData)data;
		GetComponent<Health>().Current = concreteData.health;
	}
}

[Serializable]
public class PlayerSavedData : EntitySavedData
{
	public int health;
}