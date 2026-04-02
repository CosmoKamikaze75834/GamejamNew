using UnityEngine;
using VContainer;

public class GameBootstrap : MonoBehaviour
{
    private IAudioService _audioService;

    [Inject]
    public void Construct(IAudioService audioService)
    {
        _audioService = audioService;
    }

    private void Start()
    {
        _audioService?.Music.PlayGameMusic();
    }

    private void OnDisable()
    {
        _audioService?.Music.Stop();
    }
}