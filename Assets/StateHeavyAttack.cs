using Animancer;
using Animancer.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StateHeavyAttack : StateBase
    {


    [SerializeField, DegreesPerSecond] private float _TurnSpeed = 400;
    [SerializeField] private ClipTransition chargeAttack;
    [SerializeField] private ClipTransition heavyAttack;
    [SerializeField] private UnityEvent _OnEnterState;// See the Read Me.
    [SerializeField] private UnityEvent _OnExitState;// See the Read Me.


    public bool combo = false;
    public override bool CanEnterState => true;
    public override bool FullMovementControl => false;
    private void Awake()
        {
            chargeAttack.Events.OnEnd = HeavyAttack;
            heavyAttack.Events.OnEnd =  Character.StateMachine.ForceSetDefaultState;
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
    private void FixedUpdate()
        {
            if (Character.CheckMotionState())
                return;

          //  Character.Movement.TurnTowards((Target.position - transform.position).normalized, _TurnSpeed);
        }

    public override bool CanExitState
     => chargeAttack.State.NormalizedTime >= chargeAttack.State.Events.NormalizedEndTime;

    }
