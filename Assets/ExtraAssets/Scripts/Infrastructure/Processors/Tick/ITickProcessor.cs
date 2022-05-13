using ExtraAssets.Scripts.Infrastructure.Services;

namespace ExtraAssets.Scripts.Infrastructure.Processors.Tick
{
    public interface ITickProcessor : IService
    {
        void Add(ITickable tick);
        void Remove(ITickable tick);
    }
}