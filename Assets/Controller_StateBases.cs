using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer.FSM;
using Animancer.Units;
using static Animancer.Validate;
using UnityEngine.InputSystem;

public sealed class Controller_StateBases : MonoBehaviour
    {
        /************************************************************************************************************************/

        [SerializeField] private Character _Character;
        [SerializeField] private StateAirborne _Jump;
        [SerializeField] private StateBase _Attack;
    
        Vector2 moveVect;
        [SerializeField]
        [Seconds(Rule = Value.IsNotNegative)]
        private float _AttackInputTimeOut = 0.5f;
        [Space(20)]
        [SerializeField] SO_Logging logger;

        private StateMachine<StateBase>.InputBuffer _InputBuffer;

        

        private void Awake()
        {
            _InputBuffer = new StateMachine<StateBase>.InputBuffer(_Character.StateMachine);
        }

     

        private void Update()
        {
           // UpdateMovement();
            UpdateActions();
        }


        private void UpdateMovement()
        {
            Vector3 input = new Vector3(moveVect.x, 0, moveVect.y);
            logger?.Log(input);
            if (input == default)
            {
                _Character.Parameters.MovementDirection = default;
                return;
            }

            // Get the camera's forward and right vectors and flatten them onto the XZ plane.
            Transform camera = Camera.main.transform;

            Vector3 forward = camera.forward;
            forward.y = 0;
            forward.Normalize();

            Vector3 right = camera.right;
            right.y = 0;
            right.Normalize();

            // Build the movement vector by multiplying the input by those axes.
            _Character.Parameters.MovementDirection =
                right * input.x +
                forward * input.y;
        }
    public void UpdateMovement_InputAction(InputAction.CallbackContext _context)
        {
            if (_context.performed)
                {
                    moveVect = _context.ReadValue<Vector2>();
                }
            else if (_context.canceled)
                {
                    moveVect = default;
                }
        }

        /************************************************************************************************************************/
        bool attack = false;
        public void AttackAction() => attack = true;
          
        private void UpdateActions()
        {
          /*  // Jump gets priority for better platforming.
            if (ExampleInput.SpaceDown)
            {
                _Jump.TryJump();
            }
            else if (ExampleInput.SpaceUp)
            {
                _Jump.CancelJump();
            }*/
            if (attack)
                {
                    logger.Log("has attacked");
                    _InputBuffer.Buffer(_Attack, _AttackInputTimeOut);
                }
            attack = false;
            _InputBuffer.Update();
        }

        /************************************************************************************************************************/
    }