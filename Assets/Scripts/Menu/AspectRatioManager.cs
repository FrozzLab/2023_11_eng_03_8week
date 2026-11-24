using System.Collections.Generic;
using UnityEngine;

namespace Menu
{
	public class AspectRatioManager : MonoBehaviour
	{
		public static AspectRatioManager Instance;

		private readonly List<AspectDependent>  _aspectDependentElements = new();
		
		private void Awake()
		{
			if (Instance == null)
			{
				Instance = this;
			}
			else
			{
				Destroy(this);
			}
		}

		private void Start()
		{
			UpdateElementPositions();
		}

		private static void UpdateElementPositions()
		{
			if (Instance == null || Instance._aspectDependentElements.Count == 0)
			{
				return;
			}

			var currentRatio = (float)Screen.width / Screen.height;

			foreach (var element in Instance._aspectDependentElements)
			{
				var referenceResolution = element.ReferenceResolution;
				var referenceRatio = referenceResolution.x / referenceResolution.y;
				var referencePosition = element.ReferencePosition;
				
				Debug.Log("1: " + referenceResolution);
				Debug.Log("2: " + referenceRatio);
				Debug.Log("3: " + referencePosition);
				Debug.Log("4: " + currentRatio);
				Debug.Log("5: " + referencePosition * (currentRatio / referenceRatio));
				Debug.Log("6: " + element.Transform.anchoredPosition);
				
				element.Transform.anchoredPosition = referencePosition * (currentRatio / referenceRatio);
			}
		}
		
		public static void RegisterElement(AspectDependent dependentElement)
		{
			Instance._aspectDependentElements.Add(dependentElement);
		}

		public static void UnregisterElement(AspectDependent dependentElement)
		{
			Instance._aspectDependentElements.Remove(dependentElement);
		}
	}
}