using System;
using UnityEngine;

public class BaseEntityData : MonoBehaviour
{
	public string Id {get; private set;}

	protected virtual string GetId() => $"{tag}";
	
	private void Start() 
	{
		Id = GetId();
	}

	public virtual BaseEntitySavedData GetData()
	{
		return new BaseEntitySavedData()
		{
			Id = Id,
		};
	}

	public virtual void LoadData(BaseEntitySavedData data)
	{
		if(!data.Id.Equals(Id))
			throw new SaveLoadException($"Entity Id: {Id}, data Id: {data.Id}. You're trying to load the wrong entity!!");
	}
}

[Serializable]
public class BaseEntitySavedData
{
	public string Id {get;  set;}
}