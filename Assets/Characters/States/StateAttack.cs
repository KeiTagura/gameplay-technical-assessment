using UnityEngine;


namespace Kei
{
	public sealed class StateAttack : BaseState
	{
		[SerializeField]
		private AttackTransition _CurrentAnimation;


        public override bool CanEnterState => true;// Character.CharacterMovement.IsGrounded;

        public override bool CanExitState => _CurrentAnimation.State.NormalizedTime >= _CurrentAnimation.State.Events.NormalizedEndTime;


        private void OnEnable()
        {

            Character.Animancer.Play(_CurrentAnimation);
        }

    


        private void OnAnimationEnded()
        {
            Character.StateMachine.TrySetDefaultState();
        }
    }
}