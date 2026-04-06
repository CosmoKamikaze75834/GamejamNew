using VContainer;

public class PreviousDifficultyButton : ButtonClickHandler
{
    private DifficultySwitcher _switcher;

    [Inject]
    public void Construct(DifficultySwitcher switcher) =>
        _switcher = switcher;

    public override void OnClick() =>
        _switcher.Previous();
}