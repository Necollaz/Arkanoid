using UnityEngine;

namespace MiniIT.ARKANOID
{
    public class WorldBounds
    {
        private readonly WorldBoundsConfig config;

        private readonly Transform         topWall;
        private readonly Transform         bottomWall;
        private readonly Transform         leftWall;
        private readonly Transform         rightWall;

        private readonly BoxCollider2D     topCollider;
        private readonly BoxCollider2D     bottomCollider;
        private readonly BoxCollider2D     leftCollider;
        private readonly BoxCollider2D     rightCollider;

        private readonly Camera            mainCamera;

        public WorldBounds(WorldBoundsConfig config, Camera mainCamera, Transform topWall, Transform bottomWall,
            Transform leftWall, Transform rightWall)
        {
            this.config = config;
            this.mainCamera = mainCamera;

            this.topWall = topWall;
            this.bottomWall = bottomWall;
            this.leftWall = leftWall;
            this.rightWall = rightWall;

            topCollider = topWall.GetComponent<BoxCollider2D>();
            bottomCollider = bottomWall.GetComponent<BoxCollider2D>();
            leftCollider = leftWall.GetComponent<BoxCollider2D>();
            rightCollider = rightWall.GetComponent<BoxCollider2D>();
        }

        public void ForceUpdate()
        {
            UpdateBounds();
        }

        private void UpdateBounds()
        {
            float camHeight = mainCamera.orthographicSize * 2f;
            float camWidth = camHeight * mainCamera.aspect;

            float extraWidth = camWidth * config.HorizontalExtraPercent;
            float extraHeight = camHeight * config.VerticalExtraPercent;

            float finalWidth = camWidth + extraWidth;
            float finalHeight = camHeight + extraHeight;

            float halfWidth = finalWidth * 0.5f;
            float halfHeight = finalHeight * 0.5f;

            Vector3 cameraPosition = mainCamera.transform.position;

            SetWall(leftWall, leftCollider, new Vector2(config.VerticalWallThickness, finalHeight),
                new Vector3(cameraPosition.x - halfWidth, cameraPosition.y, 0), false);
            SetWall(rightWall, rightCollider, new Vector2(config.VerticalWallThickness, finalHeight),
                new Vector3(cameraPosition.x + halfWidth, cameraPosition.y, 0), false);
            SetWall(topWall, topCollider, new Vector2(finalWidth, config.HorizontalWallThickness),
                new Vector3(cameraPosition.x, cameraPosition.y + halfHeight, 0), false);
            SetWall(bottomWall, bottomCollider, new Vector2(finalWidth, config.HorizontalWallThickness),
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