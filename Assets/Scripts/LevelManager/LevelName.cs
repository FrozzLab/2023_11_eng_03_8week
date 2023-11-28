using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

public enum LevelName
{
	[Description("Menu")]
	Menu, 
	[Description("Level 1")]
	Level1,
	[Description("Level 2")]
	Level2,
	[Description("Level 3")]
	Level3,
	[Description("Level 1 - test")]
	TestScene,
}

public static class LevelNameExtensions
{
	public static string ToSceneName(this LevelName val)
	{
		DescriptionAttribute[] attributes = (DescriptionAttribute[])val
		   .GetType()
		   .GetField(val.ToString())
		   .GetCustomAttributes(typeof(DescriptionAttribute), false);
		return attributes.Length > 0 ? attributes[0].Description : string.Empty;
	}

	public static LevelName GetLevelName(string description)
	{
		foreach(var field in typeof(LevelName).GetFields())
		{
			if (Attribute.GetCustomAttribute(field,
			typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
			{
				if (attribute.Description == description)
					return (LevelName)field.GetValue(null);
			}
			else
			{
				if (field.Name == description)
					return (LevelName)field.GetValue(null);
			}
		}

		throw new ArgumentException($"LevelName with name {description} not found");
	}
} 