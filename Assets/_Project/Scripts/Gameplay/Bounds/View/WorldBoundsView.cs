using UnityEngine;
using Zenject;

namespace MiniIT.ARKANOID
{
    public class WorldBoundsView : MonoBehaviour
    {
        private WorldBounds worldBounds;
        private Camera      mainCamera;
        private float       lastAspect;

        [Inject]
        private void Construct(
            WorldBounds worldBounds,
            Camera mainCamera)
        {
            this.worldBounds = worldBounds;
            this.mainCamera = mainCamera;
        }

        private void Start()
        {
            if (mainCamera != null)
            {
                lastAspect = mainCamera.aspect;
            }
            
            worldBounds?.ForceUpdate();
        }
        
        private void Update()
        {
            if (mainCamera == null)
            {
                return;
            }

            float currentAspect = mainCamera.aspect;

            if (!Mathf.Approximately(currentAspect, lastAspect))
            {
                lastAspect = currentAspect;
                worldBounds.ForceUpdate();
            }
        }
    }
}