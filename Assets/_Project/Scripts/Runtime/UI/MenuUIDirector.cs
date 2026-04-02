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
    private IAudioService _audioService;

    [Inject]
    public void Construct(SceneLoader sceneLoader, IAudioService audioService)
    {
        _sceneLoader = sceneLoader;
        _audioService = audioService;
    }

    private void OnEnable()
    {
        _openerSettingsButton.onClick.AddListener(OnClickOpenerSettingsButton);
        _closerSettingsButton.onClick.AddListener(OnClickCloserSettingsButton);
        _playButton.onClick.AddListener(OnClickPlaytButton);
        _exitGameButton.onClick.AddListener(OnClickExitButton);
    }

    private void OnDisable()
    {
        _openerSettingsButton.onClick.RemoveListener(OnClickOpenerSettingsButton);
        _closerSettingsButton.onClick.RemoveListener(OnClickCloserSettingsButton);
        _playButton.onClick.RemoveListener(OnClickPlaytButton);
        _exitGameButton.onClick.RemoveListener(OnClickExitButton);
    }

    private void OnClickOpenerSettingsButton()
    {
        _settingsPopApp.Show();
        _audioService.Sfx.PlayButtonClick();
    }

    private void OnClickCloserSettingsButton()
    {
        _settingsPopApp.Hide();
        _audioService.Sfx.PlayButtonClick();
    }

    private void OnClickPlaytButton()
    {
        _sceneLoader.Load(Constants.GameSceneName);
        _audioService.Sfx.PlayButtonClick();
    }

    private void OnClickExitButton()
    {
        Application.Quit();
        _audioService.Sfx.PlayButtonClick();
    }
}