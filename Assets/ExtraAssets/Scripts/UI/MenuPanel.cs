using UnityEngine;

namespace ExtraAssets.Scripts.UI
{
    public abstract class MenuPanel : MonoBehaviour
    {
        public void Show() => 
            gameObject.SetActive(true);

        public void Hide() => 
            gameObject.SetActive(false);
    }
}