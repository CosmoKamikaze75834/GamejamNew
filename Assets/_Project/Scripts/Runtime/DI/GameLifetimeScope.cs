using CustomUpdateService;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private UpdateService _updateService;
    [SerializeField] private AudioService _audioService;
    [SerializeField] private SavesDataConfig _savesDataConfig;

    private IContainerBuilder _builder;

    protected override void Configure(IContainerBuilder builder)
    {
        _builder = builder;

        BindSceneLoader();
        BindInputReader();
        BindUpdateService();
        BindAudioService();
        BindLanguageSwitcher();
        BindSaver();
    }

    private void BindSceneLoader() =>
        _builder.Register<SceneLoader>(Lifetime.Singleton);

    private void BindInputReader() =>
        _builder.Register<InputReader>(Lifetime.Singleton).As<IInputReader>();

    private void BindUpdateService() =>
        _builder.RegisterComponent<IUpdateService>(_updateService);

    private void BindAudioService() =>
        _builder.RegisterComponent<IAudioService>(_audioService);

    private void BindLanguageSwitcher() =>
        _builder.Register<LanguageSwitcher>(Lifetime.Singleton);

    private void BindSaver()
    {
        _builder.RegisterComponent(_savesDataConfig);
        _builder.RegisterComponent(_savesDataConfig.SavesData);
        _builder.Register<NoEncrypt>(Lifetime.Singleton).As<IEncryptor>();
        _builder.Register<JsonSavingUtility>(Lifetime.Singleton).As<ISavingUtility>();
        _builder.Register<Saver<SavesData>>(Lifetime.Singleton).As<ISaver<SavesData>>();
    }
}