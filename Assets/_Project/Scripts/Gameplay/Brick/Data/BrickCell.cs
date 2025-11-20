using System;
using UnityEngine;

namespace MiniIT.ARKANOID
{
    [Serializable]
    public class BrickCell
    {
        [SerializeField] private BrickConfig       _brickConfig;
        [SerializeField] private BrickCellPosition _brickCellPosition;

        public BrickCell(BrickConfig brickConfig, BrickCellPosition brickCellPosition)
        {
            _brickConfig = brickConfig;
            _brickCellPosition = brickCellPosition;
        }
    
        public BrickConfig BrickConfig => _brickConfig;
        public int         RowIndex => _brickCellPosition.RowIndex;
        public int         ColumnIndex => _brickCellPosition.ColumnIndex;
    }
}