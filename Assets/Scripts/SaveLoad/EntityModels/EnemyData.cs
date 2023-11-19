using System;
using UnityEngine;

public class EnemyData : EntityData
{
	public int Health { get => GetComponent<Health>().Current; }
	public string AnimationName { get => GetComponentInChildren<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name; }

	public override EntitySavedData GetData()
	{
		var data = base.GetData();

		return new EnemySavedData 
		{
			Id = data.Id,
			Position = data.Position,
			health = Health,
			animationName = AnimationName,
		};
	}

	public override void LoadData(EntitySavedData data)
	{
		base.LoadData(data);
		var concreteData = (EnemySavedData)data;
		GetComponent<Health>().Current = concreteData.health;
		GetComponentInChildren<Animator>().Play(concreteData.animationName);
	}
}

[Serializable]
public class EnemySavedData : EntitySavedData
{
	public int health;
	public string animationName;
}