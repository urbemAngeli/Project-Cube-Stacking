using ExtraAssets.Scripts.Infrastructure.StateMachine;
using ExtraAssets.Scripts.Services.CrossInput;
using ExtraAssets.Scripts.UI;
using UnityEngine;

namespace ExtraAssets.Scripts.GameStates
{
    public class WaitingState : IState
    {
        private readonly StateMachine _gameStateMachine;
        private readonly IMenuPanels _menuPanels;
        private readonly IInputService _input;

        public WaitingState(StateMachine gameStateMachine, IMenuPanels menuPanels, IInputService input)
        {
            _gameStateMachine = gameStateMachine;
            _menuPanels = menuPanels;
            _input = input;
        }
        
        public void Enter()
        {
            Debug.LogWarning("Enter to WaitingState");

            ShowMenu();

            _input.OnMovingStarted += GoToGameLoopState;
        }

        public void Exit()
        {
            HideMenu();
            
            _input.OnMovingStarted -= GoToGameLoopState;
        }

        private void GoToGameLoopState() => 
            _gameStateMachine.ChangeState<GameLoopState>();

        private void ShowMenu() => 
            _menuPanels.Show<WaitingMenu>();

        private void HideMenu() => 
            _menuPanels.Hide<WaitingMenu>();
        
        
    }
}