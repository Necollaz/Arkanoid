using UnityEngine;
using UnityEngine.UI;

namespace MiniIT.ARKANOID
{
    public class PlatformMovement
    {
        private const float HALF_MULTIPLIER = 0.5f;

        private readonly PlatformConfig config;
        private readonly Slider         movementSlider;
        private readonly Camera         mainCamera;
        private readonly Transform      platformTransform;
        private readonly BoxCollider2D  platformCollider;

        private float                   minWorldX;
        private float                   maxWorldX;
        private float                   targetWorldY;

        public PlatformMovement(
            PlatformConfig config,
            Slider movementSlider,
            Transform platformTransform,
            BoxCollider2D platformCollider,
            Camera mainCamera)
        {
            this.config = config;
            this.movementSlider = movementSlider;
            this.platformTransform = platformTransform;
            this.platformCollider = platformCollider;
            this.mainCamera = mainCamera;

            RecalculateBounds();
            CenterPlatform();
        }

        public void ForceUpdateBounds()
        {
            RecalculateBounds();
            CenterPlatform();
        }

        public void Tick()
        {
            MovePlatform();
        }

        private void RecalculateBounds()
        {
            float platformWidth = platformCollider.bounds.size.x;
            float halfPlatformWidth = platformWidth * HALF_MULTIPLIER;

            float cameraHeight = mainCamera.orthographicSize * 2.0f;
            float cameraWidth = cameraHeight * mainCamera.aspect;
            float halfCameraWidth = cameraWidth * 0.5f;

            Vector3 cameraPosition = mainCamera.transform.position;

            float worldLeft = cameraPosition.x - halfCameraWidth;
            float worldRight = cameraPosition.x + halfCameraWidth;

            float paddingWorld = Mathf.Max(config.ScreenPadding, 0.0f);

            minWorldX = worldLeft + halfPlatformWidth + paddingWorld;
            maxWorldX = worldRight - halfPlatformWidth - paddingWorld;

            float minY = cameraPosition.y - cameraHeight * HALF_MULTIPLIER;
            float maxY = cameraPosition.y + cameraHeight * HALF_MULTIPLIER;
            targetWorldY = Mathf.Lerp(minY, maxY, config.VerticalViewportPosition);
        }

        private void CenterPlatform()
        {
            float defaultT = Mathf.Clamp01(config.DefaultNormalizedPosition);
            float centerX = Mathf.Lerp(minWorldX, maxWorldX, defaultT);

            Vector3 position = platformTransform.position;
            position.x = Mathf.Clamp(centerX, minWorldX, maxWorldX);
            position.y = targetWorldY;

            platformTransform.position = position;
        }

        private void MovePlatform()
        {
            if (movementSlider == null)
            {
                return;
            }

            float sliderValue = movementSlider.value;
            float inverseLerp = Mathf.InverseLerp(movementSlider.minValue, movementSlider.maxValue, sliderValue);
            inverseLerp = Mathf.Clamp01(inverseLerp);

            float targetX = Mathf.Lerp(minWorldX, maxWorldX, inverseLerp);
            targetX = Mathf.Clamp(targetX, minWorldX, maxWorldX);

            Vector3 position = platformTransform.position;
            position.x = targetX;
            position.y = targetWorldY;

            platformTransform.position = position;
        }
    }
}