using UnityEngine;

public class ContinueGameButton : ButtonClickHandler
{
    [SerializeField] private PopUp _menuButtons;

    public override void OnClick() =>
        _menuButtons.Hide();
}