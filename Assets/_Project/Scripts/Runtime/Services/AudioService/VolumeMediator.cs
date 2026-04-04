using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeMediator : MediatorBase
{
    [SerializeField] private AudioMixer _mixer;

    [SerializeField] private Slider _generalSlaider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;

    private VolumeModifier _generalModifier;
    private VolumeModifier _musicModifier;
    private VolumeModifier _sfxModifier;

    public override void Init()
    {
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