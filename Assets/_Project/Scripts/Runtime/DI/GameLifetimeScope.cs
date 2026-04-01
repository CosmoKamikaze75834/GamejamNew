using VContainer;
using VContainer.Unity;
using UnityEngine;
using CustomUpdateService;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private MonoBehaviour _someSceneReference;
    [SerializeField] private UpdateService _updateService;
    [SerializeField] private AudioService _audioService;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<InputReader>(Lifetime.Singleton).As<IInputReader>();
        builder.RegisterComponent<IUpdateService>(_updateService);
        builder.RegisterComponent<IAudioService>(_audioService);
    }
}