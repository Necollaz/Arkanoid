using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MiniIT.ARKANOID
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class PlatformView : MonoBehaviour
    {
        private PlatformConfig   _platformConfig; 
        private PlatformMovement _platformMovement;
        private Slider           _movementSlider;
        private Camera           _mainCamera;
        private BoxCollider2D    _boxCollider;

        [Inject]
        private void Construct(PlatformConfig platformConfig, Slider movementSlider, Camera mainCamera)
        {
            _platformConfig = platformConfig;
            _movementSlider = movementSlider;
            _mainCamera = mainCamera;
        }

        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider2D>();
        
            _platformMovement = new PlatformMovement(_platformConfig, _movementSlider, transform, _boxCollider, _mainCamera);
        }

        private void Start()
        {
            _platformMovement?.ForceUpdateBounds();
        }

        private void Update()
        {
            _platformMovement?.Tick();
        }
    }
}