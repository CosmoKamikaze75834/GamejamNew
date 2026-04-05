using System;
using FiXiKTestScripts;
using Unity.Cinemachine;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameScope : LifetimeScope
{
    [SerializeField] private Bullet _bulletPrefab;

    [SerializeField] private ConspiracyTheory _conspiracyTheoryPrefab;
    [SerializeField] private ConspiracyTheoryConfig _conspiracyTheoryConfig;

    [SerializeField] private ColorSetConfig _colorSetConfig;

    [SerializeField] private PlayerFactory _playerFactory;
    [SerializeField] private EnemyFactory _enemyFactory;
    [SerializeField] private NpcFactory _npcFactory;
    [SerializeField] private CinemachineCamera _cinemachineCamera;

    private IContainerBuilder _builder;

    protected override void Configure(IContainerBuilder builder) 
    {
        _builder = builder;

        BindPauseSwitcher();
        BindColorSetConfig();
        BindConspiracyTheoryConfig();
        BindBulletPrefab();
        BindShooterFactory();
        BindConspiracyTheoryPrefab();
        BindConspiracyTheoryFactory();
        BindColorFactory();
        BindPlayerFactory();
        BindEnemyFactory();
        BindNpcFactory();
        BindCinemachineCamera();
        BindAttackerRegistry();
        BindNpcRegistry();
    }

    private void BindPauseSwitcher() =>
        _builder.Register<TimeScalePauseSwitcher>(Lifetime.Singleton).As<IPauseSwitcher>();

    private void BindColorSetConfig() =>
        _builder.RegisterComponent(_colorSetConfig);

    private void BindConspiracyTheoryConfig() =>
        _builder.RegisterComponent(_conspiracyTheoryConfig);

    private void BindBulletPrefab() =>
        _builder.RegisterComponent(_bulletPrefab);

    private void BindShooterFactory() =>
        _builder.Register<ShooterFactory>(Lifetime.Singleton);

    private void BindConspiracyTheoryPrefab() =>
        _builder.RegisterComponent(_conspiracyTheoryPrefab);

    private void BindConspiracyTheoryFactory() =>
        _builder.Register<ConspiracyTheoryFactory>(Lifetime.Singleton);

    private void BindColorFactory() =>
        _builder.Register<ColorFactory>(Lifetime.Singleton);

    private void BindPlayerFactory() =>
        _builder.RegisterComponent(_playerFactory);

    private void BindEnemyFactory() =>
        _builder.RegisterComponent(_enemyFactory);

    private void BindNpcFactory() =>
        _builder.RegisterComponent(_npcFactory);

    private void BindCinemachineCamera() =>
        _builder.RegisterComponent(_cinemachineCamera);

    private void BindAttackerRegistry() =>
        _builder.Register<AttackerRegistry>(Lifetime.Singleton);

    private void BindNpcRegistry() =>
        _builder.Register<NpcRegistry>(Lifetime.Singleton);
}