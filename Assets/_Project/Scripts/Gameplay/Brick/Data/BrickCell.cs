using System;
using UnityEngine;

namespace MiniIT.ARKANOID
{
    [Serializable]
    public class BrickCell
    {
        [SerializeField] private BrickConfig       brickConfig;
        [SerializeField] private BrickCellPosition brickCellPosition;

        public BrickCell(BrickConfig brickConfig, BrickCellPosition brickCellPosition)
        {
            this.brickConfig = brickConfig;
            this.brickCellPosition = brickCellPosition;
        }
    
        public BrickConfig BrickConfig => brickConfig;
        public int         RowIndex => brickCellPosition.RowIndex;
        public int         ColumnIndex => brickCellPosition.ColumnIndex;
    }
}