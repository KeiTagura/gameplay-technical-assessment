using UnityEngine;
using Animancer;
using Kei.Data;

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


        private AnimationSet _LocalCurrentAnimationSet => Character.CurrentAnimationSet;

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


        private void Awake()
        {
            ChangeAnimationSet();
        }

        private void UpdateAnimationSet()
        {
            ChangeAnimationSet();
        }

        public override bool CanEnterState => !Character.CharacterMovement.LockOnTarget;
        public override void OnEnterState()
        {
            base.OnEnterState();

            if (_LocalCurrentAnimationSet != Character.CurrentAnimationSet)
            {
                ChangeAnimationSet();
            }

            Character.Animancer.Play(CurrentAnimation);
        }

        protected virtual void Update()
        {
            Character.Animancer.Play(CurrentAnimation);
        }

        private void ChangeAnimationSet()
        {
            _Idle = Character.CurrentAnimationSet.Idle;
            _Walk = Character.CurrentAnimationSet.Walk;

        }

        private void OnDestroy()
        {
        }


    }
}
