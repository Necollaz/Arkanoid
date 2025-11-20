using UnityEngine;

namespace MiniIT.ARKANOID
{
    public class GameplayAudio
    {
        private readonly GameplayAudioConfig _config;
        private readonly AudioSource         _audioSource;

        public GameplayAudio(AudioSource audioSource, GameplayAudioConfig config)
        {
            _audioSource = audioSource;
            _config = config;
        }

        public void PlayBallHitBrick()
        {
            PlayOneShot(_config.BallHitBrickClip);
        }

        public void PlayBallHitWallOrPlatform()
        {
            PlayOneShot(_config.BallHitWallOrPlatformClip);
        }

        public void PlayBrickDestroyed()
        {
            PlayOneShot(_config.BrickDestroyedClip);
        }

        public void PlayWinMenu()
        {
            PlayOneShot(_config.WinMenuClip);
        }

        public void PlayLoseMenu()
        {
            PlayOneShot(_config.LoseMenuClip);
        }
    
        private void PlayOneShot(AudioClip clip)
        {
            if (clip == null || _audioSource == null)
            {
                return;
            }

            _audioSource.PlayOneShot(clip);
        }
    }
}