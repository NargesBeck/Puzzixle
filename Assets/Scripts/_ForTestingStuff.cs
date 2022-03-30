using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class _ForTestingStuff : MonoBehaviour
{
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            ManagersSingleton.Managers.CameraMovement.GoHere(Pages.MainMenu);
        if (Input.GetKeyDown(KeyCode.B))
            ManagersSingleton.Managers.CameraMovement.GoHere(Pages.Puzzle);
        if (Input.GetKeyDown(KeyCode.C))
            ManagersSingleton.Managers.CameraMovement.GoHere(Pages.LevelWon);
        if (Input.GetKeyDown(KeyCode.D))
            ManagersSingleton.Managers.CameraMovement.GoHere(Pages.LevelFailed);
        if (Input.GetKeyDown(KeyCode.E))
            ManagersSingleton.Managers.CameraMovement.GoHere(Pages.BoardSelection);

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