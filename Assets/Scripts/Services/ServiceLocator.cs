using System.Collections.Generic;
using UnityEngine;
public class ServiceLocator
{
    //private ctor so we cannot create a new one with new()
    private ServiceLocator() { }

    private Dictionary<int, IService> _services = new Dictionary<int, IService>();
    #region Public Methods
    public static ServiceLocator Instance { get; private set; }
    public static void Initialize()
    {
        Instance = new ServiceLocator();
    }
    public void Register<T>(T service) where T : IService
    {
        int hash = typeof(T).GetHashCode();
        if (_services.ContainsKey(hash))
        {
            Debug.LogError("Attempted to register service multiple times.");
            return;
        }
        _services.Add(hash, service);
    }
    public void Unregister<T>(T service) where T : IService
    {
        int hash = service.GetHashCode();
        if (!_services.ContainsKey(hash))
        {
            Debug.LogError("Attempted to deregister an unregistered service.");
            return;
        }
        _services.Remove(hash);
    }

    public T Get<T>() where T : IService
    {
        int hash = typeof(T).GetHashCode();
        if (!_services.ContainsKey(hash))
        {
            Debug.LogError("Attempted to retrieve unregistered service.");
            return default;
        }
        return (T)_services[hash];
    }
    #endregion
}
