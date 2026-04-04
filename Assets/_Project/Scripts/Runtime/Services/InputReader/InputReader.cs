using System;
using UnityEngine;

public class InputReader : IInputReader, IDisposable
{
    private const KeyCode CommandEscape = KeyCode.Escape;

    private readonly IUpdateService _updateService;

    public InputReader(IUpdateService updateService)
    {
        _updateService = updateService;

        _updateService.Subscribe(OnUpdate, UpdateType.Update);
    }

    public event Action EscapePressed;

    public void Dispose() =>
        _updateService.Unsubscribe(OnUpdate, UpdateType.Update);

    private void OnUpdate(float deltaTime)
    {
        ReadEscape();
    }

    private void ReadEscape()
    {
        if (Input.GetKeyDown(CommandEscape))
            EscapePressed?.Invoke();
    }
}