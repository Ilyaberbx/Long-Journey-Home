using System;
using UI.Elements;
using UI.Services.Window;
using UnityEngine.AddressableAssets;

namespace Infrastructure.Services.StaticData
{
    [Serializable]
    public class WindowConfig 
    {
        public WindowType Type;
        public AssetReferenceGameObject Prefab;
    }
}