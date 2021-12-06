using UnityEngine;
using System.Collections.Generic;
public class ServiceLocator
{
    private ServiceLocator() { }
    Dictionary<int, IService> services = new Dictionary<int, IService>();
    #region Public Methods
    public static ServiceLocator Current { get; private set; }
    public static void Initialize()
    {
        Current = new ServiceLocator();
        Debug.Log($"servicelocator initialized");
    }
    public void Register<T>(T service) where T : IService 
    {
        int hash = typeof(T).GetHashCode();
        if (services.ContainsKey(hash))
        {
            Debug.LogError("Attempted to register service multiple times.");
            return;
        }
        services.Add(hash, service);
    }
    public void Unregister<T>(T service) where T : IService
    {
        int hash = service.GetHashCode();
        if (!services.ContainsKey(hash))
        {
            Debug.LogError("Attempted to deregister an unregistered service.");
            return;
        }
        services.Remove(hash);
    }

    public T Get<T>() where T : IService
    {
        int hash = typeof(T).GetHashCode();
        if (!services.ContainsKey(hash))
        {
            Debug.LogError("Attempted to retrieve unregistered service.");
            return default;
        }
        return (T)services[hash];
    }
    #endregion
}
