using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class _ForTestingStuff : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<string> namesToChange = NamesToChange();
        CellSpriteManager[] cellSpriteManagers = FindObjectsOfType<CellSpriteManager>();
        for (int i = 0; i < cellSpriteManagers.Length; i++)
        {
            for(int iNames = 0; iNames < namesToChange.Count; iNames++)
            {
                if (cellSpriteManagers[i].name == namesToChange[iNames])
                {
                    cellSpriteManagers[i].name = ChangeName(cellSpriteManagers[i].name);
                    break;
                }
            }
        }
     
        
    }

    List<string> NamesToChange()
    {
        List<string> namesToChange = new List<string>();
        for (int iParanthesis = 2; iParanthesis < 10; iParanthesis++)
        {
            for (int iCol = 0; iCol < 10; iCol++)
            {
                string s = $"{iCol} ({iParanthesis})";
                namesToChange.Add(s);
            }
        }
        return namesToChange;
    }

    string ChangeName(string input)
    {
        string output = $"{input.Substring(3, 1)} {input.Substring(0, 1)}";
        return output;
    }
}

//[CustomEditor(typeof(_ForTestingStuff))]
//public class _ForTesting_InspectorEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        base.OnInspectorGUI();

//        if (GUILayout.Button("Name all cells"))
//        {
//            CellSpriteManager[] arr = FindObjectsOfType<CellSpriteManager>();
//            foreach(CellSpriteManager i in arr)
//            {
//                i.AssignMyRowAndColumnIndex();
//            }
//        }
//    }
//}