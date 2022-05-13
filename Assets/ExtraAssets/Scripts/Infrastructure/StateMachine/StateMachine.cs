using System;
using System.Collections.Generic;

namespace ExtraAssets.Scripts.Infrastructure.StateMachine
{
    public class StateMachine : IStateMachine
    {
        private Dictionary<Type, IState> _states = new Dictionary<Type, IState>();
        private IState _currentState;

        public void RegisterState<TState>(IState stateFactory) where TState : IState
        {
            Type stateType = typeof(TState);
            
            if (_states.ContainsKey(stateType))
                throw new Exception($"{stateType} state already registered!");
            
            _states.Add(stateType, stateFactory);
        }

        public void UnregisterState<TState>()
        {
            Type stateType = typeof(TState);
            
            if (_states.ContainsKey(stateType))
                _states.Remove(stateType);
        }

        public void ChangeState<TState>() where TState : IState
        {
            ExitFromCurrentState();
            EnterToNextState<TState>();
        }

        private void EnterToNextState<TState>() where TState : IState
        {
            if (_states.TryGetValue(typeof(TState), out IState foundState))
            {
                _currentState = foundState;
                _currentState.Enter();
                
                return;
            }

            throw new Exception($"State factory not found: {typeof(TState)}");
        }
        
        private void ExitFromCurrentState()
        {
            if (_currentState == null)
                return;
            
            _currentState.Exit();
            _currentState = null;
        }
    }
}