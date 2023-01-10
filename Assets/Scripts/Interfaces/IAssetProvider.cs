using Interfaces;
using UnityEngine;

namespace ProjectSolitude.Interfaces
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 at);
    }
}