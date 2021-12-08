using Observer;
using UnityEngine;
public class Initializer
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        ServiceLocator.Initialize();
        ServiceLocator.Instance.Register(new EventHandler());
        ServiceLocator.Instance.Register(new SceneHandler());
    }
}
