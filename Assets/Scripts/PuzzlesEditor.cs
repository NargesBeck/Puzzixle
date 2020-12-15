using UnityEditor;
using UnityEngine;

public class PuzzlesEditor : EditorWindow
{
    [SerializeField]
    public static PuzzlesPool Pool = new PuzzlesPool();

    private GameManager.PuzzleSizes PuzzSize;

    [MenuItem("Puzzixle/PuzzlesEditor &P")]
    public static void ShowWindow()
    {
        var objectDB = Resources.Load<PuzzlesScriptableObject>("Puzzles");
        if (objectDB != null)
        {
            Pool = objectDB.PuzzlesPool;
        }

        PuzzlesEditor PuzzlesWindow = (PuzzlesEditor)EditorWindow.GetWindow(typeof(PuzzlesEditor));
        Rect winSize = new Rect(100, 100, 400, 300);
        PuzzlesWindow.position = winSize;
        PuzzlesWindow.minSize = new Vector2(winSize.width, winSize.height);

        PuzzlesWindow.Show();
    }

    private void OnGUI()
    {
        GUILayout.Space(150);
        if(GUILayout.Button("Load And Process Everything"))
        {
            LoadMapsFromFile();
        }
    }

    private void LoadMapsFromFile()
    {
        //Texture2D[] assets = Resources.LoadAll("/BWTextures5x5", typeof(Texture2D)) as Texture2D[];
        PuzzSize = 0;
        int i = 0;
        while (true)
        {
            string path = $"/{PuzzSize.ToString()}/{i}/Map";
            Debug.Log(path);
            Texture2D loadedTexture = Resources.Load(path, typeof(Texture2D)) as Texture2D;
            if(loadedTexture == null)
            {
                if(PuzzSize == GameManager.PuzzleSizes.Big)
                {
                    break;
                }
                PuzzSize++;
            }
            else
            {
                ProcessTexture(PuzzSize, loadedTexture, i);
            }
            i++;
        }
    }

    private void ProcessTexture(GameManager.PuzzleSizes size, Texture2D texture, int puzzInd)
    {

    }
}