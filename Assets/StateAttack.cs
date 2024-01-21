using Animancer;
using Animancer.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StateAttack : StateBase
    {
    

        [SerializeField, DegreesPerSecond] private float _TurnSpeed = 400;
        [SerializeField] private UnityEvent _SetWeaponOwner;// See the Read Me.
        [SerializeField] private UnityEvent _OnStart;// See the Read Me.
        [SerializeField] private UnityEvent _OnEnd;// See the Read Me.
        [SerializeField] private ClipTransition[] _Animations;

        private int _CurrentAnimationIndex = int.MaxValue;
        private ClipTransition _CurrentAnimation;


        private void Awake()
        {
            _SetWeaponOwner.Invoke();
        }


        public override bool CanEnterState => true;

        public bool canCombo = false;
        /// <summary>
        /// Start at the beginning of the sequence by default, but if the previous attack hasn't faded out yet then
        /// perform the next attack instead.
        /// </summary>
        private void OnEnable()
        {
            if (_CurrentAnimationIndex >= _Animations.Length - 1 ||
                _Animations[_CurrentAnimationIndex].State.Weight == 0)
            {
                _CurrentAnimationIndex = 0;
            }
            else
            {
                _CurrentAnimationIndex++;
            }
            if(_CurrentAnimationIndex > 1)
                {
                    if(!canCombo) _CurrentAnimationIndex = 0;
                }
            _CurrentAnimation = _Animations[_CurrentAnimationIndex];
            Character.Animancer.Play(_CurrentAnimation);
            Character.Parameters.ForwardSpeed = 0;
            _OnStart.Invoke();
        }


        private void OnDisable()
        {
            _OnEnd.Invoke();
        }


        public override bool FullMovementControl => false;

        private void FixedUpdate()
        {
            if (Character.CheckMotionState())
                return;
            Character.Movement.TurnTowards((Target.position - transform.position).normalized, _TurnSpeed);
        }


        // Use the End Event time to determine when this state is alowed to exit.

        // We cannot simply have this method return false and set the End Event to call Character.CheckMotionState
        // because it uses TrySetState (instead of ForceSetState) which would be prevented if this returned false.

        // And we cannot have this method return true because that would allow other actions like jumping in the
        // middle of an attack.

        public override bool CanExitState
            => _CurrentAnimation.State.NormalizedTime >= _CurrentAnimation.State.Events.NormalizedEndTime;
    

    }