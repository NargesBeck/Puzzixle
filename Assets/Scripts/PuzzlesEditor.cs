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

    [MenuItem("Puzzixle/PuzzlesEditor &P")]
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

        }

        if (GUILayout.Button("Generate"))
        {
            if (CurrentPuzzle.Map == null && CurrentPuzzle.Size != 0)
            {
                CurrentPuzzle.Map = new bool[CurrentPuzzle.Size, CurrentPuzzle.Size];
            }

            CurrentPuzzle.Map = ManagersSingleton.Managers.PuzzleGenerator.Generate(CurrentPuzzle.Size);
        }

        GUI.color = Color.white;
        GUILayout.Space(20);
        CurrentPuzzle.LevelName = EditorGUILayout.TextField("Level Name: ", CurrentPuzzle.LevelName);
        GUILayout.Space(20);
        CurrentPuzzle.Size = EditorGUILayout.IntField("Size: ", CurrentPuzzle.Size);
        GUILayout.Space(30);

        switch (CurrentPuzzle.Size)
        {
            case 5:
                break;
            case 10:
                Draw10x10(ref CurrentPuzzle.Map);
                break;
            case 15:
                break;
        }

        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndVertical();
        GUILayout.EndArea();
    }

    private void Draw10x10(ref bool[,] puzzle)
    {
        int cellWidth = 25;
        int cellHeight = 20;
        if (puzzle == null)
            puzzle = new bool[10, 10];

        for (int row = 0; row < 10; row++)
        {
            GUILayout.BeginHorizontal("box");
            for (int col = 0; col < 10; col++)
            {
                if (puzzle[row, col])
                {
                    GUI.color = Color.cyan;
                }
                else
                {
                    GUI.color = Color.white;
                }

                if (GUILayout.Button("", GUILayout.MaxHeight(cellHeight), GUILayout.MaxWidth(cellWidth)))
                {
                    puzzle[row, col] = !puzzle[row, col];
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