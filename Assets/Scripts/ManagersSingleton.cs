using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagersSingleton : MonoBehaviour
{
    private static ManagersSingleton ManagersLinker;

    public static ManagersSingleton Managers
    {
        get
        {
            if (ManagersLinker == null)
            {
                ManagersLinker = FindObjectOfType<ManagersSingleton>();
            }
            return ManagersLinker;
        }
    }

    public Camera Camera;
    public Profile Profile;
    public MainMenu MainMenu;
    public PageTurner PageTurner;
    public GameManager GameManager;
    public PuzzleGenerator PuzzleGenerator;
    public PuzzlePageManager PuzzlePageManager;
    public LevelEndPageManager LevelWonPageManager;
    public LevelEndPageManager LevelLostPageManager;
    public BoardSelectionPageManager BoardSelectionPageManager;
}
