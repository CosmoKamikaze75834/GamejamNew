using VContainer;

public class GameSceneOpenerButton : ButtonClickHandler
{
    private SceneLoader _sceneLoader;

    [Inject]
    public void Construct(SceneLoader sceneLoader) =>
        _sceneLoader = sceneLoader;

    public override void OnClick() =>
        _sceneLoader.Load(Constants.GameSceneName);
}