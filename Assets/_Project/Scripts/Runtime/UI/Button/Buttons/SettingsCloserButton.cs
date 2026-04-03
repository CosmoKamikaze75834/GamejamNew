using UnityEngine;

public class SettingsCloserButton : ButtonClickHandler
{
    [SerializeField] private SettingsPopApp _popUp;

    public override void OnClick() =>
        _popUp.Hide();
}