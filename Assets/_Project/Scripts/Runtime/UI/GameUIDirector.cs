using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class GameUIDirector : MonoBehaviour
{
    [SerializeField] private SettingsPopApp _settingsPopApp;
    [SerializeField] private Button _openerSettingsButton;
    [SerializeField] private Button _closerSettingsButton;
    [SerializeField] private Button _returnToMenu;

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
        _openerSettingsButton.onClick.AddListener(OnClickOpenSettingsButton);
        _closerSettingsButton.onClick.AddListener(OnClickCloseSettingsButton);
        _returnToMenu.onClick.AddListener(OnClickReturnToMenuButton);
    }

    private void OnDisable()
    {
        _openerSettingsButton.onClick.RemoveListener(OnClickOpenSettingsButton);
        _closerSettingsButton.onClick.RemoveListener(OnClickCloseSettingsButton);
        _returnToMenu.onClick.RemoveListener(OnClickReturnToMenuButton);
    }

    private void OnClickOpenSettingsButton()
    {
        _settingsPopApp.Show();
        _audioService.Sfx.PlayButtonClick();
    }

    private void OnClickCloseSettingsButton()
    {
        _settingsPopApp.Hide();
        _audioService.Sfx.PlayButtonClick();
    }

    private void OnClickReturnToMenuButton()
    {
        _sceneLoader.Load(Constants.MenuSceneName);
        _audioService.Sfx.PlayButtonClick();
    }
}