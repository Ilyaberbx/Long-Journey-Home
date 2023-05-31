using Logic.Spawners;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(BaseMarker))]
    public class SpawnMarkerEditorMark : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(BaseMarker spawner, GizmoType type)
        {
            Gizmos.color = spawner.IndicatorColor;
            Gizmos.DrawSphere(spawner.transform.position,50);
            Gizmos.color = Color.white;
        }
    }
}