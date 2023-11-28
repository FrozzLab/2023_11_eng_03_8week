using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathZone : MonoBehaviour
{
	[SerializeField] int damage = 100;
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
		{
			var otherObject = other.gameObject;
			var otherHealth = otherObject.GetComponent<Health>();
			
			otherHealth.Damage(damage);
		}
	}
}
