using UnityEngine;

namespace MiniIT.ARKANOID
{
    [CreateAssetMenu(fileName = "GameplayAudioConfig", menuName = "Game/Audio/Gameplay Audio Config")]
    public class GameplayAudioConfig : ScriptableObject
    {
        [Header("Ball collisions")]
        [SerializeField] private AudioClip _ballHitBrickClip;
        [SerializeField] private AudioClip _ballHitWallOrPlatformClip;

        [Header("Bricks")]
        [SerializeField] private AudioClip _brickDestroyedClip;

        [Header("UI")]
        [SerializeField] private AudioClip _winMenuClip;
        [SerializeField] private AudioClip _loseMenuClip;

        public AudioClip BallHitBrickClip => _ballHitBrickClip;
        public AudioClip BallHitWallOrPlatformClip => _ballHitWallOrPlatformClip;
        public AudioClip BrickDestroyedClip => _brickDestroyedClip;
        public AudioClip WinMenuClip => _winMenuClip;
        public AudioClip LoseMenuClip => _loseMenuClip;
    }
}