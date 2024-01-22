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
            private  Vector2 _Movement;
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

            }

    }
