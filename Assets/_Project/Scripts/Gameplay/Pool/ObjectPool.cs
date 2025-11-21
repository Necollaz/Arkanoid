using System.Collections.Generic;
using UnityEngine;

namespace MiniIT.ARKANOID
{
    public class ObjectPool<T> where T : Component
    {
        private readonly T         prefab;
        private readonly Transform parentTransform;
        private readonly Stack<T>  pool;
    
        public ObjectPool(T prefab, Transform parentTransform, int initialSize)
        {
            this.prefab = prefab;
            this.parentTransform = parentTransform;
            pool = new Stack<T>(initialSize);
    
            for (int i = 0; i < initialSize; i++)
            {
                T instance = Create();
                Return(instance);
            }
        }
    
        public T Get()
        {
            if (pool.Count > 0)
            {
                T instance = pool.Pop();
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
            instance.transform.SetParent(parentTransform, false);
            pool.Push(instance);
        }
    
        private T Create()
        {
            T instance = Object.Instantiate(prefab, parentTransform);
            instance.gameObject.SetActive(false);
    
            return instance;
        }
    }
}