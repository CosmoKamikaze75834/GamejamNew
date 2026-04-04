using FiXiK_Utilites.QuitPanel;
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

    [Inject]
    public void Construct(IAudioService audioService)
    {
        _audioService = audioService;
    }

    private void Start()
    {
        _savingMediator.Init();
        _volumeMediator.Init();
        _anticlickerMediator.Init();
        _settingsPopUp.Init();
        _menuButtons.Init();

        _audioService?.Music.PlayGameMusic();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_menuButtons.gameObject.activeInHierarchy)
                _menuButtons.Hide();
            else
                _menuButtons.Show();
        }
    }
}