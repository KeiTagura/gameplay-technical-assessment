using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;
using Animancer.Units;
using UnityEngine.Events;

namespace Kei
    {
    public class StateHeavyAttack : BaseState
        {


        [SerializeField, DegreesPerSecond] private float _TurnSpeed = 400;
        [SerializeField] private ClipTransition chargeAttack;
        [SerializeField] private ClipTransition heavyAttack;
        [SerializeField] private ClipTransition idle;
        [SerializeField] private UnityEvent _OnEnterState;// See the Read Me.
        [SerializeField] private UnityEvent _OnExitState;// See the Read Me.


        public bool combo = false;
        public override bool CanEnterState => true;
        private void Awake()
            {
            chargeAttack.Events.OnEnd = HeavyAttack;
            heavyAttack.Events.OnEnd = Idle;
            }
        void Idle()
            {
            Character.Animancer.Play(idle);
            }
        void HeavyAttack()
            {
            Character.Animancer.Play(heavyAttack);
            }
        private void OnEnable()
            {
            Character.Animancer.Play(chargeAttack);
            _OnEnterState.Invoke();
            }

        private void OnDisable()
            {
            _OnExitState.Invoke();
            combo = false;
            OnEnable();
            }


        public override bool CanExitState
         => chargeAttack.State.NormalizedTime >= chargeAttack.State.Events.NormalizedEndTime;

        }
    }