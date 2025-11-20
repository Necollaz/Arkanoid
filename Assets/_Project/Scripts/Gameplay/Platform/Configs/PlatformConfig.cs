using UnityEngine;

namespace MiniIT.ARKANOID
{
    [CreateAssetMenu(fileName = "PlatformConfig", menuName = "Game/Platform/Platform Config")]
    public class PlatformConfig : ScriptableObject
    {
        [Header("Horizontal padding")]
        [SerializeField] private float                    _screenPadding = 0.0f;

        [Header("Vertical position")]
        [SerializeField, Range(0.0f, 1.0f)] private float _verticalViewportPosition = 0.1f;

        [Header("Input")]
        [SerializeField, Range(0.0f, 1.0f)] private float _defaultNormalizedPosition = 0.5f;
    
        public float ScreenPadding => _screenPadding;
        public float VerticalViewportPosition => _verticalViewportPosition;
        public float DefaultNormalizedPosition => _defaultNormalizedPosition;
    }
}