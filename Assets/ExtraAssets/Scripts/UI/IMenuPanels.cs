using ExtraAssets.Scripts.Infrastructure.Services;

namespace ExtraAssets.Scripts.UI
{
    public interface IMenuPanels : IService
    {
        void Show<TPanel>() where TPanel : MenuPanel;
        void Hide<TPanel>() where TPanel : MenuPanel;
    }
}