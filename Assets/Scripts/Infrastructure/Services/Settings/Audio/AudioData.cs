using System;

namespace Infrastructure.Services.Settings.Audio
{

    [Serializable]
    public class AudioData
    {
        public int GlobalVolume;
        public int MusicVolume;
        public int SoundsVolume;
    }
}