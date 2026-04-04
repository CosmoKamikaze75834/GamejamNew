using UnityEngine;

public class SettingsCloserButton : ButtonClickHandler
{
    [SerializeField] private PopUp _popUp;
    [SerializeField] private PopUp _buttonsVerticalLayout;

    public override void OnClick()
    {
        if(_buttonsVerticalLayout != null)
            _buttonsVerticalLayout.Show();

        _popUp.Hide();
    }
}