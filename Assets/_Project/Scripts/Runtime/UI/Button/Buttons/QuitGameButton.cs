using UnityEngine;

public class QuitGameButton : ButtonClickHandler
{
    public override void OnClick() =>
        Application.Quit();
}