using System;
using DG.Tweening;
using Logic.Spawners;
using UnityEngine;

namespace Logic.CutScenes
{
    public class IntroCutScene : BaseMarker, ICutScene
    {
        public void StartCutScene(Transform player, Action onCutSceneEnded)
        {
            Sequence sequence = DOTween.Sequence();

            sequence.AppendInterval(10f);
            sequence.AppendCallback(() => onCutSceneEnded?.Invoke());
        }
    }
}