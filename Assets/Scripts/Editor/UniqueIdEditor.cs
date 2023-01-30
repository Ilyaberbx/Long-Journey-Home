using System;
using System.Linq;
using Logic.Spawners;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(UniqueId))]
    public class UniqueIdEditor : UnityEditor.Editor
    {
        private void OnEnable()
        {
            var uniqueId = (UniqueId)target;

            if (IsPrefab(uniqueId))
                return;
            
            
            if (string.IsNullOrEmpty(uniqueId.Id))
                Generate(uniqueId);
            else
            {
                UniqueId[] uniqueIds = FindObjectsOfType<UniqueId>();

                if (HaveSameIds(uniqueIds, uniqueId))
                    Generate(uniqueId);
            }
        }

        private bool IsPrefab(UniqueId uniqueId)
            => uniqueId.gameObject.scene.rootCount == 0;

        private static bool HaveSameIds(UniqueId[] uniqueIds, UniqueId uniqueId) 
            => uniqueIds.Any(other => other != null && other != uniqueId && other.Id == uniqueId.Id);


        private void Generate(UniqueId uniqueId)
        {
            uniqueId.Id = $"{uniqueId.gameObject.scene.name}_{Guid.NewGuid().ToString()}";

            if (Application.isPlaying) return;
            
            EditorUtility.SetDirty(uniqueId);
            EditorSceneManager.MarkSceneDirty(uniqueId.gameObject.scene);
        }
    }
}