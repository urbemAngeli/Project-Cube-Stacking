using UnityEngine;

namespace ExtraAssets.Scripts.Environment
{
    public class WarpEffect : MonoBehaviour
    {
        public void Show() => 
            gameObject.SetActive(true);
        
        public void Hide() => 
            gameObject.SetActive(false);
    }
}