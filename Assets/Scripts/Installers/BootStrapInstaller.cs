﻿using Infrastructure.Interfaces;
using Infrastructure.Services.Factories;
using Infrastructure.Services.SceneManagement;
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.State;
using Logic;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class BootStrapInstaller : MonoInstaller, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain _loadingCurtainPrefab;

        public override void InstallBindings()
        {
            BindInstallerInterfaces();
            BindLoadingCurtain();
            BindSceneLoader();
            BindStateFactory();
        }

        private void BindSceneLoader()
            => Container.Bind<ISceneLoader>()
                .To<SceneLoader>()
                .AsSingle()
                .NonLazy();

        private void BindInstallerInterfaces()
            => Container.BindInterfacesTo<BootStrapInstaller>()
                .FromInstance(this)
                .AsSingle()
                .NonLazy();

        private void BindLoadingCurtain()
        {
            LoadingCurtain curtain = Container.InstantiatePrefabForComponent<LoadingCurtain>(_loadingCurtainPrefab);
            Container.Bind<LoadingCurtain>()
                .FromInstance(curtain)
                .AsSingle()
                .NonLazy();
        }

        private void BindStateFactory()
            => Container
                .Bind<IStateFactory>()
                .To<StateFactory>()
                .AsSingle()
                .NonLazy();
        
    }
}