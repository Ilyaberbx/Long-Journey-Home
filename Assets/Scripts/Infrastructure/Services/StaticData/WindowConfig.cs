using System;
using UI.Elements;
using UI.Services.Window;

namespace Infrastructure.Services.StaticData
{
    [Serializable]
    public class WindowConfig 
    {
        public WindowType Type;
        public WindowBase Prefab;
    }
}