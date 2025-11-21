using UnityEngine;
using Zenject;

namespace MiniIT.ARKANOID
{
    public class BrickSpawnerView : MonoBehaviour
    {
        private BrickGridSpawner spawner;

        [Inject]
        private void Construct(
            LevelBricksConfig config,
            ObjectPool<BrickView> brickPool,
            ObjectPool<BrickDestroyEffectView> effectPool,
            GameSession gameSession,
            GameplayAudio gameplayAudio)
        {
            spawner = new BrickGridSpawner(
                config,
                brickPool,
                effectPool,
                transform,
                gameSession,
                gameplayAudio);
        }

        private void Start()
        {
            spawner?.SpawnBricks();
        }
    }
}