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
        private const string PlayerInitPointTag = "PlayerInitPoint";

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            LevelData levelData = (LevelData)target;

            if (GUILayout.Button(CollectLabel))
            {
                levelData.EnemySpawners = FindObjectsOfType<EnemyMarker>()
                    .Select(x => new EnemySpawnerData(x.GetComponent<UniqueId>().Id,x.EnemyType,x.transform.position))
                    .ToList();
                
                levelData.LootSpawners= FindObjectsOfType<LootMarker>()
                    .Select(x => new LootSpawnerData(x.GetComponent<UniqueId>().Id,x.Prefab,x.transform.position,x.transform.rotation))
                    .ToList();

                levelData.PlayerInitPoint = GameObject.FindGameObjectWithTag(PlayerInitPointTag).transform.position;
                levelData.LevelKey = SceneManager.GetActiveScene().name;
            }
            
            EditorUtility.SetDirty(target);
        }
    }
}