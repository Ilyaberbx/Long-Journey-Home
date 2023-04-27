using System;

namespace Infrastructure.Services.Settings.Screen
{
    [Serializable]
    public class ScreenData
    {
        public bool IsFullScreen;
        public ResolutionData Resolution;
    }
}