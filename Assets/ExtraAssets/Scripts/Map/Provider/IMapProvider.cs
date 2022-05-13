using ExtraAssets.Scripts.Infrastructure.Services;

namespace ExtraAssets.Scripts.Map.Provider
{
    public interface IMapProvider : IService
    {
        void Create();
        void Destruct();
    }
}