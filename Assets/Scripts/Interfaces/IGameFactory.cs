using System;
using System.Collections.Generic;
using Logic;
using UnityEngine;

namespace Interfaces
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgressWriter> ProgressWriters { get; }

        GameObject CreatePlayer(Vector3 at);
        GameObject CreateHud();
        void CleanUp();
        void Register(ISavedProgressReader obj);
        GameObject CreateEnemy(EnemyType enemyType, Transform transform);
    }
}