using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MiniIT.ARKANOID
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class PlatformView : MonoBehaviour
    {
        private PlatformConfig   platformConfig; 
        private PlatformMovement platformMovement;
        private Slider           movementSlider;
        private Camera           mainCamera;
        private BoxCollider2D    boxCollider;

        [Inject]
        private void Construct(PlatformConfig platformConfig, Slider movementSlider, Camera mainCamera)
        {
            this.platformConfig = platformConfig;
            this.movementSlider = movementSlider;
            this.mainCamera = mainCamera;
        }

        private void Awake()
        {
            boxCollider = GetComponent<BoxCollider2D>();
        
            platformMovement = new PlatformMovement(platformConfig, movementSlider, transform, boxCollider, mainCamera);
        }

        private void Start()
        {
            platformMovement?.ForceUpdateBounds();
        }

        private void Update()
        {
            platformMovement?.Tick();
        }
    }
}