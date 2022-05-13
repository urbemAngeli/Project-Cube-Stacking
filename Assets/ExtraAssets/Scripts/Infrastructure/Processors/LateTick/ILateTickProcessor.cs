using ExtraAssets.Scripts.Infrastructure.Services;

namespace ExtraAssets.Scripts.Infrastructure.Processors.LateTick
{
    public interface ILateTickProcessor : IService
    {
        void Add(ILateTickable lateTick);
        void Remove(ILateTickable lateTick);
    }
}