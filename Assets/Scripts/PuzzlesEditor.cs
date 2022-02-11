using UnityEditor;
using UnityEngine;

public class PuzzlesEditor : EditorWindow
{
    private Vector2 ColumnOneScrollPos = new Vector2();
    private static int GapBetweenColumns = 20;
    private static Rect WindowRect = new Rect(10, 10, 900, 750);
    private static Rect ColumnOneRect = new Rect(0, 0, 200, WindowRect.height);
    private static Rect ColumnTwoRect = new Rect(ColumnOneRect.width + GapBetweenColumns, 0, WindowRect.width - (ColumnOneRect.width + GapBetweenColumns), WindowRect.height);

    PuzzleInfo CurrentPuzzle;

    [SerializeField]
    public static PuzzlesPool Pool = new PuzzlesPool();

    //private GameManager.PuzzleSizes PuzzSize;

    [MenuItem("Puzzixle/PuzzlesEditor &Z")]
    public static void ShowWindow()
    {
        var objectDB = Resources.Load<PuzzlesScriptableObject>("Puzzles");
        if (objectDB != null)
        {
            Pool = objectDB.PuzzlesPool;
        }

        PuzzlesEditor PuzzlesWindow = (PuzzlesEditor)EditorWindow.GetWindow(typeof(PuzzlesEditor));
        PuzzlesWindow.position = WindowRect;
        PuzzlesWindow.minSize = new Vector2(WindowRect.width, WindowRect.height);

        PuzzlesWindow.Show();
    }

    private void OnGUI()
    {
        DrawFirstColumn();
        DrawSecondColumn();
    }
    
    private void DrawFirstColumn()
    {
        GUILayout.BeginArea(ColumnOneRect);
        ColumnOneScrollPos = GUILayout.BeginScrollView(ColumnOneScrollPos);
        EditorGUILayout.BeginVertical("box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        EditorGUI.BeginDisabledGroup(false);

        GUILayout.Space(10);
        GUI.color = Color.blue;
        if (GUILayout.Button("Add Level"))
        {
            PuzzleInfo newLevel = new PuzzleInfo();
            newLevel.LevelName = "Untitled Level";
            Pool.PuzzlesList.Add(newLevel);
        }
        GUI.color = Color.white;
        GUILayout.Space(10);

        DrawListOfLevels();

        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndVertical();
        GUILayout.EndScrollView();
        GUILayout.EndArea();

    }

    private void DrawListOfLevels()
    {
        for(int i = 0; i < Pool.PuzzlesList.Count; i++)
        {
            if (CurrentPuzzle == Pool.PuzzlesList[i])
            {
                GUI.color = Color.cyan;
            }

            if (GUILayout.Button(Pool.PuzzlesList[i].LevelName))
            {
                CurrentPuzzle = Pool.PuzzlesList[i];
            }
            GUI.color = Color.white;
        }
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
                Pool.PuzzlesList.Remove(CurrentPuzzle);
                CurrentPuzzle = null;

                EditorGUI.EndDisabledGroup();
                EditorGUILayout.EndVertical();
                GUILayout.EndArea();

                return;
            }
        }

        if (GUILayout.Button("Generate"))
        {
            if (CurrentPuzzle.Map == null)
            {
                CurrentPuzzle.Map = new Cell[GetBoardSizeFromBoardEnum(CurrentPuzzle.Board), GetBoardSizeFromBoardEnum(CurrentPuzzle.Board)];
            }

            ManagersSingleton.Managers.PuzzleGenerator.Generate(ref CurrentPuzzle, GetBoardSizeFromBoardEnum(CurrentPuzzle.Board));
        }

        GUI.color = Color.white;
        GUILayout.Space(20);
        CurrentPuzzle.LevelName = EditorGUILayout.TextField("Level Name: ", CurrentPuzzle.LevelName);
        GUILayout.Space(20);
        CurrentPuzzle.Board = (Boards)EditorGUILayout.EnumPopup(CurrentPuzzle.Board);

        GUILayout.Space(30);

        switch (CurrentPuzzle.Board)
        {
            case Boards.Squ5:
                break;
            case Boards.Squ10:
                Draw10x10();
                break;
            case Boards.Squ15:
                break;
        }

        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndVertical();
        GUILayout.EndArea();
    }

    private void Draw10x10()
    {
        int cellWidth = 25;
        int cellHeight = 20;
        if (CurrentPuzzle.Map == null)
            CurrentPuzzle.Map = new Cell[GetBoardSizeFromBoardEnum(CurrentPuzzle.Board), GetBoardSizeFromBoardEnum(CurrentPuzzle.Board)];

        for (int row = 0; row < 10; row++)
        {
            GUILayout.BeginHorizontal("box");
            for (int col = 0; col < 10; col++)
            {
                if (CurrentPuzzle.Map[row, col] == null)
                    CurrentPuzzle.Map[row, col] = new Cell();

                if (CurrentPuzzle.Map[row, col].CellMode == CellModes.MarkedAsFull)
                {
                    GUI.color = Color.cyan;
                }
                else if (CurrentPuzzle.Map[row, col].CellMode == CellModes.NA)
                {
                    GUI.color = Color.black;
                }
                else
                {
                    GUI.color = Color.white;
                }

                if (GUILayout.Button("", GUILayout.MaxHeight(cellHeight), GUILayout.MaxWidth(cellWidth)))
                {
                    if (CurrentPuzzle.Map[row, col].CellMode == CellModes.MarkedAsFull)
                        CurrentPuzzle.Map[row, col].CellMode = CellModes.MarkedAsEmpty;

                    if (CurrentPuzzle.Map[row, col].CellMode == CellModes.MarkedAsEmpty)
                        CurrentPuzzle.Map[row, col].CellMode = CellModes.MarkedAsFull;
                }
            }
            GUILayout.EndHorizontal();
        }
    }

    private void ApplyChanges()
    {
        var puzzlesPool = Resources.Load<PuzzlesScriptableObject>("Rooms");
        if (puzzlesPool != null)
        {
            Pool = puzzlesPool.PuzzlesPool;
        }
        EditorUtility.SetDirty(puzzlesPool);
    }

    private int GetBoardSizeFromBoardEnum(Boards board)
    {
        switch (board)
        {
            case Boards.Squ5:
                return 5;
            case Boards.Squ10:
                return 10;
            case Boards.Squ15:
                return 15;
            default:
                return 10;
        }
    }

    //private void LoadMapsFromFile()
    //{
    //    //Texture2D[] assets = Resources.LoadAll("/BWTextures5x5", typeof(Texture2D)) as Texture2D[];
    //    PuzzSize = 0;
    //    int i = 0;
    //    while (true)
    //    {
    //        string path = $"/{PuzzSize.ToString()}/{i}/Map";
    //        Debug.Log(path);
    //        Texture2D loadedTexture = Resources.Load(path, typeof(Texture2D)) as Texture2D;
    //        if(loadedTexture == null)
    //        {
    //            if(PuzzSize == GameManager.PuzzleSizes.Big)
    //            {
    //                break;
    //            }
    //            PuzzSize++;
    //        }
    //        else
    //        {
    //            ProcessTexture(PuzzSize, loadedTexture, i);
    //        }
    //        i++;
    //    }
    //}

    //private void ProcessTexture(GameManager.PuzzleSizes size, Texture2D texture, int puzzInd)
    //{

    //}
}