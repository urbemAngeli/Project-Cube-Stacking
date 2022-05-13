using ExtraAssets.Scripts.Infrastructure.Services;
using UnityEngine;

namespace ExtraAssets.Scripts.Services.AssetManagement
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string path, Vector3 at);
        GameObject Instantiate(string path);
        T Instantiate<T>(string path) where T : MonoBehaviour;

        T Load<T>(string path) where T : Object;
        T[] LoadAll<T>(string path) where T : Object;
    }
}