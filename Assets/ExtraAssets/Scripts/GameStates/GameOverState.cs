using ExtraAssets.Scripts.Infrastructure.StateMachine;
using ExtraAssets.Scripts.Map;
using ExtraAssets.Scripts.Map.Provider;
using ExtraAssets.Scripts.Player;
using ExtraAssets.Scripts.Services.CrossInput;
using ExtraAssets.Scripts.UI;
using UnityEngine;
using UnityEngine.Rendering;

namespace ExtraAssets.Scripts.GameStates
{
    public class GameOverState : IState
    {
        private readonly IMenuPanels _menuPanels;
        private readonly IMapProvider _mapProvider;
        private readonly PlayerFactory _playerFactory;
        private readonly IInputService _input;
        private readonly StateMachine _gameStateMachine;

        public GameOverState(
            IMenuPanels menuPanels, 
            IMapProvider mapProvider,
            PlayerFactory playerFactory, 
            IInputService input,
            StateMachine gameStateMachine)
        {
            _menuPanels = menuPanels;
            _mapProvider = mapProvider;
            _playerFactory = playerFactory;
            _input = input;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            Debug.LogWarning("Enter to GameOverState");

            ShowMenu();
            
            _input.OnMovingStarted += GoToGameLoopState;
        }

        public void Exit()
        {
            HideMenu();
            RebuildLevel();
            ReloadPlayer();

            _input.OnMovingStarted -= GoToGameLoopState;
        }

        private void GoToGameLoopState() => 
            _gameStateMachine.ChangeState<GameLoopState>();

        private void ShowMenu() => 
            _menuPanels.Show<GameOverMenu>();

        private void HideMenu() => 
            _menuPanels.Hide<GameOverMenu>();

        private void RebuildLevel()
        {
            _mapProvider.Destruct();
            _mapProvider.Create();
        }

        private void ReloadPlayer() => 
            _playerFactory.Reload();
    }
}