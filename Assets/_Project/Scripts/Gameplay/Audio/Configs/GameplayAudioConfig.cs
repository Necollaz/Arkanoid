using UnityEngine;

namespace MiniIT.ARKANOID
{
    [CreateAssetMenu(fileName = "GameplayAudioConfig", menuName = "Game/Audio/Gameplay Audio Config")]
    public class GameplayAudioConfig : ScriptableObject
    {
        [Header("Ball collisions")]
        [SerializeField] private AudioClip ballHitBrickClip;
        [SerializeField] private AudioClip ballHitWallOrPlatformClip;

        [Header("Bricks")]
        [SerializeField] private AudioClip brickDestroyedClip;

        [Header("UI")]
        [SerializeField] private AudioClip winMenuClip;
        [SerializeField] private AudioClip loseMenuClip;

        public AudioClip BallHitBrickClip => ballHitBrickClip;
        public AudioClip BallHitWallOrPlatformClip => ballHitWallOrPlatformClip;
        public AudioClip BrickDestroyedClip => brickDestroyedClip;
        public AudioClip WinMenuClip => winMenuClip;
        public AudioClip LoseMenuClip => loseMenuClip;
    }
}