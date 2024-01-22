using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateHitReact : StateBase
    {
        [SerializeField] private ClipTransition _Animation;
        private void Awake()
            { 
                Character.Animancer.Play(_Animation);
                _Animation.Events.OnEnd = Character.StateMachine.ForceSetDefaultState;
            }
        public void OnDamageReceived()
        {
                Character.Animancer.Play(_Animation, 0.25f, FadeMode.FromStart);
            Character.StateMachine.ForceSetState(this);
        }
   // => Character.StateMachine.ForceSetState(this);
        private void OnEnable()
            {
                Character.Animancer.Play(_Animation, 0.25f, FadeMode.FromStart);
            }
        public override bool FullMovementControl => false;
        public override bool CanExitState => true;
    }
