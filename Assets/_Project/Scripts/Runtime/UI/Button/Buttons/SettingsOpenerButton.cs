using UnityEngine;

public class SettingsOpenerButton : ButtonClickHandler
{
    [SerializeField] private SettingsPopApp _popUp;

    public override void OnClick() =>
        _popUp.Show();
}