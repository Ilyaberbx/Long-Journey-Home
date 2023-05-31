using System;
using UnityEngine;

namespace Logic.CutScenes
{
    public interface ICutScene
    {
        void StartCutScene(Transform player, Action onCutSceneEnded);
    }
}