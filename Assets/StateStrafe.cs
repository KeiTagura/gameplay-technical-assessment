using Animancer;
using Kei;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateStrafe : StateBase
{

    [SerializeField]
    private DirectionalAnimationSet4 _DirectionalStrafeWalkMovement;

    [SerializeField]
    private MixerTransition2D _Strafe;

    [SerializeField, Range(0,1)]
    private float _MovementLevel;

    private LinearMixerState _movementMixer;
    [SerializeField]
    private ClipTransition _Idle;
    public ClipTransition Idle => _Idle;


    public override bool CanEnterState => true;

    private ITransition CurrentMixer
        {
        get
            {
                Vector2 direction = new Vector2(Character.Parameters.MovementDirection.x, Character.Parameters.MovementDirection.z);


                if (direction == Vector2.zero)
                    {
                        return _Idle;
                    }
                Debug.Log(direction);
                return _DirectionalStrafeWalkMovement.GetClip(direction);

            }
        }
    private void Awake()
        {
            _movementMixer = new LinearMixerState();
            
          //  Character.Animancer.Play(CurrentMixer);
          Character.Animancer.Play(_Strafe);
        }
    private void OnEnable()
        {
            Character.Animancer.Play(_Strafe);
        }

    private void FixedUpdate()
        {
      //  Character.Animancer.Play(_Strafe);

        
            if (Character.CheckMotionState())
                return;
              //  Character.Movement.UpdateSpeedControl();
                _Strafe.State.Parameter = new Vector2(Character.Parameters.MovementDirection.x, Character.Parameters.MovementDirection.z);
                //UpdateRotation();
        

        }
    private void UpdateRotation()
        {
            //Character.Movement.TurnTowards((Target.position - transform.position).normalized, 400);
        }
    }
