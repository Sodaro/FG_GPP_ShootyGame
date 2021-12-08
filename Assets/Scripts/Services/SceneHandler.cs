using UnityEngine.SceneManagement;
public class SceneHandler : IService
{
    public enum Scene
    {
        Level1,
        Level2
    }

    public void LoadScene(Scene scene)
    {
        SceneManager.LoadScene((int)scene);
    }

    public void RestartActiveScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
