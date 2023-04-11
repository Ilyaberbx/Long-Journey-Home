using Data;
using UnityEngine;

namespace Extensions
{
    public static class DataExentions
    {
        public static Vector3Data AsVector3Data(this Vector3 vector3) 
            => new Vector3Data(vector3.x, vector3.y, vector3.z);

        public static Vector3 AsUnityVector(this Vector3Data vector3data)
            => new Vector3(vector3data.X, vector3data.Y, vector3data.Z);
        public static T ToDeserialized<T>(this string json) 
            => JsonUtility.FromJson<T>(json);

        public static string ToJson(this object obj)
            => JsonUtility.ToJson(obj);
        public static Vector3 AddY(this Vector3 vector3,float value)
            => new Vector3(vector3.x, vector3.y + value, vector3.z);
    }
}