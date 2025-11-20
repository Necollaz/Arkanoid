using UnityEngine;
using UnityEngine.UI;

namespace MiniIT.ARKANOID
{
    public class PlatformMovement
    {
        private const float HALF_MULTIPLIER = 0.5f;

        private readonly PlatformConfig _config;
        private readonly Slider         _movementSlider;
        private readonly Camera         _mainCamera;
        private readonly Transform      _platformTransform;
        private readonly BoxCollider2D  _platformCollider;

        private float                   _minWorldX;
        private float                   _maxWorldX;
        private float                   _targetWorldY;

        public PlatformMovement(PlatformConfig config, Slider movementSlider, Transform platformTransform,
            BoxCollider2D platformCollider, Camera mainCamera)
        {
            _config = config;
            _movementSlider = movementSlider;
            _platformTransform = platformTransform;
            _platformCollider = platformCollider;
            _mainCamera = mainCamera;

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
            float platformWidth = _platformCollider.bounds.size.x;
            float halfPlatformWidth = platformWidth * HALF_MULTIPLIER;

            float cameraHeight = _mainCamera.orthographicSize * 2.0f;
            float cameraWidth = cameraHeight * _mainCamera.aspect;
            float halfCameraWidth = cameraWidth * 0.5f;

            Vector3 cameraPosition = _mainCamera.transform.position;

            float worldLeft = cameraPosition.x - halfCameraWidth;
            float worldRight = cameraPosition.x + halfCameraWidth;

            float paddingWorld = Mathf.Max(_config.ScreenPadding, 0.0f);

            _minWorldX = worldLeft + halfPlatformWidth + paddingWorld;
            _maxWorldX = worldRight - halfPlatformWidth - paddingWorld;

            float minY = cameraPosition.y - cameraHeight * HALF_MULTIPLIER;
            float maxY = cameraPosition.y + cameraHeight * HALF_MULTIPLIER;
            _targetWorldY = Mathf.Lerp(minY, maxY, _config.VerticalViewportPosition);
        }

        private void CenterPlatform()
        {
            float defaultT = Mathf.Clamp01(_config.DefaultNormalizedPosition);
            float centerX = Mathf.Lerp(_minWorldX, _maxWorldX, defaultT);

            Vector3 position = _platformTransform.position;
            position.x = Mathf.Clamp(centerX, _minWorldX, _maxWorldX);
            position.y = _targetWorldY;

            _platformTransform.position = position;
        }

        private void MovePlatform()
        {
            if (_movementSlider == null)
            {
                return;
            }

            float sliderValue = _movementSlider.value;
            float inverseLerp = Mathf.InverseLerp(_movementSlider.minValue, _movementSlider.maxValue, sliderValue);
            inverseLerp = Mathf.Clamp01(inverseLerp);

            float targetX = Mathf.Lerp(_minWorldX, _maxWorldX, inverseLerp);
            targetX = Mathf.Clamp(targetX, _minWorldX, _maxWorldX);

            Vector3 position = _platformTransform.position;
            position.x = targetX;
            position.y = _targetWorldY;

            _platformTransform.position = position;
        }
    }
}