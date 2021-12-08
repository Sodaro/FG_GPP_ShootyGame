using UnityEngine;

public class LevelExit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController _))
        {
            ServiceLocator.Instance.Get<SceneHandler>().LoadScene(SceneHandler.Scene.Level1);
        }
    }
}
