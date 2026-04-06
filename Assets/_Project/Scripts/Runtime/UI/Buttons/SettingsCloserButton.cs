using UnityEngine;

public class SettingsCloserButton : ButtonClickHandler
{
    [SerializeField] private PopUp _menuButtons;

    public override void OnClick() =>
        _menuButtons.Show();
}