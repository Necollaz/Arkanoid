using System.Collections.Generic;
using UnityEngine;

namespace MiniIT.ARKANOID
{
    [CreateAssetMenu(fileName = "LevelBricksConfig", menuName = "Game/Bricks/Level Bricks Config")]
    public class LevelBricksConfig : ScriptableObject
    {
        [Header("Grid")]
        [SerializeField, Min(1)] private int             _rows = 6;
        [SerializeField, Min(1)] private int             _columns = 10;
        [SerializeField] private Vector2                 _cellSize = new Vector2(0.6f, 0.3f);
        [SerializeField] private Vector2                 _gridOffsetFromCenter = Vector2.zero;

        [Header("Brick configs")]
        [SerializeField] private BrickConfig             _indestructibleBrickConfig;
        [SerializeField] private BrickConfig             _normalBrickConfig;
        [SerializeField] private BrickConfig             _strongBrickConfig;

        [Header("Layout Mode")]
        [SerializeField] private BrickLayoutModeType     _layoutMode = BrickLayoutModeType.Manual;

        [Header("Bricks variants")]
        [SerializeField] private List<BrickCellPosition> _indestructibleBrickPositions =
            new List<BrickCellPosition>();
        [SerializeField] private List<BrickCellPosition> _normalBrickPositions =
            new List<BrickCellPosition>();
        [SerializeField] private List<BrickCellPosition> _strongBrickPositions =
            new List<BrickCellPosition>();

        [Header("Random layout (editor only)")]
        [SerializeField, Min(0)] private int             _randomIndestructibleCount = 0;
        [SerializeField, Min(0)] private int             _randomNormalCount = 0;
        [SerializeField, Min(0)] private int             _randomStrongCount = 0;
        [SerializeField] private int                     _randomSeed = 0;

        public Vector2 CellSize => _cellSize;
        public Vector2 GridOffsetFromCenter => _gridOffsetFromCenter;
        public int     Rows => _rows;
        public int     Columns => _columns;

        public IEnumerable<BrickCell> EnumerateBrickCells()
        {
            if (_indestructibleBrickConfig != null)
            {
                foreach (BrickCellPosition position in _indestructibleBrickPositions)
                {
                    if (TryCreateBrickCell(_indestructibleBrickConfig, position, out BrickCell cell))
                    {
                        yield return cell;
                    }
                }
            }

            if (_normalBrickConfig != null)
            {
                foreach (BrickCellPosition position in _normalBrickPositions)
                {
                    if (TryCreateBrickCell(_normalBrickConfig, position, out BrickCell cell))
                    {
                        yield return cell;
                    }
                }
            }

            if (_strongBrickConfig != null)
            {
                foreach (BrickCellPosition position in _strongBrickPositions)
                {
                    if (TryCreateBrickCell(_strongBrickConfig, position, out BrickCell cell))
                    {
                        yield return cell;
                    }
                }
            }
        }

        private bool TryCreateBrickCell(BrickConfig config, BrickCellPosition position, out BrickCell cell)
        {
            int rowIndex = position.RowIndex;
            int columnIndex = position.ColumnIndex;

            if (rowIndex < 0 || rowIndex >= _rows || columnIndex < 0 || columnIndex >= _columns)
            {
                cell = null;

                return false;
            }

            cell = new BrickCell(config, position);

            return true;
        }
    }
}