using Logic;
using Logic.Triggers;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(SaveTrigger))]
    public class SaveTriggerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(SaveTrigger point, GizmoType type)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(point.transform.position,point.GetComponentInChildren<SphereCollider>().radius);
            Gizmos.color = Color.white;
        }
    }
}