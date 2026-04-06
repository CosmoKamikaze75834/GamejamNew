using VContainer;

public class NextDifficultyButton : ButtonClickHandler
{
    private DifficultySwitcher _switcher;

    [Inject]
    public void Construct(DifficultySwitcher switcher) =>
        _switcher = switcher;

    public override void OnClick() =>
        _switcher.Next();
}