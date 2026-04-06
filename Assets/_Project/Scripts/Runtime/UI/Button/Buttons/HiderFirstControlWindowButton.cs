using UnityEngine;

public class HiderFirstControlWindowButton : ButtonClickHandler
{
    [SerializeField] private IntroPopup _firstContolPopUp;
    [SerializeField] private IntroPopup _secondContolPopUp;

    public override void OnClick()
    {
        _firstContolPopUp.Hide();
        _secondContolPopUp.Show();
    }
}