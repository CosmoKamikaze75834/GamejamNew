using UnityEngine;
using VContainer;

public class EntryPointBootstrap : MonoBehaviour
{
    private SceneLoader _sceneLoader;

    [Inject]
    public void Construct(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }

    private void Start()
    {
        if (_sceneLoader.CurrentSceneName == Constants.EntryPointSceneName)
            _sceneLoader.Load(Constants.MenuSceneName);
    }
}