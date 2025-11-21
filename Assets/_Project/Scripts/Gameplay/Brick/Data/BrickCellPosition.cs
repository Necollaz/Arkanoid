using System;
using UnityEngine;

namespace MiniIT.ARKANOID
{
    [Serializable]
    public struct BrickCellPosition
    {
        [SerializeField, Min(1)] private int rowIndex;
        [SerializeField, Min(1)] private int columnIndex;

        public BrickCellPosition(int rowIndexInspector, int columnIndexInspector)
        {
            rowIndex = Mathf.Max(1, rowIndexInspector);
            columnIndex = Mathf.Max(1, columnIndexInspector);
        }
    
        public int RowIndex => rowIndex - 1;
        public int ColumnIndex => columnIndex - 1;
        public int RowIndexInspector => rowIndex;
        public int ColumnIndexInspector => columnIndex;
    }
}