using UnityEngine;

namespace ProjectSolitude.Extensions
{
    public static class PhysicsDebug
    {
        public static void DrawDebug(Vector3 worldPos,float radius,float duration)
        {
            Debug.DrawRay(worldPos,radius * Vector3.up,Color.blue, duration);
            Debug.DrawRay(worldPos,radius * Vector3.down,Color.blue, duration);
            Debug.DrawRay(worldPos,radius * Vector3.forward,Color.blue, duration);
            Debug.DrawRay(worldPos,radius * Vector3.back,Color.blue, duration);
        }
    }
}