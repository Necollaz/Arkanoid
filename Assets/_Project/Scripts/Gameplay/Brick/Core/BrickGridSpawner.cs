using UnityEngine;

namespace MiniIT.ARKANOID
{
    public class BrickGridSpawner
    {
        private const float                                 ROW_DIRECTION = -1.0f;
        private const float                                 HALF_MULTIPLIER = 0.5f;

        private readonly LevelBricksConfig                  config;
        private readonly GameSession                        gameSession;
        private readonly GameplayAudio                      gameplayAudio;
        private readonly ObjectPool<BrickView>              brickPool;
        private readonly ObjectPool<BrickDestroyEffectView> effectPool;
        private readonly Transform                          parentTransform;

        public BrickGridSpawner(
            LevelBricksConfig config,
            ObjectPool<BrickView> brickPool,
            ObjectPool<BrickDestroyEffectView> effectPool,
            Transform parentTransform,
            GameSession gameSession,
            GameplayAudio gameplayAudio)
        {
            this.config = config;
            this.brickPool = brickPool;
            this.effectPool = effectPool;
            this.parentTransform = parentTransform;
            this.gameSession = gameSession;
            this.gameplayAudio = gameplayAudio;
        }

        public void SpawnBricks()
        {
            gameSession.Reset();

            foreach (BrickCell brickCell in config.EnumerateBrickCells())
            {
                BrickConfig brickConfig = brickCell.BrickConfig;

                if (brickConfig == null)
                {
                    continue;
                }

                gameSession.RegisterBrick(brickConfig);

                SpawnBrick(brickCell);
            }
        }

        private void SpawnBrick(BrickCell brickCell)
        {
            if (brickCell.BrickConfig == null)
            {
                return;
            }

            BrickView brick = brickPool.Get();
            Transform brickTransform = brick.transform;

            brickTransform.SetParent(parentTransform, false);
            brickTransform.localPosition = ComputeLocalPosition(brickCell);

            brick.Initialize(
                brickCell.BrickConfig,
                brickPool,
                effectPool,
                gameSession,
                gameplayAudio,
                config.CellSize);
            brick.gameObject.SetActive(true);
        }

        private Vector3 ComputeLocalPosition(BrickCell brickCell)
        {
            Vector2 origin = ComputeGridOrigin();
            Vector2 cellSize = config.CellSize;

            float x = origin.x + brickCell.ColumnIndex * cellSize.x;
            float y = origin.y + brickCell.RowIndex * cellSize.y * ROW_DIRECTION;

            return new Vector3(x, y, 0.0f);
        }

        private Vector2 ComputeGridOrigin()
        {
            Vector2 cellSize = config.CellSize;
            int rows = config.Rows;
            int columns = config.Columns;

            float totalWidth = (columns - 1) * cellSize.x;
            float totalHeight = (rows - 1) * cellSize.y;

            float originX = -totalWidth * HALF_MULTIPLIER;
            float originY = totalHeight * HALF_MULTIPLIER;

            Vector2 centeredOrigin = new Vector2(originX, originY);

            return centeredOrigin + config.GridOffsetFromCenter;
        }
    }
}