using System;

namespace Infrastructure.Services.Settings.Mouse
{
    [Serializable]
    public class MouseData
    {
         public float Sensitivity;
         public Action OnSensitivityChanged;
    }
}