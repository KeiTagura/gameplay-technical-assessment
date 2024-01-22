using Animancer.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Kei
    {
        public class InputAnimancer : MonoBehaviour
            {
                [SerializeField]
                private Character _Character;
                public ref Character Character => ref _Character;


        [SerializeField] private StateHeavyAttack attackHeavy;
        private  Vector2 _Movement;


        private StateMachine<BaseState>.InputBuffer _InputBuffer;


        private void Awake() 
            {
                ClearHits();
                _InputBuffer = new StateMachine<BaseState>.InputBuffer(Character.StateMachine);
            }



        public void OnMovement(InputAction.CallbackContext context)
                {
                    _Movement = context.ReadValue<Vector2>();
                }
            private void Update()
                {
                    Character.CharacterMovement.MovementDirection = new Vector2(
                                      _Movement.x,
                                      _Movement.y);

                }


        bool attack = false;
        public void AttackAction() => attack = true;



        public bool IsClear = true;
        public Collider leftCollider;
        public Collider rightCollider;
        bool leftHit = false;
        bool rightHit = false;


        public void ClearHits()
            {
            return;
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
            if (leftHit && rightHit)
                {
                ClearHits();
                attackHeavy.combo = true;
                }
            }
        private void UpdateActions()
            {
            if (attackHeavy.combo)
                {
                _InputBuffer.Buffer(attackHeavy, 0);
                }

            if (attack)
                {
                   // _InputBuffer.Buffer(_Attack, _AttackInputTimeOut);
                }

            _InputBuffer.Update();
            attack = false;
            }

        }

    }
