using UnityEngine;
using Animancer;

namespace Kei
{
    public sealed class StateRespawn : BaseState
    {
        [SerializeField] private ClipTransition _MainAnimation;

        private void Awake()
        {
           _MainAnimation.Events.OnEnd = Character.StateMachine.ForceSetDefaultState;
           //Character.StateMachine.TrySetState(this);
        }

        public override void OnEnterState()
        {
            Character.Animancer.Play(_MainAnimation);
        }

        public override bool CanExitState => false;

        public override void OnExitState()
        {
           
        }
    }
}
