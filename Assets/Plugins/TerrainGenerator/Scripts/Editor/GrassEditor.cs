using UnityEngine;
using UnityEditor;
using Assets.Scripts.MapGenerator.Generators;

[CustomEditor(typeof(GrassGenerator))]
class GrassEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        var gen = (GrassGenerator)target;

        if (DrawDefaultInspector() && gen.GrassTextures.Count > 0)
        {
            if (gen.AutoUpdate)
            {
                gen.Generate();
            }
        }

        if (gen.GrassTextures.Count > 0)
        {
            if (GUILayout.Button("Generate"))
            {
                gen.Generate();
            }
        }

        if (GUILayout.Button("Clear"))
        {
            gen.Clear();
        }
    }
}
