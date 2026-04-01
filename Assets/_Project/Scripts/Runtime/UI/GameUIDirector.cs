using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class GameUIDirector : MonoBehaviour
{
    [SerializeField] private Button _returnToMenu;
    [SerializeField] private SettingsPopApp _settingsPopApp;

    private SceneLoader _sceneLoader;

    [Inject]
    public void Construct(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }

    private void OnEnable()
    {
        _returnToMenu.onClick.AddListener(OnClickReturnToMenuButton);
    }

    private void OnDisable()
    {
        _returnToMenu.onClick.RemoveListener(OnClickReturnToMenuButton);
    }

    private void OnClickReturnToMenuButton()
    {
        _sceneLoader.Load(Constants.MenuSceneName);
    }
}