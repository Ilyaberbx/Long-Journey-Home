﻿using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Infrastructure.Services.AssetManagement
{
    public interface IAssetProvider : IService
    {
        Task<T> Load<T>(AssetReference assetReference) where T : class;
        void CleanUp();
        Task<T> Load<T>(string address) where T : class;
        Task<T[]> Load<T>(AssetReferenceT<T>[] assetReferenceArray) where T : UnityEngine.Object;
        Task<T> Load<T>(AssetReferenceT<T> assetReference) where T : UnityEngine.Object;
        Task Initialize();
        Task<IList<T>> LoadAll<T>(string address) where T : class;
    }
}