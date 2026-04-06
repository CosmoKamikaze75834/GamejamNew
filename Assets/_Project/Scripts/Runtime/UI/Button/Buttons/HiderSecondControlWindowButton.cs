using UnityEngine;
using VContainer;

public class HiderSecondControlWindowButton: ButtonClickHandler
{
    [SerializeField] private IntroPopup _intro;

    private IInputReader _inputReader;
    private IPauseSwitcher _pauseSwitcher;

    [Inject]
    public void Construct(IInputReader inputReader, IPauseSwitcher pauseSwitcher)
    {
        _inputReader = inputReader;
        _pauseSwitcher = pauseSwitcher;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _inputReader.EscapePressed += OnEscapePressed;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        _inputReader.EscapePressed -= OnEscapePressed;
    }

    private void OnEscapePressed()
    {
        _intro.Hide();
        _pauseSwitcher.Unock();
        _pauseSwitcher.Unpause();
    }

    public override void OnClick()
    {
        _intro.Hide();
        _pauseSwitcher.Unock();
        _pauseSwitcher.Unpause();
    }
}