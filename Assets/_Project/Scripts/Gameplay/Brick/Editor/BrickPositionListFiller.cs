using System.Collections.Generic;
using UnityEditor;
using MiniIT.ARKANOID;

public class BrickPositionListFiller
{
    public void Fill(SerializedProperty list, List<BrickCellPosition> source, ref int index, int count)
    {
        for (int i = 0; i < count && index < source.Count; i++, index++)
        {
            list.InsertArrayElementAtIndex(list.arraySize);
            SerializedProperty element = list.GetArrayElementAtIndex(list.arraySize - 1);

            SerializedProperty rowProp = element.FindPropertyRelative("rowIndex");
            SerializedProperty colProp = element.FindPropertyRelative("columnIndex");

            BrickCellPosition pos = source[index];
            rowProp.intValue = pos.RowIndexInspector;
            colProp.intValue = pos.ColumnIndexInspector;
        }
    }
}