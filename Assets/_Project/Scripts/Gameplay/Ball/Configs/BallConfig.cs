using UnityEngine;

namespace MiniIT.ARKANOID
{
    [CreateAssetMenu(fileName = "BallConfig", menuName = "Game/Ball/Ball Config")]
    public class BallConfig : ScriptableObject
    {
        [Header("Collision")]
        [SerializeField] private LayerMask collisionMask;
    
        [Header("Movement")]
        [SerializeField] private float     ballSpeed = 10.0f;
        [SerializeField] private float     minVerticalNormal = 0.2f;
    
        [Header("Launch")]
        [SerializeField] private float     launchHorizontalRandomRange = 0.6f;
        [SerializeField] private float     launchVerticalDirection = 1.0f;
    
        [Header("Platform bounce")]
        [SerializeField] private float     platformHitMaxNormalizedX = 1.0f;
    
        public LayerMask CollisionMask => collisionMask;
        public float     BallSpeed => ballSpeed;
        public float     MinVerticalNormal => minVerticalNormal;
        public float     LaunchHorizontalRandomRange => launchHorizontalRandomRange;
        public float     LaunchVerticalDirection => launchVerticalDirection;
        public float     PlatformHitMaxNormalizedX => platformHitMaxNormalizedX;
    }
}