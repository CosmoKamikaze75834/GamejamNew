using UnityEngine;

public class SettingsOpenerButton : ButtonClickHandler
{
    [SerializeField] private PopUp _popUp;

    public override void OnClick() =>
        _popUp.Show();
}