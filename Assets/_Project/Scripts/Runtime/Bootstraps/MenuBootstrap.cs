using UnityEngine;
using VContainer;

public class MenuBootstrap : MonoBehaviour
{
    private IUpdateService _updater;
    private IInputReader _inputReader;

    [Inject]
    public void Construct(IUpdateService updateService, IInputReader inputReader)
    {
        Debug.Log(nameof(Construct));

        _updater = updateService;
        _inputReader = inputReader;

        if (_updater != null)
        {
            Debug.Log("_updater прокинулся");
        }
    }
}