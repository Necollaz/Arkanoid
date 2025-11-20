using System;
using System.Collections.Generic;
using UnityEditor;
using MiniIT.ARKANOID;

public class BrickRandomLayoutGenerator
{
    private readonly BrickPositionListFiller _filler;

    public BrickRandomLayoutGenerator(BrickPositionListFiller filler)
    {
        _filler = filler;
    }

    public void Generate(LevelBricksConfig config, int rows, int columns, int totalCells,
        SerializedProperty indestructibleList, SerializedProperty normalList, SerializedProperty strongList,
        int countInd, int countNormal, int countStrong, int seed)
    {
        List<BrickCellPosition> all = new List<BrickCellPosition>(totalCells);

        for (int row = 1; row <= rows; row++)
        {
            for (int column = 1; column <= columns; column++)
            {
                all.Add(new BrickCellPosition(row, column));
            }
        }

        Random random = seed == 0 ? new Random() : new Random(seed);

        for (int i = all.Count - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            (all[i], all[j]) = (all[j], all[i]);
        }

        indestructibleList.ClearArray();
        normalList.ClearArray();
        strongList.ClearArray();

        int index = 0;

        _filler.Fill(indestructibleList, all, ref index, countInd);
        _filler.Fill(normalList, all, ref index, countNormal);
        _filler.Fill(strongList, all, ref index, countStrong);

        EditorUtility.SetDirty(config);
    }
}