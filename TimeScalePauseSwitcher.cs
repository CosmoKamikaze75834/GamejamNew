using UnityEngine;

public interface IPauseSwitcher
{
    void Pause();

    void Unpause();
}

public class TimeScalePauseSwitcher : IPauseSwitcher
{
    public void Pause() =>
        Time.timeScale = 0f;

    public void Unpause() =>
        Time.timeScale = 1.0f;
}