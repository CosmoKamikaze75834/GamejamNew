using System;
using UnityEngine;

public class TimeScalePauseSwitcher : IPauseSwitcher, IDisposable
{
    private readonly float _originTimeScale;

    public TimeScalePauseSwitcher()
    {
        _originTimeScale = Time.timeScale;
    }

    public void Dispose() =>
        Unpause();

    public void Pause() =>
        Time.timeScale = 0f;

    public void Unpause() =>
        Time.timeScale = _originTimeScale;
}