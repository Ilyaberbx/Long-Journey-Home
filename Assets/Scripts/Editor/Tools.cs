using UnityEditor;
using UnityEngine;

namespace ProjectSolitude.Editor
{
    public static class Tools
    {
        [MenuItem("Tools/ClearPrefs")]
        public static void ClearAll()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}
