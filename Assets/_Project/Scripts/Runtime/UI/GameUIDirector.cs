using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class GameUIDirector : MonoBehaviour
{
    [SerializeField] private Slider _generalSound;
    [SerializeField] private Slider _music;
    [SerializeField] private Slider _sfx;
    [SerializeField] private SettingsPopApp _settingsPopApp;
    [SerializeField] private Button _openerSettingsButton;
    [SerializeField] private Button _closerSettingsButton;
    [SerializeField] private Button _returnToMenu;

    private SceneLoader _sceneLoader;
    private ISaver<SavesData> _saver;
    private IAudioService _audioService;

    [Inject]
    public void Construct(SceneLoader sceneLoader, ISaver<SavesData> saver, IAudioService audioService)
    {
        _sceneLoader = sceneLoader;
        _saver = saver;
        _audioService = audioService;
    }

    private void Start()
    {
        SavesData data = _saver.Data;
        _generalSound.value = data.GeneralSoundVolume;
        _music.value = data.MusicVolume;
        _sfx.value = data.SfxVolume;
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

        SavesData savesData = new(
            _generalSound.value,
            _music.value,
            _sfx.value);

        _saver.Save(savesData);
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