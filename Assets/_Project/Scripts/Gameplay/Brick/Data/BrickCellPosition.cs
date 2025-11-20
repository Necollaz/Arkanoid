using System;
using UnityEngine;

namespace MiniIT.ARKANOID
{
    [Serializable]
    public struct BrickCellPosition
    {
        [SerializeField, Min(1)] private int _rowIndex;
        [SerializeField, Min(1)] private int _columnIndex;

        public BrickCellPosition(int rowIndexInspector, int columnIndexInspector)
        {
            _rowIndex = Mathf.Max(1, rowIndexInspector);
            _columnIndex = Mathf.Max(1, columnIndexInspector);
        }
    
        public int RowIndex => _rowIndex - 1;
        public int ColumnIndex => _columnIndex - 1;
        public int RowIndexInspector => _rowIndex;
        public int ColumnIndexInspector => _columnIndex;
    }
}