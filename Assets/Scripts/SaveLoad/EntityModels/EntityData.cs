using System;
using UnityEngine;

public class EntityData : BaseEntityData
{
	protected float[] Position => new float[3] { transform.position.x, transform.position.y, transform.position.z };

	protected override string GetId() => $"{tag}_{transform.position.x}_{transform.position.y}_{transform.position.z}";

	public override BaseEntitySavedData GetData()
	{
		return new EntitySavedData()
		{
			Id = Id,
			Position = Position,
		};
	}

	public virtual void LoadData(EntitySavedData data)
	{
		if(!data.Id.Equals(Id))
			throw new SaveLoadException($"Entity Id: {Id}, data Id: {data.Id}. You're trying to load the wrong entity!!");

		transform.position = new Vector3(data.Position[0], data.Position[1], data.Position[2]);
	}
}

[Serializable]
public class EntitySavedData : BaseEntitySavedData
{
	public float[] Position {get;  set;}
}