using UnityEngine;
using Kei.Data;

namespace Kei
{
	public sealed class StateAttack : BaseState
	{
		[SerializeField]
		private AttackTransition _CurrentAnimation;

		private AnimationSet _LocalCurrentAnimationSet => Character.CurrentAnimationSet;

        public override bool CanEnterState => true;// Character.CharacterMovement.IsGrounded;

        public override bool CanExitState => _CurrentAnimation.State.NormalizedTime >= _CurrentAnimation.State.Events.NormalizedEndTime;


        private void OnEnable()
        {
            if (_LocalCurrentAnimationSet != Character.CurrentAnimationSet)
            {
                ChangeAnimationSet();
            }

            Character.Animancer.Play(_CurrentAnimation);
        }

    

        private void UpdateAnimationSet()
        {
            ChangeAnimationSet();
        }

        private void ChangeAnimationSet()
        {
           // _CurrentAnimation.CopyFrom(Character.CurrentAnimationSet.QuickAttack);
            _CurrentAnimation.Events.OnEnd = OnAnimationEnded;
        }

        private void OnAnimationEnded()
        {
            Character.StateMachine.TrySetDefaultState();
        }
    }
}