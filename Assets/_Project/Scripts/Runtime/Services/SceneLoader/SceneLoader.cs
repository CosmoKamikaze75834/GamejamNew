using UnityEngine.SceneManagement;

public class SceneLoader
{
    public void Load(string name)
    { 
        SceneManager.LoadScene(name);
    }
}