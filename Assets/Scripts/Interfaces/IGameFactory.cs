using System;
using System.Collections.Generic;
using Interfaces;
using ProjectSolitude.Interfaces;
using UnityEngine;

namespace ProjectSolitude.Infrastructure
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgressWriter> ProgressWriters { get; }
        
        GameObject HeroGameObject { get;}
        
        event Action OnHeroCreated;
        GameObject CreatePlayer(Vector3 at);
        GameObject CreateHud();
        void CleanUp();
    }
}