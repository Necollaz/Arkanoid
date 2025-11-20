using UnityEngine;

namespace MiniIT.ARKANOID
{
    [CreateAssetMenu(fileName = "WorldBoundsConfig", menuName = "Game/Bounds/World Bounds Config")]
    public class WorldBoundsConfig : ScriptableObject
    {
        [Header("Extra size (percent of canvas)")]
        [SerializeField, Min(0.0f)] private float _horizontalExtraPercent = 0.1f;
        [SerializeField, Min(0.0f)] private float _verticalExtraPercent = 0.1f;

        [Header("Wall thickness (UI units)")]
        [SerializeField, Min(1.0f)] private float _horizontalWallThickness = 1.0f;
        [SerializeField, Min(1.0f)] private float _verticalWallThickness = 1.0f;

        public float HorizontalExtraPercent => _horizontalExtraPercent;
        public float VerticalExtraPercent => _verticalExtraPercent;
        public float HorizontalWallThickness => _horizontalWallThickness;
        public float VerticalWallThickness => _verticalWallThickness;
    }
}