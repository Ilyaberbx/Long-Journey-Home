using System;

namespace Infrastructure.Services.Settings.Screen
{

    [Serializable]
    public class ResolutionData
    {
        public int Width;
        public int Height;

        public ResolutionData(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}