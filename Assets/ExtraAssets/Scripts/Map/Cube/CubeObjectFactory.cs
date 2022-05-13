using System.Collections.Generic;
using ExtraAssets.Scripts.Infrastructure;
using ExtraAssets.Scripts.Services.AssetManagement;
using UnityEngine;

namespace ExtraAssets.Scripts.Map.Cube
{
    public class CubeObjectFactory
    {
        private PoolMono<CubeObject> _cubeObjects;

        public CubeObjectFactory(IAssetProvider assetProvider)
        {
            CubeObject prefab = assetProvider.Load<CubeObject>(AssetPath.CUBE_OBJECT_PATH);
            
            Transform root = new GameObject("[CubeObjectFactory]").transform;
            
            _cubeObjects = new PoolMono<CubeObject>(prefab, 20, root, false);
        }

        public CubeObject Take()
        {
            CubeObject cubeObject = _cubeObjects.Take();
            cubeObject.Initialize();
            
            cubeObject.OnGrilled += OnCubeGrilled;
            
            return cubeObject;
        }

        public void Put(CubeObject cubeObject) => 
            DisposeCube(cubeObject);
        
        public void Put(List<CubeObject> cubeObjects)
        {
            for (int i = 0; i < cubeObjects.Count; i++) 
                DisposeCube(cubeObjects[i]);
        }

        public void PutAll()
        {
            List<CubeObject> takenCubes = _cubeObjects.TakenObjects;

            for (int i = 0; i < takenCubes.Count; i++)
            {
                DisposeCube(takenCubes[i]);
                i--;
            }
        }

        private void OnCubeGrilled(CubeObject cubeObject) => 
            DisposeCube(cubeObject);

        private void DisposeCube(CubeObject cubeObject)
        {
            cubeObject.OnGrilled -= OnCubeGrilled;
            
            cubeObject.Dispose();
            _cubeObjects.Put(cubeObject);
        }
    }
}