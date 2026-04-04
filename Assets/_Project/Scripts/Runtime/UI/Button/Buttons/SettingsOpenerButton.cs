using UnityEngine;

public class SettingsOpenerButton : ButtonClickHandler
{
    [SerializeField] private PopUp _popUp;
    [SerializeField] private PopUp _buttonsVerticalLayout;

    public override void OnClick()
    {
        _popUp.Show();

        if (_buttonsVerticalLayout != null)
            _buttonsVerticalLayout.Hide();
    }
}