using UnityEngine;

namespace MiniIT.ARKANOID
{
    public class BrickGridSpawner
    {
        private const float                                 ROW_DIRECTION = -1.0f;
        private const float                                 HALF_MULTIPLIER = 0.5f;

        private readonly LevelBricksConfig                  _config;
        private readonly GameSession                        _gameSession;
        private readonly GameplayAudio                      _gameplayAudio;
        private readonly ObjectPool<BrickView>              _brickPool;
        private readonly ObjectPool<BrickDestroyEffectView> _effectPool;
        private readonly Transform                          _parentTransform;

        public BrickGridSpawner(LevelBricksConfig config, ObjectPool<BrickView> brickPool,
            ObjectPool<BrickDestroyEffectView> effectPool, Transform parentTransform, GameSession gameSession,
            GameplayAudio gameplayAudio)
        {
            _config = config;
            _brickPool = brickPool;
            _effectPool = effectPool;
            _parentTransform = parentTransform;
            _gameSession = gameSession;
            _gameplayAudio = gameplayAudio;
        }

        public void SpawnBricks()
        {
            _gameSession.Reset();

            foreach (BrickCell brickCell in _config.EnumerateBrickCells())
            {
                BrickConfig brickConfig = brickCell.BrickConfig;

                if (brickConfig == null)
                {
                    continue;
                }

                _gameSession.RegisterBrick(brickConfig);

                SpawnBrick(brickCell);
            }
        }

        private void SpawnBrick(BrickCell brickCell)
        {
            if (brickCell.BrickConfig == null)
            {
                return;
            }

            BrickView brick = _brickPool.Get();
            Transform brickTransform = brick.transform;

            brickTransform.SetParent(_parentTransform, false);
            brickTransform.localPosition = ComputeLocalPosition(brickCell);

            brick.Initialize(brickCell.BrickConfig, _brickPool, _effectPool, _gameSession, _gameplayAudio,
                _config.CellSize);
            brick.gameObject.SetActive(true);
        }

        private Vector3 ComputeLocalPosition(BrickCell brickCell)
        {
            Vector2 origin = ComputeGridOrigin();
            Vector2 cellSize = _config.CellSize;

            float x = origin.x + brickCell.ColumnIndex * cellSize.x;
            float y = origin.y + brickCell.RowIndex * cellSize.y * ROW_DIRECTION;

            return new Vector3(x, y, 0.0f);
        }

        private Vector2 ComputeGridOrigin()
        {
            Vector2 cellSize = _config.CellSize;
            int rows = _config.Rows;
            int columns = _config.Columns;

            float totalWidth = (columns - 1) * cellSize.x;
            float totalHeight = (rows - 1) * cellSize.y;

            float originX = -totalWidth * HALF_MULTIPLIER;
            float originY = totalHeight * HALF_MULTIPLIER;

            Vector2 centeredOrigin = new Vector2(originX, originY);

            return centeredOrigin + _config.GridOffsetFromCenter;
        }
    }
}