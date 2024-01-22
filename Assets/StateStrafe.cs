using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateStrafe : StateBase
{
    [SerializeField]
    private MixerTransition2D _Strafe;

    [SerializeField, Range(0,1)]
    private float _MovementLevel;

    private LinearMixerState _movementMixer;


    public override bool CanEnterState => true;

    private void Awake()
        {
            _movementMixer = new LinearMixerState();
            
            Character.Animancer.Play(_Strafe);
        }
    private void OnEnable()
        {
            Character.Animancer.Play(_Strafe);
        }

    private void FixedUpdate()
        {
            if (Character.CheckMotionState())
                return;
                Character.Movement.UpdateSpeedControl();
                _Strafe.State.Parameter = new Vector3(Character.Parameters.MovementDirection.x, Character.Parameters.MovementDirection.z);
                UpdateRotation();

        }
    private void UpdateRotation()
        {
            //Character.Movement.TurnTowards((Target.position - transform.position).normalized, 400);
        }
    }
