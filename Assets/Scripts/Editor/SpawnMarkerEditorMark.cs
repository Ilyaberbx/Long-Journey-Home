using Logic.Spawners;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(SpawnMarker))]
    public class SpawnMarkerEditorMark : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(SpawnMarker spawner, GizmoType type)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(spawner.transform.position,3);
            Gizmos.color = Color.white;
        }
    }
}