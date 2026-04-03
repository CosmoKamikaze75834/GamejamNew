using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class MenuUIDirector : MonoBehaviour
{
    [SerializeField] private Button _openerSettingsButton;
    [SerializeField] private Button _closerSettingsButton;
    [SerializeField] private Button _exitGameButton;
    [SerializeField] private Button _playButton;

    [SerializeField] private SettingsPopApp _settingsPopApp;

    private SceneLoader _sceneLoader;

    [Inject]
    public void Construct(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }

    private void OnEnable()
    {
        _openerSettingsButton.onClick.AddListener(OnClickOpenerSettingsButton);
        _closerSettingsButton.onClick.AddListener(OnClickCloserSettingsButton);
        _playButton.onClick.AddListener(OnClickPlayButton);
        _exitGameButton.onClick.AddListener(OnClickExitButton);
    }

    private void OnDisable()
    {
        _openerSettingsButton.onClick.RemoveListener(OnClickOpenerSettingsButton);
        _closerSettingsButton.onClick.RemoveListener(OnClickCloserSettingsButton);
        _playButton.onClick.RemoveListener(OnClickPlayButton);
        _exitGameButton.onClick.RemoveListener(OnClickExitButton);
    }

    private void OnClickOpenerSettingsButton()
    {
        _settingsPopApp.Show();
    }

    private void OnClickCloserSettingsButton()
    {
        _settingsPopApp.Hide();
    }

    private void OnClickPlayButton()
    {
        _sceneLoader.Load(Constants.GameSceneName);
    }

    private void OnClickExitButton()
    {
        Application.Quit();
    }
}