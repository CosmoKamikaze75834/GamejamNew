using UnityEngine.SceneManagement;

public class SceneLoader
{
    public string CurrentSceneName => SceneManager.GetActiveScene().name;

    public void Load(string name)
    {
        SceneManager.LoadScene(name);
    }
}