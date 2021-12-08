using Observer;
using UnityEngine;
public class Initializer : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Initialize()
    {
        ServiceLocator.Initialize();
        ServiceLocator.Current.Register(new EventHandler());
        ServiceLocator.Current.Register(new AudioSystem());
    }
}
