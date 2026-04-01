using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class MenuBootstrap : MonoBehaviour
{
    [SerializeField] private Slider _generalSound;
    [SerializeField] private Slider _music;
    [SerializeField] private Slider _sfx;


    private IUpdateService _updater;
    private IInputReader _inputReader;
    private ISaver<SavesData> _saver;

    [Inject]
    public void Construct(IUpdateService updateService, IInputReader inputReader, ISaver<SavesData> saver)
    {
        Debug.Log(nameof(Construct));

        _updater = updateService;
        _inputReader = inputReader;
        _saver = saver;

        if (_updater != null)
        {
            Debug.Log("_updater прокинулся");
        }

        SavesData data = _saver.Data;
        _generalSound.value = data.GeneralSoundVolume;
        _music.value = data.MusicVolume;
        _sfx.value = data.SfxVolume;
    }

    private void OnDisable()
    {
        SavesData savesData = new(
            _generalSound.value,
            _music.value,
            _sfx.value);

        _saver.Save(savesData);
    }
}