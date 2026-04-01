using UnityEngine;
using VContainer;

public class MenuBootstrap : MonoBehaviour
{
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
    }
}