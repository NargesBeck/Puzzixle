using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class PuzzlesEditor : EditorWindow
{
    private Vector2 BoardsScrollPos = new Vector2();
    private Vector2 LevelsScrollPos = new Vector2();

    private static int GapBetweenColumns = 20;
    private static Rect WindowRect = new Rect(10, 10, 500, 700);
    private static Rect ColumnOneUpperRect = new Rect(0, 0, 200, 345);
    private static Rect ColumnOneBottomRect = new Rect(0, 350, 200, 325);
    private static Rect ColumnTwoRect = new Rect(ColumnOneUpperRect.width + GapBetweenColumns, 0, WindowRect.width - (ColumnOneUpperRect.width + GapBetweenColumns), WindowRect.height);

    PuzzlesPoolForOneBoard CurrentBoardPool;
    PuzzleInfo CurrentPuzzle;

    [SerializeField]
    public static List<PuzzlesPoolForOneBoard> PuzzlesPool = new List<PuzzlesPoolForOneBoard>();

    //private GameManager.PuzzleSizes PuzzSize;

    [MenuItem("Puzzixle/PuzzlesEditor &Z")]
    public static void ShowWindow()
    {
        var objectDB = Resources.Load<PuzzlesScriptableObject>("Puzzles");
        if (objectDB != null)
        {
            PuzzlesPool = objectDB.PuzzlesPool;
        }

        PuzzlesEditor PuzzlesWindow = (PuzzlesEditor)EditorWindow.GetWindow(typeof(PuzzlesEditor));
        PuzzlesWindow.position = WindowRect;
        PuzzlesWindow.minSize = new Vector2(WindowRect.width, WindowRect.height);

        PuzzlesWindow.Show();
    }

    private void OnGUI()
    {
        Apply();
        DrawFirstColumn();
        DrawSecondColumn();
    }

    private void Apply()
    {
        var objectDB = Resources.Load<PuzzlesScriptableObject>("Puzzles");
        if (objectDB != null)
        {
            EditorUtility.SetDirty(objectDB);
            PuzzlesPool = objectDB.PuzzlesPool;
        }
    }
    
    private void DrawFirstColumn()
    {
        DrawListOfBoards();
        DrawListOfLevels();
    }

    private void DrawListOfBoards()
    {
        GUILayout.BeginArea(ColumnOneUpperRect);
        BoardsScrollPos = GUILayout.BeginScrollView(BoardsScrollPos);
        EditorGUILayout.BeginVertical("box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        EditorGUI.BeginDisabledGroup(false);

        GUILayout.Space(10);
        GUILayout.Label("Boards:");

        for (int iBoard = 0; iBoard < PuzzlesPool.Count; iBoard++)
        {
            if (CurrentBoardPool == PuzzlesPool[iBoard])
            {
                GUI.color = Color.cyan;
            }

            if (GUILayout.Button(PuzzlesPool[iBoard].BoardType.ToString()))
            {
                CurrentBoardPool = PuzzlesPool[iBoard];
                CurrentPuzzle = null;
            }
            GUI.color = Color.white;
        }

        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndVertical();
        GUILayout.EndScrollView();
        GUILayout.EndArea();
    }

    private void DrawListOfLevels()
    {
        if (CurrentBoardPool == null)
            return;

        GUILayout.BeginArea(ColumnOneBottomRect);
        LevelsScrollPos = GUILayout.BeginScrollView(LevelsScrollPos);
        EditorGUILayout.BeginVertical("box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        EditorGUI.BeginDisabledGroup(false);

        GUI.color = Color.cyan;
        if (GUILayout.Button("Generate 100"))
        {
            for (int iAppendedLevel = 0; iAppendedLevel < 100; iAppendedLevel++)
            {
                PuzzleInfo puzzle = new PuzzleInfo();
                if (puzzle.Map1D == null)
                {
                    int boardSize = PuzzlePageManager.GetBoardSizeFromBoardEnum(CurrentBoardPool.BoardType);
                    puzzle.Map1D = new Cell[boardSize * boardSize];
                }
                CurrentBoardPool.PuzzlesList.Add(puzzle);
                ManagersSingleton.Managers.PuzzleGenerator.Generate(ref puzzle, PuzzlePageManager.GetBoardSizeFromBoardEnum(CurrentBoardPool.BoardType));
            }
        }
        GUI.color = Color.white;

        for (int iLevel = 0; iLevel < CurrentBoardPool.PuzzlesList.Count; iLevel++)
        {
            GUI.color = (CurrentBoardPool.PuzzlesList[iLevel] == CurrentPuzzle) ? Color.cyan : Color.white;

            if (GUILayout.Button($"Level {iLevel + 1}"))
            {
                CurrentPuzzle = CurrentBoardPool.PuzzlesList[iLevel];
            }
        }
        GUI.color = Color.white;

        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndVertical();
        GUILayout.EndScrollView();
        GUILayout.EndArea();
    }

    private void DrawSecondColumn()
    {
        if (CurrentPuzzle == null)
            return;

        GUILayout.BeginArea(ColumnTwoRect);
        EditorGUILayout.BeginVertical("box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        EditorGUI.BeginDisabledGroup(false);

        GUILayout.Space(10);

        GUI.color = Color.blue;
        if (GUILayout.Button("Delete"))
        {
            if (CurrentPuzzle != null)
            {
                CurrentBoardPool.PuzzlesList.Remove(CurrentPuzzle);
                CurrentPuzzle = null;

                EditorGUI.EndDisabledGroup();
                EditorGUILayout.EndVertical();
                GUILayout.EndArea();

                return;
            }
        }

        GUI.color = Color.white;
        GUILayout.Space(20);

        if (CurrentPuzzle.LevelName == null || CurrentPuzzle.LevelName == "")
            CurrentPuzzle.LevelName = $"Level {CurrentBoardPool.PuzzlesList.IndexOf(CurrentPuzzle)}";
        CurrentPuzzle.LevelName = EditorGUILayout.TextField("Level Name: ", CurrentPuzzle.LevelName);

        GUILayout.Space(30);

        switch (CurrentBoardPool.BoardType)
        {
            case BoardTypes.Squ5:
                Draw5x5();
                break;
            case BoardTypes.Squ10:
                Draw10x10();
                break;
            case BoardTypes.Squ15:
                Draw15x15();
                break;
        }

        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndVertical();
        GUILayout.EndArea();
    }

    private void Draw15x15()
    {
        int cellWidth = 17;
        int cellHeight = 13;
        int boardSize = PuzzlePageManager.GetBoardSizeFromBoardEnum(CurrentBoardPool.BoardType);
        if (CurrentPuzzle.Map1D == null)
            CurrentPuzzle.Map1D = new Cell[boardSize * boardSize];

        DrawSquareBoardCells(boardSize, cellWidth, cellHeight);
    }

    private void Draw10x10()
    {
        int cellWidth = 25;
        int cellHeight = 20;
        int boardSize = PuzzlePageManager.GetBoardSizeFromBoardEnum(CurrentBoardPool.BoardType);
        if (CurrentPuzzle.Map1D == null)
            CurrentPuzzle.Map1D = new Cell[boardSize * boardSize];

        DrawSquareBoardCells(boardSize, cellWidth, cellHeight);
    }

    private void Draw5x5()
    {
        int cellWidth = 50;
        int cellHeight = 40;
        int boardSize = PuzzlePageManager.GetBoardSizeFromBoardEnum(CurrentBoardPool.BoardType);
        if (CurrentPuzzle.Map1D == null)
            CurrentPuzzle.Map1D = new Cell[boardSize * boardSize];

        DrawSquareBoardCells(boardSize, cellWidth, cellHeight);
    }

    private void DrawSquareBoardCells(int boardSize, int cellWidth, int cellHeight)
    {
        int index = 0;
        for (int row = 0; row < boardSize; row++)
        {
            GUILayout.BeginHorizontal("box");
            for (int col = 0; col < boardSize; col++)
            {
                if (CurrentPuzzle.Map1D[index] == null)
                    CurrentPuzzle.Map1D[index] = new Cell();

                if (CurrentPuzzle.Map1D[index].CellMode == CellModes.MarkedAsFull)
                {
                    GUI.color = Color.cyan;
                }
                else if (CurrentPuzzle.Map1D[index].CellMode == CellModes.NA)
                {
                    GUI.color = Color.black;
                }
                else
                {
                    GUI.color = Color.white;
                }

                if (GUILayout.Button("", GUILayout.MaxHeight(cellHeight), GUILayout.MaxWidth(cellWidth)))
                {
                    CurrentPuzzle.Map1D[index].CellMode = (CurrentPuzzle.Map1D[index].CellMode == CellModes.MarkedAsFull) ?
                        CellModes.MarkedAsEmpty : CellModes.MarkedAsFull;
                }
                index++;
            }
            GUILayout.EndHorizontal();
        }
    }
}