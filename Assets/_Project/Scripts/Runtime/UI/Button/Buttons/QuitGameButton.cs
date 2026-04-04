using UnityEngine;

public class QuitGameButton : ButtonClickHandler
{
    [SerializeField] private PopUp _quitGameDialog;

    public override void OnClick() =>
        _quitGameDialog.Show();
}