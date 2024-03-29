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
        [SerializeField] private StateHeavyAttack attackHeavy;
    
        Vector2 moveVect;
        [SerializeField]
        [Seconds(Rule = Value.IsNotNegative)]
        private float _AttackInputTimeOut = 0.5f;
        [Space(20)]
        [SerializeField] SO_Logging logger;

        private StateMachine<StateBase>.InputBuffer _InputBuffer;

        
        private void Awake()
        {
            ClearHits();
            _InputBuffer = new StateMachine<StateBase>.InputBuffer(_Character.StateMachine);
        }

     

        private void Update()
        {
           // UpdateMovement();
            UpdateCombo();
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
        


        public bool IsClear = true;
        public Collider leftCollider;
        public Collider rightCollider;
        bool leftHit = false;
        bool rightHit = false;

        
        public void ClearHits()
        {
            leftHit = false;
            rightHit = false;
            leftCollider.gameObject.SetActive(false);
            rightCollider.gameObject.SetActive(false);

        }
        public void LeftHit()
        {
            Debug.Log("Hit Left");
            leftHit = true;
        }
    
        public void RightHit()
        {
            Debug.Log("Hit Right");
            rightHit = true;
        }

    public void LeftActive()
            {
                leftCollider.gameObject.SetActive(true);
            }
        public void LeftInactive()
            {
                leftCollider.gameObject.SetActive(false);
            }
        public void RightActive()
            {
                rightCollider.gameObject.SetActive(true);
            }
        public void RightInactive()
            {
                rightCollider.gameObject.SetActive(false);
            }

        void UpdateCombo()
            {
                if(leftHit && rightHit)
                    {
                        ClearHits();
                        attackHeavy.combo = true;
                    }
            }
        private void UpdateActions()
        {
            if(attackHeavy.combo)
                {
                    _InputBuffer.Buffer(attackHeavy, 0);
                }

            if (attack)
                {
                    _InputBuffer.Buffer(_Attack, _AttackInputTimeOut);
                }

        _InputBuffer.Update();
            attack = false;
        }

        /************************************************************************************************************************/
    }