using System;
using System.Collections.Generic;
using ExtraAssets.Scripts.Infrastructure;
using ExtraAssets.Scripts.Map.Cube;
using ExtraAssets.Scripts.Services.AssetManagement;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ExtraAssets.Scripts.Map
{
    public class CubePackFactory
    {
        private readonly CubeObjectFactory _cubeObjectFactory;
        
        private List<PoolMono<CubePack>> _cubePacks = new List<PoolMono<CubePack>>();
        private Dictionary<CubePack, PoolMono<CubePack>> _takenCubePacks = new Dictionary<CubePack, PoolMono<CubePack>>();

        private Transform _root;

        public CubePackFactory(IAssetProvider assetProvider, CubeObjectFactory cubeObjectFactory)
        {
            _cubeObjectFactory = cubeObjectFactory;
            CubePack[] cubePackPrefabs = assetProvider.LoadAll<CubePack>(AssetPath.CUBE_PACK_PATH);
            
            CreateRoot();
            AddCubePacks(cubePackPrefabs);
        }

        public CubePack Take()
        {
            CubePack cubePack = SelectedCubePack();
            cubePack.Initialize();
            
            FillCubePack(cubePack);
            
            return cubePack;
        }

        public void Put(CubePack cubePack)
        {
            if (_takenCubePacks.TryGetValue(cubePack, out PoolMono<CubePack> pool))
            {
                cubePack.Dispose();
                pool.Put(cubePack);
                _takenCubePacks.Remove(cubePack);
                
                return;
            }

            throw new Exception($"Pool not found for {cubePack}");
        }

        private CubePack SelectedCubePack()
        {
            int index = Random.Range(0, _cubePacks.Count);
            CubePack cubePack = _cubePacks[index].Take();
            
            _takenCubePacks.Add(cubePack, _cubePacks[index]);

            return cubePack;
        }

        private void FillCubePack(CubePack cubePack)
        {
            CubeObject cubeObject;
            
            for (int i = 0; i < cubePack.Length; i++)
            {
                cubeObject = _cubeObjectFactory.Take();
                cubePack[i].SetCube(cubeObject);
            }
        }
        
        private void CreateRoot() => 
            _root = new GameObject("[CubePackFactory]").transform;

        private void AddCubePacks(CubePack[] cubePackPrefabs)
        {
            for (int i = 0; i < cubePackPrefabs.Length; i++)
                _cubePacks.Add(new PoolMono<CubePack>(cubePackPrefabs[i], 1, _root, false));
        }
    }
}