using UnityEngine.SceneManagement;
public class SceneHandler : IService
{
    public enum Scene
    {
        Menu,
        Level1,
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
