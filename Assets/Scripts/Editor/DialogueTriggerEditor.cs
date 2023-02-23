using Logic.DialogueSystem;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(DialogueTrigger))]
    public class DialogueTriggerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(DialogueTrigger point, GizmoType type)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(point.transform.position,point.GetComponentInChildren<SphereCollider>().radius);
            Gizmos.color = Color.white;
        }
    }
}