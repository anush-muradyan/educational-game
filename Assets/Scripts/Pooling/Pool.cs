using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pooling
{
    public class Pool<T>where T:IPoolObject
    {
        private Queue<T> _pendingQueue;
        private Func<T> _creator;
        private Action<T> _destroyer;
        private List<T> _activeItems = new List<T>();
        public Pool(Func<T> creator, Action<T> destroyer, int capacity = 0)
        {
            _creator = creator;
            _destroyer = destroyer;
            _pendingQueue = new Queue<T>(CreateRange(capacity));
        }

        public T Get()
        {
            if (_pendingQueue.Count == 0)
            {
                var i = Create();
                _activeItems.Add(i);
                return i;
            }
            var item = _pendingQueue.Dequeue();
            item.Activate();
            _activeItems.Add(item);
            return item;
        }

        public void Release(T item)
        {
            item.Deactivate();
            _activeItems.Remove(item);
            _pendingQueue.Enqueue(item);
        }

        public void Clean()
        {
            foreach (T poolObject in _pendingQueue)
            {
                _destroyer?.Invoke(poolObject);
            }
        
            _pendingQueue.Clear();
        }
        public bool AllAvailable()
        {
            return _activeItems.Count == 0;
        }
        
        private IEnumerable<T> CreateRange(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var item = Create();
                item?.Deactivate();
                yield return item;
            }
        }

        private T Create()
        {
            T item = _creator.Invoke();
            if (item == null)
            {
                Debug.LogError("Creator or creating item is null");
            }
            return item;
        }
    }
}