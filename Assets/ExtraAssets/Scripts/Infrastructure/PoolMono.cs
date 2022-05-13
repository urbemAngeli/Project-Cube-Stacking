using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ExtraAssets.Scripts.Infrastructure
{
    public class PoolMono<T> where T : MonoBehaviour
    {
        public List<T> TakenObjects => _takenObjects;
        
        private readonly T _prefab;
        private readonly Transform _container;

        private List<T> _poolObjects;
        private List<T> _takenObjects;

        private bool _isDefaultActive;
        private Action<T> _onObjectCreated;


        public PoolMono(T prefab, int count, Transform container, bool isDefaultActive, Action<T> onObjectCreated = null)
        {
            _prefab = prefab;
            _container = container;
            _isDefaultActive = isDefaultActive;
            _onObjectCreated = onObjectCreated;

            CreatePool(count);
        }

        public T Take()
        {
            return HasFreeObject() 
                ? TakeFromPool() 
                : TakeFromNew();
        }

        public void Put(T target)
        {
            if (_takenObjects.Contains(target))
            {
                SetInContainer(target);
                
                _poolObjects.Add(target);
                _takenObjects.Remove(target);
                
                return;
            }

            throw new NullReferenceException($"Don't found object in _takenObjects: {target}");
        }

        private void CreatePool(int count)
        {
            _poolObjects = new List<T>();
            _takenObjects = new List<T>();
            
            for (int i = 0; i < count; i++) 
                _poolObjects.Add(CreateObject());
        }

        private bool HasFreeObject() => 
            _poolObjects.Count > 0;

        private T TakeFromPool()
        {
            T takenObject = _poolObjects[_poolObjects.Count - 1];
            
            _takenObjects.Add(takenObject);
            _poolObjects.Remove(takenObject);

            return takenObject;
        }

        private T TakeFromNew()
        {
            T target = CreateObject();
            _takenObjects.Add(target);

            return target;
        }

        private T CreateObject()
        {
            T createdObject = Object.Instantiate(_prefab, _container);
            SetInContainer(createdObject);
            _onObjectCreated?.Invoke(createdObject);

            return createdObject;
        }

        private void SetInContainer(T target)
        {
            target.gameObject.SetActive(_isDefaultActive);
            target.transform.parent = _container;
        }
    }
}