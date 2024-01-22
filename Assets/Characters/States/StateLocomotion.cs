using UnityEngine;
using Animancer;


namespace Kei
{
    public class StateLocomotion : BaseState
    {
        [SerializeField]
        private ClipTransition _Idle;
        public ClipTransition Idle => _Idle;

        [SerializeField]
        private ClipTransition _Walk;
        public ClipTransition Walk => _Walk;



        public ClipTransition CurrentAnimation
        {
            get
            {
                if (Character.MovementDirection != Vector2.zero)
                {
                   return _Walk;
                }

                return _Idle;
            }
        }




        public override bool CanEnterState => !Character.CharacterMovement.LockOnTarget;
        public override void OnEnterState()
        {
            base.OnEnterState();


            Character.Animancer.Play(CurrentAnimation);
        }

        protected virtual void Update()
        {
            Character.Animancer.Play(CurrentAnimation);
        }


        private void OnDestroy()
        {
        }


    }
}
