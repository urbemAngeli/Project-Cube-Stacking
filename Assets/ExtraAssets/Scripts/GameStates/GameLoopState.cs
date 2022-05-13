using ExtraAssets.Scripts.Environment;
using ExtraAssets.Scripts.Infrastructure.Processors.FixedTick;
using ExtraAssets.Scripts.Infrastructure.StateMachine;
using ExtraAssets.Scripts.Map.Section;
using ExtraAssets.Scripts.Player;
using UnityEngine;

namespace ExtraAssets.Scripts.GameStates
{
    public class GameLoopState : IState
    {
        private readonly SectionControl _sectionControl;
        private readonly IFixedTickProcessor _fixedTickProcessor;
        private readonly PlayerFactory _playerFactory;
        private readonly StateMachine _gameMachineState;
        private readonly WarpEffect _warpEffect;

        public GameLoopState(
            SectionControl sectionControl,
            IFixedTickProcessor fixedTickProcessor,
            PlayerFactory playerFactory,
            StateMachine gameMachineState, 
            WarpEffect warpEffect)
        {
            _sectionControl = sectionControl;
            _fixedTickProcessor = fixedTickProcessor;
            _playerFactory = playerFactory;
            _gameMachineState = gameMachineState;
            _warpEffect = warpEffect;
        }

        public void Enter()
        {
            Debug.LogWarning("Enter to GameLoopState");
            
            _sectionControl.StartMoving();
            _warpEffect.Show();
            
            InitializePlayer(_playerFactory.Player);
        }

        public void Exit()
        {
            _sectionControl.StopMoving();
            _warpEffect.Hide();
            DisposePlayer(_playerFactory.Player);
        }

        private void InitializePlayer(PlayerControl player)
        {
            player.Initialize();
            _fixedTickProcessor.Add(player);

            player.OnFail += GoToGameOverState;
        }
        
        private void DisposePlayer(PlayerControl player)
        {
            player.Dispose();
            _fixedTickProcessor.Remove(player);
        }

        private void GoToGameOverState() => 
            _gameMachineState.ChangeState<GameOverState>();
    }
}