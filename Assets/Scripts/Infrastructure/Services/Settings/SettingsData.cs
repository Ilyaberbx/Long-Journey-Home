using System;
using Infrastructure.Services.Settings.Audio;
using Infrastructure.Services.Settings.Mouse;
using Infrastructure.Services.Settings.Quality;
using Infrastructure.Services.Settings.Screen;

namespace Infrastructure.Services.Settings
{
    [Serializable]
    public class SettingsData
    {
        public AudioData Audio;
        public QualityData Quality;
        public ScreenData Screen;
        public MouseData Mouse;
        
        public SettingsData()
        {
            Audio = new AudioData();
            Quality = new QualityData();
            Screen = new ScreenData();
            Mouse = new MouseData();
        }
    }
}