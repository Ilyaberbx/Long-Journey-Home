using Logic.Shatter;
using UnityEngine;

namespace Extensions
{
    public static class GameObjectExtenstion
    {
        public static ShatteringObject Shatter(this GameObject selfObject, ShatteringObject shatteringObjectPrefab,
            bool isHideSelf,
            Transform container = null)
        {
            ShatteringObject shattered = Object.Instantiate(shatteringObjectPrefab, selfObject.transform.position,
                selfObject.transform.rotation, container);
            shattered.Shatter();

            if (isHideSelf)
                selfObject.SetActive(false);

            return shattered;
        }
    }
}