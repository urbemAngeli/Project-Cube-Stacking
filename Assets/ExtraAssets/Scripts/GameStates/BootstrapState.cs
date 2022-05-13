using ExtraAssets.Scripts.Environment;
using ExtraAssets.Scripts.Infrastructure;
using ExtraAssets.Scripts.Infrastructure.Processors.FixedTick;
using ExtraAssets.Scripts.Infrastructure.Processors.Tick;
using ExtraAssets.Scripts.Infrastructure.Services;
using ExtraAssets.Scripts.Infrastructure.StateMachine;
using ExtraAssets.Scripts.Map;
using ExtraAssets.Scripts.Map.Cube;
using ExtraAssets.Scripts.Map.Provider;
using ExtraAssets.Scripts.Map.Section;
using ExtraAssets.Scripts.Player;
using ExtraAssets.Scripts.Services.AssetManagement;
using ExtraAssets.Scripts.Services.CrossInput;
using ExtraAssets.Scripts.UI;
using UnityEngine;
using StateMachine = ExtraAssets.Scripts.Infrastructure.StateMachine.StateMachine;

namespace ExtraAssets.Scripts.GameStates
{
    public class BootstrapState : IState
    {
        private readonly AllServices _services;
        private readonly StateMachine _gameStateMachine;
        private readonly Game.SceneData _sceneData;
        private CubeObjectFactory _cubeObjectFactory;

        public BootstrapState(AllServices services, StateMachine gameStateMachine, Game.SceneData sceneData)
        {
            _services = services;
            _gameStateMachine = gameStateMachine;
            _sceneData = sceneData;
        }

        public void Enter()
        {
            Debug.LogWarning("Enter to BootstrapState");
            
            RegisterServices();
            RegisterFactories();
            RegisterGameStates();

            InitializeMenuPanels();
            
            GoToLevelInitializationState();
        }

        private void InitializeMenuPanels() => 
            _sceneData.MenuPanelHolder.Initialize();

        public void Exit() { }

        private void RegisterSectionControl()
        {
            SectionControl sectionControl = _sceneData.SectionControl;

            sectionControl.Construct();
            _services.Single<IFixedTickProcessor>().Add(sectionControl);
        }

        private void RegisterServices()
        {
            RegisterAssetProvider();
            RegisterProcessors();
            RegisterInputService();
            RegisterSectionControl();
            RegisterMapProvider();
            RegisterMenuPanels();
        }

        private void RegisterMenuPanels() => 
            _services.RegisterSingle<IMenuPanels>(_sceneData.MenuPanelHolder);

        private void RegisterGameStates()
        {
            _gameStateMachine.RegisterState<LevelInitializationState>(
                new LevelInitializationState(
                    _gameStateMachine, 
                    _services.Single<IMapProvider>(),
                    _services.Single<PlayerFactory>()));
            
            _gameStateMachine.RegisterState<WaitingState>(
                new WaitingState(_gameStateMachine, 
                    _services.Single<IMenuPanels>(),
                    _services.Single<IInputService>()));

            _gameStateMachine.RegisterState<GameLoopState>(
                new GameLoopState(
                    _sceneData.SectionControl,
                    _services.Single<IFixedTickProcessor>(),
                    _services.Single<PlayerFactory>(),
                    _gameStateMachine,
                    CreateWarpEffect()));
            
            _gameStateMachine.RegisterState<GameOverState>(
                new GameOverState(
                    _services.Single<IMenuPanels>(),
                    _services.Single<IMapProvider>(),
                    _services.Single<PlayerFactory>(),
                    _services.Single<IInputService>(),
                    _gameStateMachine));
        }

        private void RegisterFactories()
        {
            RegisterPlayerFactory();
        }

        private void RegisterPlayerFactory()
        {
            _services.RegisterSingle<PlayerFactory>(new PlayerFactory(
                _services.Single<IAssetProvider>(),
                _services.Single<IInputService>(),
                _cubeObjectFactory));
        }

        private void GoToLevelInitializationState() => 
            _gameStateMachine.ChangeState<LevelInitializationState>();

        private void RegisterAssetProvider() => 
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());

        private void RegisterProcessors()
        {
            Transform root = new GameObject("[PROCESSORS]").transform;
            
            Object.DontDestroyOnLoad(root);
        
            RegisterTickProcessor(root);
            RegisterFixedTickProcessor(root);
        }

        private void RegisterTickProcessor(Transform processorRoot)
        {
            GameObject processor = new GameObject("TickProcessor");
            processor.transform.parent = processorRoot;
            _services.RegisterSingle<ITickProcessor>(processor.AddComponent<TickProcessor>());
        }

        private void RegisterFixedTickProcessor(Transform processorRoot)
        {
            GameObject processor = new GameObject("FixedTickProcessor");
            processor.transform.parent = processorRoot;
            _services.RegisterSingle<IFixedTickProcessor>(processor.AddComponent<FixedTickProcessor>());
        }

        private void RegisterMapProvider()
        {
            IAssetProvider assetProvider = _services.Single<IAssetProvider>();

            TrackSectionFactory trackSectionFactory = new TrackSectionFactory(assetProvider);
            WallFactory wallFactory = new WallFactory(assetProvider);
            _cubeObjectFactory = new CubeObjectFactory(assetProvider);
            CubePackFactory _cubePackFactory = new CubePackFactory(assetProvider, _cubeObjectFactory);
            
            _services.RegisterSingle<IMapProvider>(
                new MapProvider(
                    _sceneData.SectionControl,
                    trackSectionFactory,
                    wallFactory,
                    _cubePackFactory,
                    _cubeObjectFactory));
        }

        private void RegisterInputService()
        {
            StandaloneInputService service = new StandaloneInputService();
            _sceneData.controlMovement.Construct(service);
            _services.RegisterSingle<IInputService>(service);
        }

        private WarpEffect CreateWarpEffect()
        {
            WarpEffect warpEffect = _services.Single<IAssetProvider>().Instantiate<WarpEffect>(AssetPath.WARP_EFFECT);
            warpEffect.Hide();
            return warpEffect;
        }
    }
}