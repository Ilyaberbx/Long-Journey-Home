using System.Linq;
using Data;
using Logic.Spawners;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Editor
{
    [CustomEditor(typeof(LevelData))]
    public class LevelDataEditor : UnityEditor.Editor
    {
        private const string CollectLabel = "Collect";

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            LevelData levelData = (LevelData)target;

            if (GUILayout.Button(CollectLabel))
            {
                levelData.EnemySpawners = FindObjectsOfType<SpawnMarker>()
                    .Select(x => new EnemySpawnerData(x.GetComponent<UniqueId>().Id,x.EnemyType,x.transform.position))
                    .ToList();

                levelData.LevelKey = SceneManager.GetActiveScene().name;
            }
            
            EditorUtility.SetDirty(target);
        }
    }
}