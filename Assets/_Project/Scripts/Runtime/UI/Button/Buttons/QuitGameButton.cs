using UnityEngine;

public class QuitGameButton : ButtonClickHandler
{
    [SerializeField] private PopUp _quitGameDialog;
    [SerializeField] private PopUp _buttonsVerticalLayout;

    public override void OnClick()
    {
        _quitGameDialog.Show();
        _buttonsVerticalLayout.Hide();
    }
}