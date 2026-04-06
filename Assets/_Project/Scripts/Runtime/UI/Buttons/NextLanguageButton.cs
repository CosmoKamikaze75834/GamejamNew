using VContainer;

public class NextLanguageButton : ButtonClickHandler
{
    private LanguageSwitcher _languageSwitcher;

    [Inject]
    public void Construct(LanguageSwitcher languageSwitcher) =>
        _languageSwitcher = languageSwitcher;

    public override void OnClick() =>
        _languageSwitcher.Next();
}