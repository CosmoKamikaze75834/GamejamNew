using UnityEngine;
using VContainer;

public class MenuBootstrap : BootstrapBase
{
    [SerializeField] private SavingMediator _savingMediator;
    [SerializeField] private VolumeMediator _volumeMediator;
    [SerializeField] private AnticlickerMediator _anticlickerMediator;
    [SerializeField] private PopUp _settingsPopUp;
    [SerializeField] private PopUp _quitGameDialog;
    [SerializeField] private PopUp _buttonsVerticalLayout;

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
        _quitGameDialog.Init();
        _buttonsVerticalLayout.Init();

        _buttonsVerticalLayout.Show();

        _audioService.Music.PlayMenuMusic();
    }
}