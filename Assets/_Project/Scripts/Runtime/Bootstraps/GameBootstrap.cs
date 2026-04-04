using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class GameBootstrap : BootstrapBase
{
    [SerializeField] private List<PopUp> _popUpList;
    [SerializeField] private List<MediatorBase> _mediators;
    [SerializeField] private PopUp _menuButtons;

    private IAudioService _audioService;
    private IInputReader _inputReader;
    private IPauseSwitcher _pauseSwitcher;

    [Inject]
    public void Construct(
        IAudioService audioService,
        IInputReader inputReader,
        IPauseSwitcher pauseSwitcher)
    {
        _audioService = audioService;
        _inputReader = inputReader;
        _pauseSwitcher = pauseSwitcher;
    }

    private void Start()
    {
        foreach (PopUp popUp in _popUpList)
        {
            popUp.Init();
            popUp.FastHide();
        }

        foreach (MediatorBase mediator in _mediators)
            mediator.Init();

        _inputReader.EscapePressed += OnEscapePressed;

        _audioService?.Music.PlayGameMusic();
    }

    private void OnDestroy()
    {
        if (_inputReader != null)
            _inputReader.EscapePressed -= OnEscapePressed;
    }

    private void OnEscapePressed()
    {
        if (_menuButtons.gameObject.activeInHierarchy)
            _menuButtons.Hide();
        else
            _menuButtons.Show();

        if (_menuButtons.IsActive)
            _pauseSwitcher.Pause();
        else
            _pauseSwitcher.Unpause();
    }
}