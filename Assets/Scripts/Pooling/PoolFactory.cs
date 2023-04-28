using UnityEngine;

namespace Pooling
{
    public interface IFactory<out T>
    {
        T Get();
    }

    public class PoolFactory<T> : IFactory<T> where T : MonoBehaviour, IPoolObject
    {
        private readonly T _item;
        private readonly RectTransform _container;
        private readonly Pool<T> _pool;

        public PoolFactory(T item, RectTransform container, int capacity = 0)
        {
            _item = item;
            _container = container;
            _pool = new Pool<T>(Creator, Destroyer, capacity);
        }

        public T Get()
        {
            return _pool.Get();
        }

        public void Release(T item)
        {
            _pool.Release(item);
        }
        
        
        public void Clean()
        {
            _pool.Clean();
        }

        public bool AllAvailable()
        {
            return _pool.AllAvailable();
        }

        private T Creator()
        {
            return Object.Instantiate(_item, _container);
        }

        private void Destroyer(T item)
        {
            Object.Destroy(item.gameObject);
        }
    }
}