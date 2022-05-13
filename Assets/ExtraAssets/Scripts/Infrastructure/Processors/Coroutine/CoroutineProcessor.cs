using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExtraAssets.Scripts.Infrastructure.Processors.Coroutine
{
    public class CoroutineProcessor : MonoBehaviour, ICoroutineProcessor
    {
        private readonly Dictionary<int, IEnumerator> _routines = new Dictionary<int, IEnumerator>();
        

        public IEnumerator Run(IEnumerator routine)
        {
            int key = routine.GetHashCode();
            if (!_routines.ContainsKey(key))
            {
                StartCoroutine(routine);
                _routines.Add(routine.GetHashCode(), routine);
            }

            return routine;
        }
    
        public IEnumerator Stop(IEnumerator routine)
        {
            int key = routine.GetHashCode();
            if (_routines.ContainsKey(key))
            { 
                StopCoroutine(routine);
                _routines.Remove(key);
                routine = null;
            }

            return null;
        }

        public void StopAll()
        {
            StopAllCoroutines();
            _routines.Clear();
        }

        public bool Contains(IEnumerator routine)
        {
            if (routine == null)
                return false;

            int key = routine.GetHashCode();
            return _routines.ContainsKey(key);
        }
    }
}