using UnityEngine;

public class SettingsCloserButton : ButtonClickHandler
{
    [SerializeField] private PopUp _popUp;

    public override void OnClick() =>
        _popUp.Hide();
}