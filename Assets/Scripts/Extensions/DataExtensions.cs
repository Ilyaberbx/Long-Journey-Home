using Data;
using Infrastructure.Services.Settings.Screen;
using UnityEngine;

namespace Extensions
{
    public static class DataExtensions
    {
        public static Vector3Data AsVector3Data(this Vector3 vector3)
            => new Vector3Data(vector3.x, vector3.y, vector3.z);

        public static Vector3 AsUnityVector(this Vector3Data vector3data)
            => new Vector3(vector3data.X, vector3data.Y, vector3data.Z);

        public static ResolutionData AsResolutionData(this Resolution resolution)
            => new ResolutionData(resolution.width, resolution.height);

        public static bool IsSameResolution(this ResolutionData firstResolution, ResolutionData secondResolution)
            => firstResolution.Width == secondResolution.Width && firstResolution.Height == secondResolution.Height;

        public static Resolution AsUnityResolution(this ResolutionData resolutionData)
        {
            Resolution resolution = new Resolution
            {
                width = resolutionData.Width,
                height = resolutionData.Height
            };

            return resolution;
        }

        public static T ToDeserialized<T>(this string json)
            => JsonUtility.FromJson<T>(json);

        public static string ToJson(this object obj)
            => JsonUtility.ToJson(obj);

        public static Vector3 AddY(this Vector3 vector3, float value)
            => new Vector3(vector3.x, vector3.y + value, vector3.z);

        public static Vector3 AddX(this Vector3 vector3, float value)
            => new Vector3(vector3.x + value, vector3.y, vector3.z);
        
        public static Vector3 AddZ(this Vector3 vector3, float value)
            => new Vector3(vector3.x, vector3.y, vector3.z + value);
    }
}