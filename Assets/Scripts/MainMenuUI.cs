using UnityEngine;
using UnityEngine.UI;
public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button _startButton;


    private void OnEnable()
    {
        _startButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        ServiceLocator.Instance.Get<SceneHandler>().LoadScene(SceneHandler.Scene.Level1);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(StartGame);
    }
}
