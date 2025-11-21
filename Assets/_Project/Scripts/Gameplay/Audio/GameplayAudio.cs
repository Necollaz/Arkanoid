using UnityEngine;

namespace MiniIT.ARKANOID
{
    public class GameplayAudio
    {
        private readonly GameplayAudioConfig config;
        private readonly AudioSource         audioSource;

        public GameplayAudio(AudioSource audioSource, GameplayAudioConfig config)
        {
            this.audioSource = audioSource;
            this.config = config;
        }

        public void PlayBallHitBrick()
        {
            PlayOneShot(config.BallHitBrickClip);
        }

        public void PlayBallHitWallOrPlatform()
        {
            PlayOneShot(config.BallHitWallOrPlatformClip);
        }

        public void PlayBrickDestroyed()
        {
            PlayOneShot(config.BrickDestroyedClip);
        }

        public void PlayWinMenu()
        {
            PlayOneShot(config.WinMenuClip);
        }

        public void PlayLoseMenu()
        {
            PlayOneShot(config.LoseMenuClip);
        }
    
        private void PlayOneShot(AudioClip clip)
        {
            if (clip == null || audioSource == null)
            {
                return;
            }

            audioSource.PlayOneShot(clip);
        }
    }
}