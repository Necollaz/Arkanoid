using UnityEngine;

namespace MiniIT.ARKANOID
{
    public class WorldBounds
    {
        private readonly WorldBoundsConfig _config;

        private readonly Transform     _topWall;
        private readonly Transform     _bottomWall;
        private readonly Transform     _leftWall;
        private readonly Transform     _rightWall;

        private readonly BoxCollider2D _topCollider;
        private readonly BoxCollider2D _bottomCollider;
        private readonly BoxCollider2D _leftCollider;
        private readonly BoxCollider2D _rightCollider;

        private readonly Camera        _mainCamera;

        public WorldBounds(WorldBoundsConfig config, Camera mainCamera, Transform topWall, Transform bottomWall,
            Transform leftWall, Transform rightWall)
        {
            _config = config;
            _mainCamera = mainCamera;

            _topWall = topWall;
            _bottomWall = bottomWall;
            _leftWall = leftWall;
            _rightWall = rightWall;

            _topCollider = topWall.GetComponent<BoxCollider2D>();
            _bottomCollider = bottomWall.GetComponent<BoxCollider2D>();
            _leftCollider = leftWall.GetComponent<BoxCollider2D>();
            _rightCollider = rightWall.GetComponent<BoxCollider2D>();
        }

        public void ForceUpdate()
        {
            UpdateBounds();
        }

        private void UpdateBounds()
        {
            float camHeight = _mainCamera.orthographicSize * 2f;
            float camWidth = camHeight * _mainCamera.aspect;

            float extraWidth = camWidth * _config.HorizontalExtraPercent;
            float extraHeight = camHeight * _config.VerticalExtraPercent;

            float finalWidth = camWidth + extraWidth;
            float finalHeight = camHeight + extraHeight;

            float halfWidth = finalWidth * 0.5f;
            float halfHeight = finalHeight * 0.5f;

            Vector3 cameraPosition = _mainCamera.transform.position;

            SetWall(_leftWall, _leftCollider, new Vector2(_config.VerticalWallThickness, finalHeight),
                new Vector3(cameraPosition.x - halfWidth, cameraPosition.y, 0), false);
            SetWall(_rightWall, _rightCollider, new Vector2(_config.VerticalWallThickness, finalHeight),
                new Vector3(cameraPosition.x + halfWidth, cameraPosition.y, 0), false);
            SetWall(_topWall, _topCollider, new Vector2(finalWidth, _config.HorizontalWallThickness),
                new Vector3(cameraPosition.x, cameraPosition.y + halfHeight, 0), false);
            SetWall(_bottomWall, _bottomCollider, new Vector2(finalWidth, _config.HorizontalWallThickness),
                new Vector3(cameraPosition.x, cameraPosition.y - halfHeight, 0), true);
        }

        private void SetWall(Transform wall, BoxCollider2D collider, Vector2 size, Vector3 position, bool isTrigger)
        {
            wall.position = position;
            collider.size = size;
            collider.isTrigger = isTrigger;
        }
    }
}