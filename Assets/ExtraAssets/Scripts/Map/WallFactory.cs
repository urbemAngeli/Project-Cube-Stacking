using System;
using System.Collections.Generic;
using ExtraAssets.Scripts.Enemy;
using ExtraAssets.Scripts.Infrastructure;
using ExtraAssets.Scripts.Services.AssetManagement;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ExtraAssets.Scripts.Map
{
    public class WallFactory
    {
        private List<PoolMono<EnemyWall>> _walls = new List<PoolMono<EnemyWall>>();
        private Dictionary<EnemyWall, PoolMono<EnemyWall>> _takenWall = new Dictionary<EnemyWall, PoolMono<EnemyWall>>();

        private Transform _root;

        public WallFactory(IAssetProvider assetProvider)
        {
            EnemyWall[] wallPrefabs = assetProvider.LoadAll<EnemyWall>(AssetPath.ENEMY_WALL_PATH);
            
            CreateRoot();
            AddWalls(wallPrefabs);
        }

        public EnemyWall Take()
        {
            EnemyWall enemyWall = SelectWall();
            
            enemyWall.Initialize();
            
            return enemyWall;
        }

        public void Put(EnemyWall enemyWall)
        {
            if (_takenWall.TryGetValue(enemyWall, out PoolMono<EnemyWall> pool))
            {
                enemyWall.Dispose();
                
                pool.Put(enemyWall);
                _takenWall.Remove(enemyWall);
                
                return;
            }

            throw new Exception($"Pool not found for {enemyWall}");
        }

        private EnemyWall SelectWall()
        {
            int index = Random.Range(0, _walls.Count);
            EnemyWall enemyWall = _walls[index].Take();
            
            _takenWall.Add(enemyWall, _walls[index]);
            
            return enemyWall;
        }

        private void CreateRoot() => 
            _root = new GameObject("[WallFactory]").transform;

        private void AddWalls(EnemyWall[] wallPrefabs)
        {
            for (int i = 0; i < wallPrefabs.Length; i++)
                _walls.Add(new PoolMono<EnemyWall>(wallPrefabs[i], 1, _root, false, OnObjectCreated));
        }

        private void OnObjectCreated(EnemyWall enemyWall) => 
            enemyWall.Construct();
    }
}