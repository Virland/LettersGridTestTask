using System.Collections.Generic;
using UnityEngine;

namespace Grid
{
    public abstract class GameObjectPool<T> : MonoBehaviour where T : class
    {
        [Min(0)] [SerializeField] private int m_InitialCount;
        [Min(1)] [SerializeField] private int m_ExtendCount;
        [SerializeField] private GameObject m_SourceObject;

        private readonly List<PoolElement> m_Elements = new List<PoolElement>();

        public void Init()
        {
            Extend(m_InitialCount);
        }

        public T Get()
        {
            PoolElement poolElement = m_Elements.Find(e=>e.free);
            if(poolElement == null)
            {
                Extend(m_ExtendCount);
                poolElement = m_Elements.Find(e => e.free);
            }

            poolElement.free = false;
            OnElementGet(poolElement.obj);
            return poolElement.obj;
        }

        public void Return(T element)
        {
            var poolElement = m_Elements.Find(e => e.obj == element);
            if (poolElement == null)
            {
                Debug.LogError($"{element} does not belong to this pool");
            }
            else
            {
                poolElement.free = true;
                OnElementReturn(poolElement.obj);
            }
        }

        protected abstract void OnElementGet(T obj);

        protected abstract void OnElementReturn(T obj);

        private void Extend(int count)
        {
            for(int i = 0; i < m_ExtendCount; i++)
            {
                m_Elements.Add(CreateElement());
            }
        }

        private PoolElement CreateElement()
        {
            return new PoolElement(CreateObject());
        }

        private T CreateObject()
        {
            GameObject obj = Instantiate(m_SourceObject);
            T result = obj.GetComponent<T>();
            return result;
        }

        class PoolElement
        {
            public bool free;
            public T obj;

            public PoolElement(T obj)
            {
                this.obj = obj;
                this.free = true;
            }
        }
    }
}