using Base;
using UnityEditor;
using UnityEngine;

namespace EDITOR
{
   public class LevelEditorWindow : EditorWindow
{
    private int gridWidth = 2;
    private int gridHeight = 2;
    private float cellSize = .5f;

    private GameObject[] prefabToPlace;

    [MenuItem("Window/Level Generator")]
    public static void ShowWindow()
    {
        GetWindow<LevelEditorWindow>("Level Generator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Grid Settings", EditorStyles.boldLabel);

        gridWidth = EditorGUILayout.IntField("Grid Width", gridWidth);
        gridHeight = EditorGUILayout.IntField("Grid Height", gridHeight);
        cellSize = EditorGUILayout.FloatField("Cell Size", cellSize);

        EditorGUILayout.Space();

        GUILayout.Label("Prefab Placement", EditorStyles.boldLabel);

        EnsurePrefabToPlaceSize(gridWidth * gridHeight);

        EditorGUILayout.BeginVertical();
        for (var y = 0; y < gridHeight; y++)
        {
            EditorGUILayout.BeginHorizontal();
            for (var x = 0; x < gridWidth; x++)
            {
                var index = y * gridWidth + x;
                prefabToPlace[index] = (GameObject)EditorGUILayout.ObjectField("", prefabToPlace[index], typeof(GameObject), false, GUILayout.Width(60), GUILayout.Height(60));
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space();

        if (GUILayout.Button("Generate Level"))
        {
            GenerateLevel();
        }
    }

    private void EnsurePrefabToPlaceSize(int size)
    {
        if (prefabToPlace == null || prefabToPlace.Length != size)
        {
            prefabToPlace = new GameObject[size];
        }
    }

    private void GenerateLevel()
    {
        var gameScene = FindObjectOfType<GameScene>();

        var _objPos = new Transform[gridWidth][];
        for (var index = 0; index < gridWidth; index++)
        {
            _objPos[index] = new Transform[gridHeight];
        }

        var startX = +(gridWidth * cellSize) / 2 + cellSize / 2; 
        var startY = +(gridHeight * cellSize) / 2 + cellSize / 2;

        for (var x = 0; x < gridWidth; x++)
        {
            for (var y = 0; y < gridHeight; y++)
            {
                var index = y * gridWidth + x;
                var prefab = prefabToPlace[index];
                var position = new Vector2(startX + x * cellSize, startY + y * cellSize);

                if (prefab == null) continue;
                Instantiate(prefab, position, Quaternion.identity, gameScene.transform);
                _objPos[x][y] = prefab.transform;
            }
        }
    }
}


}
