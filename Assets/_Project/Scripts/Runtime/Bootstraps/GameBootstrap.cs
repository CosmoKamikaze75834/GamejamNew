using UnityEngine;
using VContainer;

public class GameBootstrap : MonoBehaviour
{
    [SerializeField] private SavingMediator _savingMediator;
    [SerializeField] private VolumeMediator _volumeMediator;

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

        _audioService?.Music.PlayGameMusic();
    }
}