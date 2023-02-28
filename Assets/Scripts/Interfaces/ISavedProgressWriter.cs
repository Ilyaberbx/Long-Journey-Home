using System;
using Data;

namespace Interfaces
{
    public interface ISavedProgressWriter : ISavedProgressReader
    {
        void UpdateProgress(PlayerProgress progress);

    }
}