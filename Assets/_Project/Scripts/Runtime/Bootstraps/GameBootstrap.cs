using System.Collections.Generic;
using FiXiKTestScripts;
using UnityEngine;
using VContainer;

public class GameBootstrap : BootstrapBase
{
    [SerializeField] private IntroPopup _controlPopUp;
    [SerializeField] private IntroPopup _secondControlPopUp;
    [SerializeField] private RadioFactory _radioFactory;
    [SerializeField] private GameStats _gameStats;
    [SerializeField] private PopUp _menuButtons;
    [SerializeField] private PopUp _losePopUp;
    [SerializeField] private Transform _playerStartPosition;
    [SerializeField] private List<PopUp> _popUpList;
    [SerializeField] private List<MediatorBase> _mediators;
    [SerializeField] private int _enemyCount = 6;
    [SerializeField] private int _npcCount = 80;

    private IAudioService _audioService;
    private IInputReader _inputReader;
    private IPauseSwitcher _pauseSwitcher;
    private PlayerFactory _playerFactory;
    private EnemyFactory _enemyFactory;
    private NpcFactory _npcFactory;

    [Inject]
    public void Construct(
        IAudioService audioService,
        IInputReader inputReader,
        IPauseSwitcher pauseSwitcher,
        PlayerFactory playerFactory,
        EnemyFactory enemyFactory,
        NpcFactory npcFactory)
    {
        _audioService = audioService;
        _inputReader = inputReader;
        _pauseSwitcher = pauseSwitcher;
        _playerFactory = playerFactory;
        _enemyFactory = enemyFactory;
        _npcFactory = npcFactory;        
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
        SmallPlayer player = _playerFactory.CreateSmallPlayer(_playerStartPosition.position, _playerStartPosition.rotation);
        _enemyFactory.Spawn(_enemyCount);
        _npcFactory.Spawn(_npcCount);        
        _gameStats.CreateLines();
        _radioFactory.Spawn(_enemyCount + 1);

        _pauseSwitcher.Pause();
        _pauseSwitcher.Lock();
        _controlPopUp.Show();
        _secondControlPopUp.Hide();

        player.Required += OnPlayerRequired;
    }

    private void OnPlayerRequired(SmallPlayer smallPlayer)
    {
        smallPlayer.Required -= OnPlayerRequired;
        _losePopUp.Show();
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