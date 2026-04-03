using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class SavingMediator : MonoBehaviour
{
    [SerializeField] private Slider _generalSound;
    [SerializeField] private Slider _music;
    [SerializeField] private Slider _sfx;

    private ISaver<SavesData> _saver;
    private LanguageSwitcher _languageSwitcher;

    [Inject]
    public void Construct(ISaver<SavesData> saver, LanguageSwitcher languageSwitcher)
    {
        _saver = saver;
        _languageSwitcher = languageSwitcher;
    }

    public void Init()
    {
        SavesData data = _saver.Data;

        _generalSound.value = data.GeneralSoundVolume;
        _music.value = data.MusicVolume;
        _sfx.value = data.SfxVolume;
        _languageSwitcher.SetLang(data.Lang);
    }

    private void OnDisable() =>
        Save();

    private void Save()
    {
        SavesData savesData = new(
            _generalSound.value,
            _music.value,
            _sfx.value,
            LanguageSwitcher.LangType);

        _saver.Save(savesData);
    }
}