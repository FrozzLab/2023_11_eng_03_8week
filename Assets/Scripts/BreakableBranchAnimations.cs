using UnityEngine;

namespace DefaultNamespace
{
	public class BreakableBranchAnimations : MonoBehaviour
	{
		public Animator animator;

		public void FadeOutStart()
		{
			animator.SetBool("FadeOut", true);
		}
		
		public void FadeOutEnd()
		{
			animator.SetBool("FadeOut", false);
		}
		
		public void FadeInStart()
		{
			animator.SetBool("FadeIn", true);
		}
		
		public void FadeInEnd()
		{
			animator.SetBool("FadeIn", false);
		}
	}
}