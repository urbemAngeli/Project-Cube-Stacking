using System;
using ExtraAssets.Scripts.GameStates;
using ExtraAssets.Scripts.Infrastructure.Services;
using ExtraAssets.Scripts.Map.Section;
using ExtraAssets.Scripts.Services.CrossInput;
using ExtraAssets.Scripts.UI;
using UnityEngine;

namespace ExtraAssets.Scripts.Infrastructure
{
    public class Game : MonoBehaviour
    {
        [SerializeField]
        private SceneData _sceneData;
        
        private AllServices _services;
        private StateMachine.StateMachine _gameStateMachine;

        private void Awake()
        {
            RegisterServiceContainer();
            
            RegisterGameStateMachine();
            RegisterBootstrapState();

            GoToBootstrap();
        }

        private void RegisterServiceContainer() => 
            _services = new AllServices();

        private void RegisterGameStateMachine() => 
            _gameStateMachine = new StateMachine.StateMachine();

        private void RegisterBootstrapState()
        {
            _gameStateMachine.RegisterState<BootstrapState>(
                new BootstrapState(_services, _gameStateMachine, _sceneData));
        }

        private void GoToBootstrap() => 
            _gameStateMachine.ChangeState<BootstrapState>();
        
        [Serializable]
        public class SceneData
        {
            public Camera Camera;
            public SectionControl SectionControl;
            public MenuPanelHolder MenuPanelHolder;
            public ControlMovement controlMovement;
        }
    }
}