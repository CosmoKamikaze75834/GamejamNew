using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class MenuBootstrap : MonoBehaviour
{
    [SerializeField] private Slider _generalSound;
    [SerializeField] private Slider _music;
    [SerializeField] private Slider _sfx;

    private ISaver<SavesData> _saver;
    private IAudioService _audioService;

    [Inject]
    public void Construct(ISaver<SavesData> saver, IAudioService audioService)
    {
        _saver = saver;
        _audioService = audioService;
    }

    private void Start()
    {
        SavesData data = _saver.Data;
        _generalSound.value = data.GeneralSoundVolume;
        _music.value = data.MusicVolume;
        _sfx.value = data.SfxVolume;
        _audioService.Music.PlayMenuMusic();
    }

    private void OnDisable()
    {
        SavesData savesData = new(
            _generalSound.value,
            _music.value,
            _sfx.value);

        _saver.Save(savesData);
        _audioService?.Music.Stop();
    }
}