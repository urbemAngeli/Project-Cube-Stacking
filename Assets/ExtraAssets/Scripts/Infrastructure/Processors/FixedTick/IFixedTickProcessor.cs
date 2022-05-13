using ExtraAssets.Scripts.Infrastructure.Services;

namespace ExtraAssets.Scripts.Infrastructure.Processors.FixedTick
{
    public interface IFixedTickProcessor : IService
    {
        void Add(IFixedTickable fixedTick);
        void Remove(IFixedTickable fixedTick);
    }
}