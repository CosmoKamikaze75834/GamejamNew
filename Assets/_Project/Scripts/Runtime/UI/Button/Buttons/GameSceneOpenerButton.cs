using UnityEngine;
using VContainer;

public class GameSceneOpenerButton : ButtonClickHandler
{
    [SerializeField] private PopUp _buttonsVerticalLayout;

    private SceneLoader _sceneLoader;

    [Inject]
    public void Construct(SceneLoader sceneLoader) =>
        _sceneLoader = sceneLoader;

    public override void OnClick()
    {
        _buttonsVerticalLayout.Hide();
        _sceneLoader.Load(Constants.GameSceneName);
    }
}