using System;
using UnityEngine;

public class PlayerData : EntityData
{
	protected float[] LocalPosition => new float[3] { transform.localPosition.x, transform.localPosition.y, transform.localPosition.z };

	public override BaseEntitySavedData GetData()
	{
		return new PlayerSavedData 
		{
			Id = Id,
			Position = Position,
			LocalPosition = LocalPosition,
		};
	}

	public override void LoadData(BaseEntitySavedData data)
	{
		base.LoadData(data);
		var concreteData = (PlayerSavedData)data;
		transform.localPosition = new Vector3(concreteData.LocalPosition[0], concreteData.LocalPosition[1], concreteData.LocalPosition[2]);
	}
	
}

[Serializable]
public class PlayerSavedData : EntitySavedData
{
	public float[] LocalPosition {get;  set;}
}