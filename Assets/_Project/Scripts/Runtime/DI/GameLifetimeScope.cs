using VContainer;
using VContainer.Unity;
using UnityEngine;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private MonoBehaviour someSceneReference;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<InputReader>(Lifetime.Singleton).As<IInputReader>();
    }
}