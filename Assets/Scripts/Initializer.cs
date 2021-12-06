using UnityEngine;
using Observer;
public class Initializer : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Initialize()
    {
        ServiceLocator.Initialize();
        ServiceLocator.Current.Register(new EventHandler());
    }
}
