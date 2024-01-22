using UnityEngine;
using Animancer.FSM;
using Kei.Data;
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


       // [SerializeField]
       // private CharacterState _Respawn;
      //  public CharacterState Respawn => _Respawn;

        [SerializeField]
        private BaseState _Idle;
        public BaseState Idle => _Idle;

       // [SerializeField]
        //private CharacterState _Jump;
       // public CharacterState Jump => _Jump;

        public Vector2 MovementDirection => _CharacterMovement.MovementDirection;


        [SerializeField]
        private AnimationSet _DefaultAnimationSet;
        public AnimationSet DefaultAnimationSet => _DefaultAnimationSet;

        [SerializeField]
        private AnimationSet _CurrentAnimationSet;
        public AnimationSet CurrentAnimationSet => _CurrentAnimationSet;            


        [SerializeField]
        public StateMachine<BaseState>.WithDefault StateMachine;
        //= new StateMachine<CharacterState>.WithDefault();

        protected virtual void Awake()
        {

            _CurrentAnimationSet = _DefaultAnimationSet;

            StateMachine.InitializeAfterDeserialize();
            //StateMachine.DefaultState = _Respawn; // Start in the Respawn state if there is one.
            StateMachine.DefaultState = _Idle; // But the actual default state is the Idle
        }

        private void Start()
        {
        }

        private void Update()
        {
        }


        public void SetCurrentAnimationSet(AnimationSet theAnimationSet)
        {
           _CurrentAnimationSet = theAnimationSet;
        }

        public void SetDefaultAnimationSet()
        {
            _CurrentAnimationSet = _DefaultAnimationSet;
        }

    }
}
