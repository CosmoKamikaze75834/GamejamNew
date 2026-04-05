using UnityEngine;
using VContainer;

public class ContinueGameButton : ButtonClickHandler
{
    [SerializeField] private PopUp _menuButtons;

    private IPauseSwitcher _pauseSwitcher;

    [Inject]
    public void Construct(IPauseSwitcher pauseSwitcher)
    {
        _pauseSwitcher = pauseSwitcher;
    }

    public override void OnClick()
    {
        _menuButtons.Hide();
        _pauseSwitcher.Unpause();
    }
}