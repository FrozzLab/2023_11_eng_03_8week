using System;
using UnityEngine;

public class EnemyData : EntityData
{
	public int Health { get => GetComponent<Health>().Current; }
	public string AnimationName { get => GetComponentInChildren<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name; }

	public override BaseEntitySavedData GetData()
	{
		return new EnemySavedData 
		{
			Id = Id,
			Position = Position,
			Health = Health,
			AnimationName = AnimationName,
		};
	}

	public override void LoadData(BaseEntitySavedData data)
	{
		base.LoadData(data);
		var concreteData = (EnemySavedData)data;
		GetComponent<Health>().Current = concreteData.Health;
		GetComponentInChildren<Animator>().Play(concreteData.AnimationName);
	}
}

[Serializable]
public class EnemySavedData : EntitySavedData
{
	public int Health {get;  set;}
	public string AnimationName {get;  set;}
}