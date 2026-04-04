using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class MenuBootstrap : BootstrapBase
{
    [SerializeField] private List<PopUp> _popUpList;
    [SerializeField] private List<MediatorBase> _mediators;
    [SerializeField] private PopUp _menuButtonsPopUp;

    private IAudioService _audioService;

    [Inject]
    public void Construct(IAudioService audioService) =>
        _audioService = audioService;

    private void Start()
    {
        foreach (PopUp popUp in _popUpList)
        {
            popUp.Init();
            popUp.FastHide();
        }

        foreach (MediatorBase mediator in _mediators)
            mediator.Init();

        _menuButtonsPopUp.Show();
        _audioService.Music.PlayMenuMusic();
    }
}