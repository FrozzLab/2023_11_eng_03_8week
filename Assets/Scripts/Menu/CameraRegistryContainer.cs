using UnityEngine;

namespace Menu
{
    public class CameraRegistryContainer : MonoBehaviour
    {
        public static CameraRegistryContainer Instance;
        
        public ViewType defaultView;
        public CameraRegistry[] cameraRegistries;
        
        private void Awake()    
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}