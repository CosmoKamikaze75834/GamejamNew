using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using VContainer;

public class VolumeMediator : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;

    [SerializeField] private Slider _generalSlaider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;

    private VolumeModifier _generalModifier;
    private VolumeModifier _musicModifier;
    private VolumeModifier _sfxModifier;

    private ISaver<SavesData> _saver;

    [Inject]
    public void Constract(ISaver<SavesData> saver)
    {
        _saver = saver;
    }

    private void Start()
    {
        SavesData data = _saver.Data;

        _generalSlaider.value = data.GeneralSoundVolume;
        _musicSlider.value = data.MusicVolume;
        _sfxSlider.value = data.SfxVolume;

        _generalModifier = new(_mixer, _generalSlaider, Constants.MasterGroup);
        _musicModifier = new(_mixer, _musicSlider, Constants.MusicGroup);
        _sfxModifier = new(_mixer, _sfxSlider, Constants.SfxGroup);
    }

    private void OnDestroy()
    {
        _generalModifier.Dispose();
        _musicModifier.Dispose();
        _sfxModifier.Dispose();
    }
}