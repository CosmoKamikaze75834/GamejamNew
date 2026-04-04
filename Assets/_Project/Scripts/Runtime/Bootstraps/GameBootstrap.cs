using UnityEngine;
using VContainer;

public class GameBootstrap : BootstrapBase
{
    [SerializeField] private SavingMediator _savingMediator;
    [SerializeField] private VolumeMediator _volumeMediator;
    [SerializeField] private AnticlickerMediator _anticlickerMediator;
    [SerializeField] private PopUp _settingsPopUp;
    [SerializeField] private PopUp _menuButtons;

    private IAudioService _audioService;
    private IInputReader _inputReader;

    [Inject]
    public void Construct(IAudioService audioService, IInputReader inputReader)
    {
        _audioService = audioService;
        _inputReader = inputReader;
    }

    private void Start()
    {
        _savingMediator.Init();
        _volumeMediator.Init();
        _anticlickerMediator.Init();
        _settingsPopUp.Init();
        _menuButtons.Init();

        _inputReader.EscapePressed += OnEscapePressed;

        _audioService?.Music.PlayGameMusic();
    }

    private void OnDestroy()
    {
        if (_audioService != null)
            _inputReader.EscapePressed -= OnEscapePressed;
    }

    private void OnEscapePressed()
    {
        if (_menuButtons.gameObject.activeInHierarchy)
            _menuButtons.Hide();
        else
            _menuButtons.Show();
    }
}