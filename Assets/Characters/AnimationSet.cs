

using UnityEngine;
using Animancer;

namespace Kei.Data
{
	[CreateAssetMenu(menuName ="SO/AnimationSet", fileName ="AnimationSet", order = 0)]
	public class AnimationSet : ScriptableObject
	{
		[Header("Locomotion")]
		[SerializeField]
		private ClipTransition _Idle;
		public ClipTransition Idle => _Idle;

		[SerializeField]
		private ClipTransition _Walk;
		public ClipTransition Walk => _Walk;


		[Header("Strafe Animations Walking")]
		[SerializeField]
		private DirectionalAnimationSet4 _DirectionalStrafeWalk;
		public DirectionalAnimationSet4 DirectionalStrafeWalk => _DirectionalStrafeWalk;


	}
}