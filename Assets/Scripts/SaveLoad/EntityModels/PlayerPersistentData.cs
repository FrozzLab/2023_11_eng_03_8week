using System;

public class PlayerPersistentData : BaseEntityData
{
	public int Health { get => GetComponent<Health>().Current; }

	public override BaseEntitySavedData GetData()
	{
		//if(Health <= 0) throw new SaveLoadException("cannot save dead player progress!!");
		return new PlayerPersistentSavedData 
		{
			Id = Id,
			Health = Health,
		};
	}

	public override void LoadData(BaseEntitySavedData data)
	{
		base.LoadData(data);
		var concreteData = (PlayerPersistentSavedData)data;
		GetComponent<Health>().Current = concreteData.Health;
	}
}

[Serializable]
public class PlayerPersistentSavedData : BaseEntitySavedData
{
	public int Health {get;  set;}
}