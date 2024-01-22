using Animancer;
using System;
using UnityEngine;

namespace Kei
{
    public sealed class CharacterMovement : MonoBehaviour
    {

        [SerializeField] private Vector2 _MoveDirection;
        public Vector2 MovementDirection
        {
            get => _MoveDirection;
            set
            {
                _MoveDirection.x = value.x;
                _MoveDirection.y = value.y;
            }
        }


        [SerializeField] private Transform _CameraTransform;
        [SerializeField] private float _RotationSeed = 300f;
        private float _YSpeed;

        [Header("Temp Target")]
        [SerializeField]
        private Transform TargetLock;
        public bool LockOnTarget = false;


        [SerializeField]
        private AnimancerComponent _Animancer;
        public AnimancerComponent Animancer => _Animancer;

        [SerializeField]
        private CharacterController _CharacterController;
        public CharacterController CharacterController => _CharacterController;



        private void Awake()
        {
            _CameraTransform = Camera.main.transform;
        }
        private void Update()
        {
            //_Grounded = _CharacterController.isGrounded;

            HandleMovement();

            if (LockOnTarget)
                {
                    HandleTargetLockedRotation();
                }
            else
                {
                    HandleRotation();
                }



            Vector3 moveDirection = Vector3.forward * _MoveDirection.y + Vector3.right * _MoveDirection.x;

            moveDirection = Quaternion.AngleAxis(_CameraTransform.rotation.eulerAngles.y, Vector3.up) * moveDirection;
            moveDirection.Normalize();

            // transform.position += moveDirection * 5f * Time.deltaTime;
            _YSpeed += Physics.gravity.y * Time.deltaTime;
        }

        private void HandleTargetLockedRotation()
        {
            Vector3 rotationOffset = TargetLock.transform.position - transform.position;
            rotationOffset.y = 0f;
            transform.forward += Vector3.Lerp(transform.forward, rotationOffset, Time.deltaTime * _RotationSeed);
        }

        private void HandleRotation()
        {
            Vector3 rotationOffset = _CameraTransform.TransformDirection(new Vector3(_MoveDirection.x, 0, _MoveDirection.y));
            rotationOffset.y = 0f;
            transform.forward += Vector3.Lerp(transform.forward, rotationOffset, Time.deltaTime * _RotationSeed);
        }

        private void HandleMovement()
        {

        }




        private void OnAnimatorMove()
        {
            Vector3 velocity = _Animancer.Animator.deltaPosition;
            velocity.y = _YSpeed * Time.deltaTime;
            _CharacterController.Move(velocity);
        }

        public void OnApplicationFocus(bool focus)
        {
            if (focus)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}