using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class SavingMediator : MonoBehaviour
{
    [SerializeField] private Slider _generalSound;
    [SerializeField] private Slider _music;
    [SerializeField] private Slider _sfx;

    private ISaver<SavesData> _saver;

    [Inject]
    public void Construct(ISaver<SavesData> saver)
    {
        _saver = saver;
    }

    public void Init()
    {
        SavesData data = _saver.Data;

        _generalSound.value = data.GeneralSoundVolume;
        _music.value = data.MusicVolume;
        _sfx.value = data.SfxVolume;
    }

    private void OnDisable() =>
        Save();

    private void Load()
    {

    }

    private void Save()
    {
        SavesData savesData = new(
            _generalSound.value,
            _music.value,
            _sfx.value);

        _saver.Save(savesData);
    }
}