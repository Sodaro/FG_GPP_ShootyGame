using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool<T> : MonoBehaviour where T : Component
{
    [SerializeField] private int _poolSize = 10;
    [SerializeField] T _poolObject;

    private static ObjectPool<T> _instance;
    public static ObjectPool<T> Instance => _instance;

    private Queue<T> _freeList;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        _freeList = new Queue<T>();
        for (int i = 0; i < _poolSize; i++)
        {
            T component = Instantiate(_poolObject, transform);
            GameObject instance = component.gameObject;
            instance.SetActive(false);
            _freeList.Enqueue(component);
        }
    }

    public void AddToPool(T obj)
    {
        obj.gameObject.SetActive(false);
        _freeList.Enqueue(obj);
    }

    public T Get()
    {
        if (_freeList.Count == 0)
            return null;

        T instance = _freeList.Dequeue();
        instance.gameObject.SetActive(true);
        return instance;
    }
}
