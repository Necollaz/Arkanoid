using UnityEditor;
using UnityEngine;
using MiniIT.ARKANOID;

[CustomEditor(typeof(LevelBricksConfig))]
public class LevelBricksConfigEditor : Editor
{
    private BrickPositionListFiller    positionFiller;
    private BrickRandomLayoutGenerator layoutGenerator;
    
    private SerializedProperty         rowsProperty;
    private SerializedProperty         columnsProperty;
    private SerializedProperty         cellSizeProperty;
    private SerializedProperty         gridOffsetProperty;

    private SerializedProperty         indestructibleConfigProperty;
    private SerializedProperty         normalConfigProperty;
    private SerializedProperty         strongConfigProperty;

    private SerializedProperty         layoutModeProperty;

    private SerializedProperty         indestructiblePositionsProperty;
    private SerializedProperty         normalPositionsProperty;
    private SerializedProperty         strongPositionsProperty;

    private SerializedProperty         randomIndCountProperty;
    private SerializedProperty         randomNormalCountProperty;
    private SerializedProperty         randomStrongCountProperty;
    private SerializedProperty         randomSeedProperty;

    private void OnEnable()
    {
        rowsProperty = serializedObject.FindProperty("rows");
        columnsProperty = serializedObject.FindProperty("columns");
        cellSizeProperty = serializedObject.FindProperty("cellSize");
        gridOffsetProperty = serializedObject.FindProperty("gridOffsetFromCenter");

        indestructibleConfigProperty = serializedObject.FindProperty("indestructibleBrickConfig");
        normalConfigProperty = serializedObject.FindProperty("normalBrickConfig");
        strongConfigProperty = serializedObject.FindProperty("strongBrickConfig");

        layoutModeProperty = serializedObject.FindProperty("layoutMode");

        indestructiblePositionsProperty = serializedObject.FindProperty("indestructibleBrickPositions");
        normalPositionsProperty = serializedObject.FindProperty("normalBrickPositions");
        strongPositionsProperty = serializedObject.FindProperty("strongBrickPositions");

        randomIndCountProperty = serializedObject.FindProperty("randomIndestructibleCount");
        randomNormalCountProperty = serializedObject.FindProperty("randomNormalCount");
        randomStrongCountProperty = serializedObject.FindProperty("randomStrongCount");
        randomSeedProperty = serializedObject.FindProperty("randomSeed");
        
        positionFiller = new BrickPositionListFiller();
        layoutGenerator = new BrickRandomLayoutGenerator(positionFiller);
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawGridSection();
        DrawBrickConfigsSection();

        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(layoutModeProperty, new GUIContent(BrickEditorConstants.LABEL_LAYOUT_MODE));

        BrickLayoutModeType mode = (BrickLayoutModeType)layoutModeProperty.enumValueIndex;

        EditorGUILayout.Space();

        if (mode == BrickLayoutModeType.Manual)
        {
            DrawManualLayout();
        }
        else
        {
            DrawRandomLayout();
        }

        serializedObject.ApplyModifiedProperties();
    }


    private void DrawGridSection()
    {
        EditorGUILayout.LabelField(BrickEditorConstants.LABEL_GRID, EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(rowsProperty);
        EditorGUILayout.PropertyField(columnsProperty);
        EditorGUILayout.PropertyField(cellSizeProperty);
        EditorGUILayout.PropertyField(gridOffsetProperty);
        EditorGUILayout.Space();
    }

    private void DrawBrickConfigsSection()
    {
        EditorGUILayout.LabelField(BrickEditorConstants.LABEL_BRICK_CONFIGS, EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(indestructibleConfigProperty);
        EditorGUILayout.PropertyField(normalConfigProperty);
        EditorGUILayout.PropertyField(strongConfigProperty);
        EditorGUILayout.Space();
    }

    private void DrawManualLayout()
    {
        EditorGUILayout.LabelField(BrickEditorConstants.LABEL_MANUAL, EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(indestructiblePositionsProperty, true);
        EditorGUILayout.PropertyField(normalPositionsProperty, true);
        EditorGUILayout.PropertyField(strongPositionsProperty, true);
    }


    private void DrawRandomLayout()
    {
        LevelBricksConfig config = (LevelBricksConfig)target;

        int rows = Mathf.Max(1, rowsProperty.intValue);
        int columns = Mathf.Max(1, columnsProperty.intValue);
        int totalCells = rows * columns;

        EditorGUILayout.LabelField(BrickEditorConstants.LABEL_RANDOM, EditorStyles.boldLabel);
        EditorGUILayout.HelpBox("Укажи количество кирпичей каждого типа. " +
                                "Сумма должна точно совпадать с количеством ячеек сетки (rows * columns).", 
            MessageType.Info);
        
        ClampProperty(randomIndCountProperty, totalCells);
        ClampProperty(randomNormalCountProperty, totalCells);
        ClampProperty(randomStrongCountProperty, totalCells);

        int totalBricks = randomIndCountProperty.intValue + randomNormalCountProperty.intValue +
                          randomStrongCountProperty.intValue;
        
        EditorGUILayout.LabelField($"Total bricks: {totalBricks} / {totalCells}", totalBricks == totalCells ?
            EditorStyles.label : EditorStyles.boldLabel);

        if (totalBricks != totalCells)
        {
            EditorGUILayout.HelpBox(
                "Сумма Indestructible + Normal + Strong должна равняться rows * columns. " +
                "Измени значения выше, чтобы они совпали.",
                MessageType.Error);
        }

        EditorGUILayout.Space();
        
        DrawSimpleSlider(randomIndCountProperty, BrickEditorConstants.SLIDER_INDESTRUCTIBLE, totalCells);
        DrawSimpleSlider(randomNormalCountProperty, BrickEditorConstants.SLIDER_NORMAL, totalCells);
        DrawSimpleSlider(randomStrongCountProperty, BrickEditorConstants.SLIDER_STRONG, totalCells);

        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(randomSeedProperty, new GUIContent("Random Seed (0 = random)"));
        EditorGUILayout.Space();
        
        EditorGUI.BeginDisabledGroup(totalBricks != totalCells);
        
        if (GUILayout.Button(BrickEditorConstants.BUTTON_GENERATE))
        {
            layoutGenerator.Generate(config, rows, columns, totalCells, indestructiblePositionsProperty,
                normalPositionsProperty, strongPositionsProperty, randomIndCountProperty.intValue,
                randomNormalCountProperty.intValue, randomStrongCountProperty.intValue, randomSeedProperty.intValue);
        }
        
        EditorGUI.EndDisabledGroup();
    }


    private void DrawSimpleSlider(SerializedProperty property, string label, int totalCells)
    {
        EditorGUI.BeginChangeCheck();
        int newValue = EditorGUILayout.IntSlider(label, property.intValue, 0, totalCells);
        
        if (EditorGUI.EndChangeCheck())
        {
            property.intValue = Mathf.Clamp(newValue, 0, totalCells);
        }
    }

    private void ClampProperty(SerializedProperty property, int totalCells)
    {
        int value = property.intValue;
        
        if (value < 0)
        {
            property.intValue = 0;
        }
        else if (value > totalCells)
        {
            property.intValue = totalCells;
        }
    }
}