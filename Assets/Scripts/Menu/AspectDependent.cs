using UnityEngine;

namespace Menu
{
	public class AspectDependent : MonoBehaviour
	{
		public RectTransform Transform => (RectTransform)transform;
		[SerializeField] private Vector2 referenceResolution;
		public Vector2 ReferenceResolution => referenceResolution;
		[SerializeField] private Vector3 referencePosition;
		public Vector3 ReferencePosition => referencePosition;
		
		private void Start()
		{
			AspectRatioManager.RegisterElement(this);
		}

		private void OnDestroy()
		{
			AspectRatioManager.UnregisterElement(this);
		}
	}
}