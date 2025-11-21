using UnityEngine;

namespace MiniIT.ARKANOID
{
    [CreateAssetMenu(fileName = "WorldBoundsConfig", menuName = "Game/Bounds/World Bounds Config")]
    public class WorldBoundsConfig : ScriptableObject
    {
        [Header("Extra size (percent of canvas)")]
        [SerializeField, Min(0.0f)] private float horizontalExtraPercent = 0.065f;
        [SerializeField, Min(0.0f)] private float verticalExtraPercent = 0.1f;

        [Header("Wall thickness (UI units)")]
        [SerializeField, Min(1.0f)] private float horizontalWallThickness = 1.0f;
        [SerializeField, Min(1.0f)] private float verticalWallThickness = 1.0f;

        public float HorizontalExtraPercent => horizontalExtraPercent;
        public float VerticalExtraPercent => verticalExtraPercent;
        public float HorizontalWallThickness => horizontalWallThickness;
        public float VerticalWallThickness => verticalWallThickness;
    }
}