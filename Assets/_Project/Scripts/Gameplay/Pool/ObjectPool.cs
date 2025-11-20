using System.Collections.Generic;
using UnityEngine;

namespace MiniIT.ARKANOID
{
    public class ObjectPool<T> where T : Component
    {
        private readonly T         _prefab;
        private readonly Transform _parentTransform;
        private readonly Stack<T>  _pool;
    
        public ObjectPool(T prefab, Transform parentTransform, int initialSize)
        {
            _prefab = prefab;
            _parentTransform = parentTransform;
            _pool = new Stack<T>(initialSize);
    
            for (int i = 0; i < initialSize; i++)
            {
                T instance = Create();
                Return(instance);
            }
        }
    
        public T Get()
        {
            if (_pool.Count > 0)
            {
                T instance = _pool.Pop();
                instance.gameObject.SetActive(true);
                
                return instance;
            }
    
            return Create();
        }
    
        public void Return(T instance)
        {
            if (instance == null)
            {
                return;
            }
    
            instance.gameObject.SetActive(false);
            instance.transform.SetParent(_parentTransform, false);
            _pool.Push(instance);
        }
    
        private T Create()
        {
            T instance = Object.Instantiate(_prefab, _parentTransform);
            instance.gameObject.SetActive(false);
    
            return instance;
        }
    }
}