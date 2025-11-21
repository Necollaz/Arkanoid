using System.Collections.Generic;
using UnityEngine;

namespace MiniIT.ARKANOID
{
    [CreateAssetMenu(fileName = "LevelBricksConfig", menuName = "Game/Bricks/Level Bricks Config")]
    public class LevelBricksConfig : ScriptableObject
    {
        [Header("Grid")]
        [SerializeField, Min(1)] private int             rows = 6;
        [SerializeField, Min(1)] private int             columns = 10;
        [SerializeField] private Vector2                 cellSize = new Vector2(0.6f, 0.3f);
        [SerializeField] private Vector2                 gridOffsetFromCenter = Vector2.zero;

        [Header("Brick configs")]
        [SerializeField] private BrickConfig             indestructibleBrickConfig;
        [SerializeField] private BrickConfig             normalBrickConfig;
        [SerializeField] private BrickConfig             strongBrickConfig;

        [Header("Layout Mode")]
        [SerializeField] private BrickLayoutModeType     layoutMode = BrickLayoutModeType.Manual;

        [Header("Bricks variants")]
        [SerializeField] private List<BrickCellPosition> indestructibleBrickPositions =
            new List<BrickCellPosition>();
        [SerializeField] private List<BrickCellPosition> normalBrickPositions =
            new List<BrickCellPosition>();
        [SerializeField] private List<BrickCellPosition> strongBrickPositions =
            new List<BrickCellPosition>();

        [Header("Random layout (editor only)")]
        [SerializeField, Min(0)] private int             randomIndestructibleCount = 0;
        [SerializeField, Min(0)] private int             randomNormalCount = 0;
        [SerializeField, Min(0)] private int             randomStrongCount = 0;
        [SerializeField] private int                     randomSeed = 0;

        public Vector2 CellSize => cellSize;
        public Vector2 GridOffsetFromCenter => gridOffsetFromCenter;
        public int     Rows => rows;
        public int     Columns => columns;

        public IEnumerable<BrickCell> EnumerateBrickCells()
        {
            BrickSet[] sets =
            {
                new BrickSet(indestructibleBrickConfig, indestructibleBrickPositions),
                new BrickSet(normalBrickConfig, normalBrickPositions),
                new BrickSet(strongBrickConfig, strongBrickPositions)
            };

            foreach (BrickSet set in sets)
            {
                if (set.Config == null)
                {
                    continue;
                }

                foreach (BrickCellPosition position in set.Positions)
                {
                    if (TryCreateBrickCell(set.Config, position, out BrickCell cell))
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

            if (rowIndex < 0 || rowIndex >= rows || columnIndex < 0 || columnIndex >= columns)
            {
                cell = null;

                return false;
            }

            cell = new BrickCell(config, position);

            return true;
        }
    }
}