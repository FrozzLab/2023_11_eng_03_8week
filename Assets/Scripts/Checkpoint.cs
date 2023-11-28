using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
	private SaveLoad _saveManager;
	private bool _firstTimeEnter = true;

	private void Awake()
	{
		_saveManager = GetComponent<SaveLoad>();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (_firstTimeEnter)
		{
			_saveManager.Save();
			_firstTimeEnter = false;
		}
	}
}
