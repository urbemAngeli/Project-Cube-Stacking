using System;
using System.Collections.Generic;
using UnityEngine;

namespace ExtraAssets.Scripts.UI
{
    public class MenuPanelHolder : MonoBehaviour, IMenuPanels
    {
        public WaitingMenu WaitingMenu => _waitingMenu;

        public GameOverMenu GameOverMenu => _gameOverMenu;

        [SerializeField]
        private WaitingMenu _waitingMenu;

        [SerializeField]
        private GameOverMenu _gameOverMenu;

        private List<MenuPanel> _menuPanels;


        public void Initialize()
        {
            _menuPanels = new List<MenuPanel>();
            
            _menuPanels.Add(_waitingMenu);
            _menuPanels.Add(_gameOverMenu);

            HideAll();
        }

        public void Show<TPanel>() where TPanel : MenuPanel
        {
            Type targetPanel = typeof(TPanel);
            
            for (int i = 0; i < _menuPanels.Count; i++)
            {
                if (_menuPanels[i].GetType() == targetPanel)
                {
                    _menuPanels[i].Show();
                    continue;
                }
                
                _menuPanels[i].Hide();
            }
        }
        
        public void Hide<TPanel>() where TPanel : MenuPanel
        {
            Type targetPanel = typeof(TPanel);
            
            for (int i = 0; i < _menuPanels.Count; i++)
            {
                if (_menuPanels[i].GetType() == targetPanel)
                {
                    _menuPanels[i].Hide();
                    return;
                }
            }
        }

        private void HideAll()
        {
            for (int i = 0; i < _menuPanels.Count; i++)
                _menuPanels[i].Hide();
        }
    }
}