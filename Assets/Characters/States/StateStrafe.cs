using UnityEngine;
using Animancer;
using Kei.Data;

namespace Kei
{
    public sealed class StateStrafe : BaseState
    {

        [SerializeField]
        private DirectionalAnimationSet4 _DirectionalStrafeWalkMovement;



        [SerializeField]
        private float _MagnitudeThreshold = 0.98f;

        private AnimationSet _LocalCurrentAnimationSet => Character.CurrentAnimationSet;

        public override bool CanEnterState => Character.CharacterMovement.LockOnTarget;

        private ITransition CurrentMixer
        {
            get
            {
                Vector2 direction = Character.MovementDirection;

            
                    if (direction == Vector2.zero)
                    {
                        return Character.CurrentAnimationSet.Idle;
                    }

                    return _DirectionalStrafeWalkMovement.GetClip(direction);
                
            }
        }


        public override void OnEnterState()
        {
            base.OnEnterState();

            if (_LocalCurrentAnimationSet != Character.CurrentAnimationSet)
            {
                ChangeAnimationSet();
            }

            Character.Animancer.Play(CurrentMixer);
        }

        private void Awake()
        {
            ChangeAnimationSet();
        }

        private void Update()
        {
            Character.Animancer.Play(CurrentMixer);
        }

        private void UpdateAnimationSet()
        {
            ChangeAnimationSet();
        }

        private void ChangeAnimationSet()
        {
            _DirectionalStrafeWalkMovement = Character.CurrentAnimationSet.DirectionalStrafeWalk;

         
        }

        private void OnDestroy()
        {
        }

        private void OnLeftFootStep()
        {
           // Character.CharacterMovement.FootStepper.FootstepWalkIndex(1);
        }

        private void OnRightFootStep()
        {
            //Character.CharacterMovement.FootStepper.FootstepWalkIndex(0);
        }

    }
}
