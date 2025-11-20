using UnityEngine;

namespace MiniIT.ARKANOID
{
    [CreateAssetMenu(fileName = "BallConfig", menuName = "Game/Ball/Ball Config")]
    public class BallConfig : ScriptableObject
    {
        [Header("Collision")]
        [SerializeField] private LayerMask _collisionMask;
    
        [Header("Movement")]
        [SerializeField] private float     _ballSpeed = 10.0f;
        [SerializeField] private float     _minVerticalNormal = 0.2f;
    
        [Header("Launch")]
        [SerializeField] private float     _launchHorizontalRandomRange = 0.6f;
        [SerializeField] private float     _launchVerticalDirection = 1.0f;
    
        [Header("Platform bounce")]
        [SerializeField] private float     _platformHitMaxNormalizedX = 1.0f;
    
        public LayerMask CollisionMask => _collisionMask;
        public float     BallSpeed => _ballSpeed;
        public float     MinVerticalNormal => _minVerticalNormal;
        public float     LaunchHorizontalRandomRange => _launchHorizontalRandomRange;
        public float     LaunchVerticalDirection => _launchVerticalDirection;
        public float     PlatformHitMaxNormalizedX => _platformHitMaxNormalizedX;
    }
}