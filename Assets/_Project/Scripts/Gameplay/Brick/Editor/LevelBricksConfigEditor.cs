using UnityEditor;
using UnityEngine;
using MiniIT.ARKANOID;

[CustomEditor(typeof(LevelBricksConfig))]
public class LevelBricksConfigEditor : Editor
{
    private BrickPositionListFiller    _positionFiller;
    private BrickRandomLayoutGenerator _layoutGenerator;
    
    private SerializedProperty         _rowsProperty;
    private SerializedProperty         _columnsProperty;
    private SerializedProperty         _cellSizeProperty;
    private SerializedProperty         _gridOffsetProperty;

    private SerializedProperty         _indestructibleConfigProperty;
    private SerializedProperty         _normalConfigProperty;
    private SerializedProperty         _strongConfigProperty;

    private SerializedProperty         _layoutModeProperty;

    private SerializedProperty         _indestructiblePositionsProperty;
    private SerializedProperty         _normalPositionsProperty;
    private SerializedProperty         _strongPositionsProperty;

    private SerializedProperty         _randomIndCountProperty;
    private SerializedProperty         _randomNormalCountProperty;
    private SerializedProperty         _randomStrongCountProperty;
    private SerializedProperty         _randomSeedProperty;

    private void OnEnable()
    {
        _rowsProperty = serializedObject.FindProperty("_rows");
        _columnsProperty = serializedObject.FindProperty("_columns");
        _cellSizeProperty = serializedObject.FindProperty("_cellSize");
        _gridOffsetProperty = serializedObject.FindProperty("_gridOffsetFromCenter");

        _indestructibleConfigProperty = serializedObject.FindProperty("_indestructibleBrickConfig");
        _normalConfigProperty = serializedObject.FindProperty("_normalBrickConfig");
        _strongConfigProperty = serializedObject.FindProperty("_strongBrickConfig");

        _layoutModeProperty = serializedObject.FindProperty("_layoutMode");

        _indestructiblePositionsProperty = serializedObject.FindProperty("_indestructibleBrickPositions");
        _normalPositionsProperty = serializedObject.FindProperty("_normalBrickPositions");
        _strongPositionsProperty = serializedObject.FindProperty("_strongBrickPositions");

        _randomIndCountProperty = serializedObject.FindProperty("_randomIndestructibleCount");
        _randomNormalCountProperty = serializedObject.FindProperty("_randomNormalCount");
        _randomStrongCountProperty = serializedObject.FindProperty("_randomStrongCount");
        _randomSeedProperty = serializedObject.FindProperty("_randomSeed");
        
        _positionFiller = new BrickPositionListFiller();
        _layoutGenerator = new BrickRandomLayoutGenerator(_positionFiller);
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawGridSection();
        DrawBrickConfigsSection();

        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(_layoutModeProperty, new GUIContent(BrickEditorConstants.LABEL_LAYOUT_MODE));

        BrickLayoutModeType mode = (BrickLayoutModeType)_layoutModeProperty.enumValueIndex;

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
        EditorGUILayout.PropertyField(_rowsProperty);
        EditorGUILayout.PropertyField(_columnsProperty);
        EditorGUILayout.PropertyField(_cellSizeProperty);
        EditorGUILayout.PropertyField(_gridOffsetProperty);
        EditorGUILayout.Space();
    }

    private void DrawBrickConfigsSection()
    {
        EditorGUILayout.LabelField(BrickEditorConstants.LABEL_BRICK_CONFIGS, EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_indestructibleConfigProperty);
        EditorGUILayout.PropertyField(_normalConfigProperty);
        EditorGUILayout.PropertyField(_strongConfigProperty);
        EditorGUILayout.Space();
    }

    private void DrawManualLayout()
    {
        EditorGUILayout.LabelField(BrickEditorConstants.LABEL_MANUAL, EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_indestructiblePositionsProperty, true);
        EditorGUILayout.PropertyField(_normalPositionsProperty, true);
        EditorGUILayout.PropertyField(_strongPositionsProperty, true);
    }


    private void DrawRandomLayout()
    {
        LevelBricksConfig config = (LevelBricksConfig)target;

        int rows = Mathf.Max(1, _rowsProperty.intValue);
        int columns = Mathf.Max(1, _columnsProperty.intValue);
        int totalCells = rows * columns;

        EditorGUILayout.LabelField(BrickEditorConstants.LABEL_RANDOM, EditorStyles.boldLabel);
        EditorGUILayout.HelpBox("Укажи количество кирпичей каждого типа. " +
                                "Сумма должна точно совпадать с количеством ячеек сетки (rows * columns).", 
            MessageType.Info);
        
        ClampProperty(_randomIndCountProperty, totalCells);
        ClampProperty(_randomNormalCountProperty, totalCells);
        ClampProperty(_randomStrongCountProperty, totalCells);

        int totalBricks = _randomIndCountProperty.intValue + _randomNormalCountProperty.intValue +
                          _randomStrongCountProperty.intValue;
        
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
        
        DrawSimpleSlider(_randomIndCountProperty, BrickEditorConstants.SLIDER_INDESTRUCTIBLE, totalCells);
        DrawSimpleSlider(_randomNormalCountProperty, BrickEditorConstants.SLIDER_NORMAL, totalCells);
        DrawSimpleSlider(_randomStrongCountProperty, BrickEditorConstants.SLIDER_STRONG, totalCells);

        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(_randomSeedProperty, new GUIContent("Random Seed (0 = random)"));
        EditorGUILayout.Space();
        
        EditorGUI.BeginDisabledGroup(totalBricks != totalCells);
        
        if (GUILayout.Button(BrickEditorConstants.BUTTON_GENERATE))
        {
            _layoutGenerator.Generate(config, rows, columns, totalCells, _indestructiblePositionsProperty,
                _normalPositionsProperty, _strongPositionsProperty, _randomIndCountProperty.intValue,
                _randomNormalCountProperty.intValue, _randomStrongCountProperty.intValue, _randomSeedProperty.intValue);
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