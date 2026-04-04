using UnityEngine;

public class ConfirmationQuitGameButton : ButtonClickHandler
{
    [SerializeField] private PopUp _quitGameDialog;

    public override void OnClick()
    {
        _quitGameDialog.Hide();
        Application.Quit();
    }
}