using UnityEngine;

public class CancelQuitGameButton : ButtonClickHandler
{
    [SerializeField] private PopUp _quitGameDialog;
    [SerializeField] private PopUp _buttonsVerticalLayout;

    public override void OnClick()
    {
        _buttonsVerticalLayout.Show();
        _quitGameDialog.Hide();
    }
}