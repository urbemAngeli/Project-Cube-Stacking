using System.Collections.Generic;
using ExtraAssets.Scripts.Infrastructure.Services;
using ExtraAssets.Scripts.Map.Cube;
using ExtraAssets.Scripts.Services.AssetManagement;
using ExtraAssets.Scripts.Services.CrossInput;
using UnityEngine;

namespace ExtraAssets.Scripts.Player
{
    public class PlayerFactory : IService
    {
        public PlayerControl Player => _player;

        private readonly IAssetProvider _assetProvider;
        private readonly IInputService _input;
        private readonly CubeObjectFactory _cubeObjectFactory;

        private PlayerControl _player;

        public PlayerFactory(IAssetProvider assetProvider, IInputService input, CubeObjectFactory cubeObjectFactory)
        {
            _assetProvider = assetProvider;
            _input = input;
            _cubeObjectFactory = cubeObjectFactory;
        }

        public void Create()
        {
            _player = _assetProvider.Instantiate<PlayerControl>(AssetPath.PLAYER_PATH);
            _player.Construct(_input);
            _player.Set(Vector3.zero, _cubeObjectFactory.Take());
        }

        public void Reload()
        {
            Destruct();
            _player.Set(Vector3.zero, _cubeObjectFactory.Take());
        }

        private void Destruct()
        {
            List<CubeObject> detachedCubes = _player.DetachAllCubeObjects();
            _cubeObjectFactory.Put(detachedCubes);
        }
    }
}