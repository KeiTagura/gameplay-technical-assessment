using UnityEngine;
using Animancer;


namespace Kei
{
    public sealed class StateStrafe : BaseState
    {





        [SerializeField]
        private MixerTransition2D _Strafe;

        [SerializeField, Range(0, 1)]
        private float _MovementLevel;








        [SerializeField]
        private DirectionalAnimationSet4 _DirectionalStrafeWalkMovement;




        public override bool CanEnterState => true;






        private void Awake()
            {
                Character.Animancer.Play(_Strafe);
            }
      

      

        private void FixedUpdate()
            {
            _Strafe.State.Parameter = new Vector2(Character.MovementDirection.x, Character.MovementDirection.y);

        }



        private void OnDestroy()
        {
        }


    }
}
