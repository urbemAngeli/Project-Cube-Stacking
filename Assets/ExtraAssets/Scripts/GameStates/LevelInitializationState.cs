using ExtraAssets.Scripts.Infrastructure.StateMachine;
using ExtraAssets.Scripts.Map;
using ExtraAssets.Scripts.Map.Provider;
using ExtraAssets.Scripts.Player;
using UnityEngine;

namespace ExtraAssets.Scripts.GameStates
{
    public class LevelInitializationState : IState
    {
        private readonly StateMachine _gameStateMachine;
        private readonly IMapProvider _mapProvider;
        private readonly PlayerFactory _playerFactory;

        public LevelInitializationState(StateMachine gameStateMachine, IMapProvider mapProvider, PlayerFactory playerFactory)
        {
            _gameStateMachine = gameStateMachine;
            _mapProvider = mapProvider;
            _playerFactory = playerFactory;
        }

        public void Enter()
        {
            Debug.LogWarning("Enter to LevelInitializationState");
            
            InitializeWorld();

            GoToWaitingState();
        }

        private void GoToWaitingState() => 
            _gameStateMachine.ChangeState<WaitingState>();

        private void InitializeWorld()
        {
            CreateMap();
            CreatePlayer();
        }

        public void Exit()
        {
            
        }

        private void CreateMap()
        {
            _mapProvider.Create();
        }

        private void CreatePlayer()
        {
            _playerFactory.Create();
        }
    }
}