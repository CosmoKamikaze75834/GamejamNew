using UnityEngine;
using VContainer;

public class MenuBootstrap : MonoBehaviour
{
    private IAudioService _audioService;

    [Inject]
    public void Construct(IAudioService audioService)
    {
        _audioService = audioService;
    }

    private void Start()
    {
        _audioService.Music.PlayMenuMusic();
    }

    private void OnDisable()
    {
        _audioService?.Music.Stop();
    }
}