
using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Character : MonoBehaviour
    {

        [SerializeField]
        private AnimancerComponent _Animancer;
        public AnimancerComponent Animancer => _Animancer;

        [SerializeField]
        private CharacterLocomotion _Movement;
        public CharacterLocomotion Movement => _Movement;

        [SerializeField]
        private ParamsCharacter _Parameters;
        public ParamsCharacter Parameters => _Parameters;

        [SerializeField]
        private StateBase.StateMachine _StateMachine;
        public StateBase.StateMachine StateMachine => _StateMachine;

        private void Awake()
            {
                StateMachine.InitializeAfterDeserialize();
            }

        /// <summary>
        /// Check if this <see cref="Character"/> should enter the Idle, Locomotion, or Airborne state depending on
        /// whether it is grounded and the movement input from the <see cref="Brain"/>.
        /// </summary>
        /// <remarks>
        /// We could add some null checks to this method to support characters that don't have all the standard states,
        /// such as a character that can't move or a flying character that never lands.
        /// </remarks>
        public bool CheckMotionState()
            {
            StateBase state;
           // if (Movement.IsGrounded)
            //    {
                state = Parameters.MovementDirection == default
                    ? StateMachine.DefaultState
                    : StateMachine.Locomotion;
              //  }
           // else
          //      {
          //      state = StateMachine.Airborne;
          //      }

            return
                state != StateMachine.CurrentState &&
                StateMachine.TryResetState(state);
            }

        /************************************************************************************************************************/
    }