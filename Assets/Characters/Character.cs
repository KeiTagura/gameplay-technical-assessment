using UnityEngine;
using Animancer.FSM;
using System;
using Animancer;

namespace Kei
{
    [DefaultExecutionOrder(-1100)]
    public class Character : MonoBehaviour
        {
        [SerializeField]
        private AnimancerComponent _Animancer;
        public AnimancerComponent Animancer => _Animancer;

        [SerializeField]
        private CharacterMovement _CharacterMovement;
        public CharacterMovement CharacterMovement => _CharacterMovement;


        [SerializeField]
        private BaseState _default;
        public BaseState Default => _default;

        public Vector2 MovementDirection => _CharacterMovement.MovementDirection;



        [SerializeField]
        public StateMachine<BaseState>.WithDefault StateMachine;

        protected virtual void Awake()
        {


            StateMachine.InitializeAfterDeserialize();
            StateMachine.DefaultState = _default;
        }




    }
}
