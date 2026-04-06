using System;
using UnityEngine;

public class TimeScalePauseSwitcher : IPauseSwitcher, IDisposable
{
    private readonly float _originTimeScale;

    private bool _isLock;

    public TimeScalePauseSwitcher()
    {
        _originTimeScale = Time.timeScale;
    }

    public void Dispose() =>
        Unpause();


    public void Pause()
    {
        if (_isLock == false)
            Time.timeScale = 0f;
    }


    public void Unpause()
    {
        if (_isLock == false)
            Time.timeScale = _originTimeScale;
    }

    public void Lock() =>
        _isLock = true;

    public void Unock() =>
        _isLock = false;
}