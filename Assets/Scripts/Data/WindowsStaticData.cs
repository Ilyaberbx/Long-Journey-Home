using System.Collections.Generic;
using Infrastructure.Services.StaticData;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "WindowsData",menuName = "StaticData/UI/Windows")]
    public class WindowsStaticData : ScriptableObject
    {
        public List<WindowConfig> Configs;
    }
}