using Animancer.FSM;
using UnityEngine;

namespace Kei
{
    [DefaultExecutionOrder(DefaultExecutionOrder)]
    public abstract class BaseState : StateBehaviour, IOwnedState<BaseState>
    {

        public const int DefaultExecutionOrder = -1000;

        [SerializeField]
        private Character _Character;
        public Character Character => _Character;
        public StateMachine<BaseState> OwnerStateMachine => _Character.StateMachine;
    }
}
