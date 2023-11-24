using System.Collections;
using UnityEngine;

namespace Menu
{
    public class ButtonHoverSizeChanger : MonoBehaviour
    {
        [SerializeField] public float scaleFactor;
        [SerializeField] public float timeToScale;

        private Vector3 _originalScale;

        private void Awake()
        {
            _originalScale = transform.localScale;
        }

        public void IncreaseElementSizeOnHover()
        {
            StopAllCoroutines();
            StartCoroutine(ScaleElementOverTime(transform.localScale, _originalScale * scaleFactor));
        }

        public void RestoreSizeOnExit()
        {
            StopAllCoroutines();
            StartCoroutine(ScaleElementOverTime(transform.localScale, _originalScale));
        }

        private IEnumerator ScaleElementOverTime(Vector3 currentScale, Vector3 targetScale)
        {
            float elapsedTime = 0;

            while (elapsedTime < timeToScale)
            {
                transform.localScale = Vector3.Lerp(currentScale, targetScale, elapsedTime / timeToScale);
                elapsedTime += Time.deltaTime;

                yield return null;
            }

            transform.localScale = targetScale;
        }
    }
}
